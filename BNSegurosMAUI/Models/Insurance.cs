using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.Models
{
    public enum InsuranceType
    {
        MedicalExpense = 1,
        Home,
        Unemployment,
        Other,
        Bicycle
    }

    public class Insurance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ImageSource Image { get; set; }

        public InsuranceType Type => (InsuranceType)Id;

        public static string ImageNameFromInsuranceType(InsuranceType type)
        {
            switch (type)
            {
                case InsuranceType.MedicalExpense:
                    return "insuranceicmedical.png";
                case InsuranceType.Home:
                    return "insuranceichousing.png";
                case InsuranceType.Unemployment:
                    return "insuranceicjobless.png";
                case InsuranceType.Other:
                    return "insuranceicothers.png";
                case InsuranceType.Bicycle:
                    return "insuranceicbicycle.png";
                default:
                    return "insuranceicothers.png";
            }
        }
    }
}
