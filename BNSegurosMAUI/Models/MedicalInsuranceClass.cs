using System;
namespace BNSegurosMAUI.Models
{
    public class MedicalInsuranceClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Maternity { get; set; }
        public string Location { get; set; }

        public string DisplayedText => string.Format("{0} - {1}", Location, Maternity);
    }
}
