using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FirstStepMVC.Code.Indexes.Student;
using FirstStepMVC.Models;
using Raven.Client;

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
        private readonly IDocumentSession _documentSession;

        static StudentApplicationService()
        {
            Mapper.Initialize(cfg =>
                                  {
                                      cfg.CreateMap<StudentViewModel, Domain.Student>();
                                      cfg.CreateMap<Domain.Student, StudentViewModel>();
                                  });
        }

        public StudentApplicationService(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public Domain.Student AddStudent(StudentViewModel studentViewModel)
        {
            if (studentViewModel == null)
            {
                throw new ArgumentNullException("studentViewModel");
            }

            var student = Mapper.Map<Domain.Student>(studentViewModel);

            _documentSession.Store(student);

            return student;
        }

        public IEnumerable<StudentViewModel> GetStudents(string name, SearchMode searchMode)
        {
            IEnumerable<Domain.Student> students;
            if (String.IsNullOrWhiteSpace(name))
            {
                students = _documentSession.Query<Domain.Student>().ToList();
            }
            else
            {
                switch (searchMode)
                {
                    case SearchMode.BeginsWith:
                        students = _documentSession.Query<Domain.Student, Student_ByName>()
                                        .Where(s => s.FirstName.StartsWith(name) || s.LastName.StartsWith(name))
                                        .ToList();
                        break;
                    case SearchMode.Contains:
                        students = _documentSession.Advanced
                                        .LuceneQuery<Domain.Student, Student_ByName>()

                                        .WhereIn("FirstName", new[] { name })
                                        .OrElse()
                                        .WhereIn("LastName", new[] { name })
                                        .ToList();
                        break;
                    default:
                        students = _documentSession.Query<Domain.Student>().ToList();
                        break;
                }
            }
            return Mapper.Map<IEnumerable<Domain.Student>, IEnumerable<StudentViewModel>>(students);
        }
    }
}