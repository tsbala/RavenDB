using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain;
using FirstStepMVC.Models;
using Raven.Client;

namespace FirstStepMVC.Code
{
    public interface IStudentApplicationService
    {
        Student AddStudent(StudentViewModel studentViewModel);
        IEnumerable<StudentViewModel> GetStudents();
    }

    public class StudentApplicationService : IStudentApplicationService
    {
        private readonly IDocumentSession _documentSession;

        static StudentApplicationService()
        {
            Mapper.Initialize(cfg =>
                                  {
                                      cfg.CreateMap<StudentViewModel, Student>();
                                      cfg.CreateMap<Student, StudentViewModel>();
                                  });
        }

        public StudentApplicationService(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public Student AddStudent(StudentViewModel studentViewModel)
        {
            if (studentViewModel == null)
            {
                throw new ArgumentNullException("studentViewModel");
            }

            var student = Mapper.Map<Student>(studentViewModel);

            _documentSession.Store(student);

            return student;
        }

        public IEnumerable<StudentViewModel> GetStudents()
        {
            var students = from s in _documentSession.Query<Student>()
                           select s;
            return Mapper.Map<IEnumerable<Student>, IEnumerable<StudentViewModel>>(students.ToList());
        }
    }
}