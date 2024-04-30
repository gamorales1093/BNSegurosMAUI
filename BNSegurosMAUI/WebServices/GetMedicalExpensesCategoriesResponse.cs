using System;
using System.Collections.Generic;
using BNSegurosMAUI.Models;

namespace BNSegurosMAUI.WebServices
{
    public class GetMedicalExpensesCategoriesResponse : BaseResponse
    {
        public List<MedicalInsuranceCategory> Data { get; set; }
    }
}
