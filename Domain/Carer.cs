using System.Collections.Generic;

namespace Domain
{
    public class Carer
    {
        public int CarerId { get; set; }
        public List<int> Children { get; set; }
        public CarerResponsibility CarerResponsibility { get; set; }
    }

    public enum CarerResponsibility
    {
        Father,
        Mother,
        StepFather,
        StepMother
    }
}
