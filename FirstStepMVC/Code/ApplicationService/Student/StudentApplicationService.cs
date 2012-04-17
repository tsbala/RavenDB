using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FirstStepMVC.Code.Indexes.Student;
using FirstStepMVC.Models;
using Raven.Client;
using Raven.Client.Linq;

namespace FirstStepMVC.Code.ApplicationService.Student
{
    public interface IStudentApplicationService
    {
        Domain.Student AddStudent(StudentViewModel studentViewModel);
        IEnumerable<StudentViewModel> GetStudents(string name, SearchMode searchMode);
    }

    public enum SearchMode
    {
        NotSet,
        BeginsWith,
        Contains
    }

    public class StudentApplicationService : IStudentApplicationService
    {
        private readonly IDocumentSession _session;

        static StudentApplicationService()
        {
            Mapper.Initialize(cfg =>
                                  {
                                      cfg.CreateMap<StudentViewModel, Domain.Student>();
                                      cfg.CreateMap<Domain.Student, StudentViewModel>();
                                  });
        }

        public StudentApplicationService(IDocumentSession session)
        {
            _session = session;
        }

        public Domain.Student AddStudent(StudentViewModel studentViewModel)
        {
            if (studentViewModel == null)
            {
                throw new ArgumentNullException("studentViewModel");
            }

            var student = Mapper.Map<Domain.Student>(studentViewModel);

            _session.Store(student);

            return student;
        }

        public IEnumerable<StudentViewModel> GetStudents(string name, SearchMode searchMode)
        {
            IEnumerable<Domain.Student> students;
            if (String.IsNullOrWhiteSpace(name))
            {
                students = _session.Query<Domain.Student>().ToList();
            }
            else 
            {
                switch (searchMode) 
                {
                    case SearchMode.BeginsWith:
                        students = _session.Query<Domain.Student, Student_ByName>()
                                        .Where(s => s.FirstName.StartsWith(name) || 
                                                    s.LastName.StartsWith(name))
                                        .ToList();
                            break;
                    case SearchMode.Contains:
                        var query = _session.Query<Student_FullSearch.ReduceResult, Student_FullSearch>()
                            .Search(x => x.Query, name)
                            .As<Domain.Student>();
                        students = query.ToList();

                        if (!students.Any())
                        {
                            var suggestions = query.Suggest();
                            if (suggestions.Suggestions.Length == 1)
                            {
                                return GetStudents(suggestions.Suggestions[0], searchMode);
                            }
                        }
                        break;
                    default:
                        students = _session.Query<Domain.Student>().ToList();
                        break;
                }
            }
            return Mapper.Map<IEnumerable<Domain.Student>, IEnumerable<StudentViewModel>>(students);
        }
    }
}