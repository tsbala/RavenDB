using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Person
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Every student has a first name !!!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Every student has a last name !!!")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Every student must have a date of birth !!!")]
        [AgeRange("Age range for student is invalid")]
        public DateTime DateOfBirth { get; set; }
        [GenderRange("Gender for student is invalid")]
        public Gender Gender { get; set; }
    }

    public class AgeRangeAttribute : RangeAttribute
    {
        public AgeRangeAttribute(string errorMessage) : base(typeof(DateTime), DateTime.Today.AddYears(-15).ToShortDateString(), DateTime.Today.AddYears(-3).ToShortDateString())
        {
            ErrorMessage = errorMessage;
        }
    }

    public class GenderRangeAttribute : RangeAttribute
    {
        public GenderRangeAttribute(string errorMessage)
            : base(typeof(Gender), Gender.Male.ToString(), Gender.Female.ToString())
        {
            ErrorMessage = errorMessage;
        }
    }

    public enum Gender
    {
        Unknown,
        Male,
        Female
    }
}
