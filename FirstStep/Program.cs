using System;
using Raven.Client;
using Domain;
using Raven.Client.Embedded;


namespace FirstStep
{
    class Program
    {
        static void Main(string[] args)
        {
            var add = false;
            var query = true;

            new NDesk.Options.OptionSet
            {
                { "a|Add", v => add = v != null },
                { "q|Query", v => query = v != null }
            }; 

            using (var documentStore = new EmbeddableDocumentStore { DataDirectory = "documentStore" })
            {
                documentStore.Initialize();
                using (var documentSession = documentStore.OpenSession())
                {
                    if (add) { AddStudents(documentSession); }
                    if (query) { QueryStudents(documentSession); }
                }
            }
        }

        static void AddStudents(IDocumentSession documentSession)
        {
            AddStudent(documentSession, Student.Create("Ben", "Abbot", new DateTime(2000, 4, 22), Gender.Male));
            AddStudent(documentSession, Student.Create("Francis", "Abbot", new DateTime(2000, 5, 17), Gender.Male));
            AddStudent(documentSession, Student.Create("James", "Langley", new DateTime(2000, 3, 21), Gender.Male));
            AddStudent(documentSession, Student.Create("David", "Globe", new DateTime(2000, 4, 2), Gender.Male));
            AddStudent(documentSession, Student.Create("Victoria", "Green", new DateTime(2000, 6, 3), Gender.Female));
            AddStudent(documentSession, Student.Create("James", "Gregory", new DateTime(2000, 5, 11), Gender.Male));
            AddStudent(documentSession, Student.Create("Bryan", "Holmes", new DateTime(2000, 2, 1), Gender.Male));
            AddStudent(documentSession, Student.Create("Jake", "Whitehall", new DateTime(2000, 8, 22), Gender.Male));
            documentSession.SaveChanges();
        }

        static void QueryStudents(IDocumentSession documentSession)
        {
            var students = documentSession.Query<Student>();
            foreach (var student in students)
	        {
                Console.WriteLine(String.Join(" ", student.FirstName, student.LastName));
            }

            Console.ReadLine();
        }

        static void AddStudent(IDocumentSession documentSession, Student student)
        {
            documentSession.Store(student);
        }
    }
}
