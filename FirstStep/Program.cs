using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;
using Raven.Client.Document;
using Domain;

namespace FirstStep
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var documentStore = new DocumentStore { Url = "http://localhost:8081" })
            {
                documentStore.Initialize();
                using (var documentSession = documentStore.OpenSession())
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
            }
        }

        static void AddStudent(IDocumentSession documentSession, Student student)
        {
            documentSession.Store(student);
        }
    }
}
