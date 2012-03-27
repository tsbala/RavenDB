using System.Linq;
using Domain;

namespace FirstStep
{
    public class StudentBy_DateOfBirth : Raven.Client.Indexes.AbstractIndexCreationTask<Student, StudentCountByDobMonth>
    {
        public StudentBy_DateOfBirth()
        {
            Map = students => from s in students
                              select new
                                         {
                                            s.DateOfBirth.Month,
                                            Count = 1
                                         };

            Reduce = results => from result in results
                                group result by result.Month
                                into g
                                select new
                                           {
                                               Month = g.Key,
                                               Count = g.Sum(x => x.Count)
                                           };
        }
    }

    public class StudentCountByDobMonth
    {
        public int Month { get; set; }
        public int Count { get; set; }
    }
}
