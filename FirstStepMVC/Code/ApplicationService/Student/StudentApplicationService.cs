using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
                        students = _session.Advanced.LuceneQuery<Domain.Student>()
                                        .WhereStartsWith("FirstName", name).Boost(3)
                                        .WhereStartsWith("LastName", name)
                                        .ToList();
                            break;
                    case SearchMode.Contains:
                        students = _session.Advanced.LuceneQuery<Domain.Student>()
                                            .Where(String.Format("FirstName:*{0}*", name)).Boost(3)
                                            .Where(String.Format("LastName:*{0}*", name))
                                            .ToList();

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