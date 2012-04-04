using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using Raven.Client.Linq.Indexing;


namespace FirstStepMVC.Code.Indexes.Student
{
    public class Student_ByName : AbstractIndexCreationTask<Domain.Student>
    {
        public Student_ByName()
        {
            Map = students => from s in students
                              select new
                                         {
                                             FirstName = s.FirstName.Boost(3), 
                                             s.LastName,
                                             s.DateOfBirth,
                                             s.Gender
                                         };
            Index(s => s.FirstName, FieldIndexing.Analyzed);
            Index(s => s.LastName, FieldIndexing.Analyzed);
        }
    }
}