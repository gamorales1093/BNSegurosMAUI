using System;
using System.Collections.Generic;
using BNSegurosMAUI.Models;

namespace BNSegurosMAUI.WebServices
{
    public class GetMedicalExpensesClassesResponse : BaseResponse
    {
        public List<MedicalInsuranceClass> Data { get; set; }
    }
}
