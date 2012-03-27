using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Domain;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using Raven.Client.Linq;
using Raven.Database.Indexing;


namespace FirstStep
{
    class Program
    {
        static void Main(string[] args)
        {
            var add = false;
            var queryBy = String.Empty;
            var queryByValue = String.Empty;

            var settings = new NDesk.Options.OptionSet
                                {
                                    {"a|Add", v => add = v != null},
                                    {"qb=|QueryBy", v => queryBy = v}, 
                                    {"qv=|QueryByValue", v => queryByValue = v}
                                };
            settings.Parse(args);

            using (var documentStore = new EmbeddableDocumentStore { DataDirectory = "documentStore" })
            {
                documentStore.Initialize();
                using (var documentSession = documentStore.OpenSession())
                {
                    SetupIndex(documentStore);
                    
                    if (add || NoDocumentsFound<Student>(documentSession)) { AddStudents(documentSession); }
                    QueryDocument(documentSession, queryByValue, queryBy); 
                }
            }
        }

        private static void QueryDocument(IDocumentSession documentSession, string queryByValue, string queryBy)
        {
            if (!String.IsNullOrWhiteSpace(queryBy) && !String.IsNullOrWhiteSpace(queryByValue))
            {
                if (queryBy == "Index")
                {
                    QueryByIndex(documentSession);
                }
                else
                {
                    QueryStudents(documentSession, queryBy, queryByValue);
                }
            }
            else
            {
                QueryStudents(documentSession);
            }
        }

        private static void QueryByIndex(IDocumentSession documentSession)
        {
            var indexedResults = documentSession.Query<StudentCountByDobMonth>("StudentBy/DateOfBirth");
            WriteResultsToConsole(indexedResults, s => Console.WriteLine(String.Join(" ", s.Month, s.Count)));
        }

        private static bool NoDocumentsFound<T>(IDocumentSession documentSession)
        {
            var results = documentSession.Query<T>();
            return !results.Any();
        }

        private static void SetupIndex(IDocumentStore documentStore)
        {
            IndexCreation.CreateIndexes(typeof(StudentBy_DateOfBirth).Assembly, documentStore);
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

        static void QueryStudents(IDocumentSession documentSession, string query = null, string value = null)
        {
            var students = documentSession.Query<Student>();
            var results = from s in students
                          select s;
            if (!String.IsNullOrWhiteSpace(query))
            {
                switch (query)
                {
                    case "FirstName":
                        results = from r in results
                                  where r.FirstName == value
                                  select r;
                        break;
                    case "LastName":
                        results = from r in results
                                  where r.LastName == value 
                                  select r;
                        break;

                }
            }

            WriteResultsToConsole(results, s => Console.WriteLine(String.Join(" ", s.FirstName, s.LastName)));
        }

        static void WriteResultsToConsole<T>(IEnumerable<T> source, Action<T> action)
        {
            source.ToList().ForEach(action);
            Console.ReadLine();
        }

        static void AddStudent(IDocumentSession documentSession, Student student)
        {
            documentSession.Store(student);
        }
    }
}
