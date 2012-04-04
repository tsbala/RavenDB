using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace FirstStepMVC.Code.Indexes.Student
{
    public class Student_FullSearch : AbstractIndexCreationTask<Domain.Student, Student_FullSearch.Result>
    {
        public class Result
        {
            public string Query { get; set; }
        }

        public Student_FullSearch()
        {
            Map = students => from s in students
                              select new 
                                         {
                                             Query = new object[] { s.FirstName, s.LastName }, 
                                         };
            Index(r => r.Query, FieldIndexing.Analyzed);
        }
    }
}