using System;
using System.ComponentModel.DataAnnotations;
using Domain;

namespace FirstStepMVC.Models
{
    public class StudentViewModel
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public Gender Gender { get; set; }
    }
}