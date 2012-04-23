using System;
using System.ComponentModel.DataAnnotations;
using Domain;
using FirstStepMVC.Validators;

namespace FirstStepMVC.Models
{
    public class StudentViewModel
    {
        [Required(ErrorMessage = "Every student has a first name !!!")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Every student has a last name !!!")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Every student must have a date of birth !!!")]
        [AgeRange("Age range for student is invalid")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [GenderRange("Gender for student is invalid")]
        public Gender Gender { get; set; }
    }
}