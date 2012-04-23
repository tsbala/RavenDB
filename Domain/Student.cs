using System;

namespace Domain
{
    public class Student : Person
    {
        public static Student Create(params object[] args)
        {
            return new Student
                       {
                           FirstName = (string)args[0], 
                           LastName = (string)args[1], 
                           DateOfBirth = (DateTime)args[2], 
                           Gender = (Gender)args[3]
                       };
        }
    }
}
