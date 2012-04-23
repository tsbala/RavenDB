using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Person
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        Unknown,
        Male,
        Female
    }
}
