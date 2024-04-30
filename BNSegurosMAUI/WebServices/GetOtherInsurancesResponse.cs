using System;
using System.Collections.Generic;
using BNSegurosMAUI.Models;

namespace BNSegurosMAUI.WebServices
{
    public class GetOtherInsurancesResponse : BaseResponse
    {
        public List<OtherInsurance> Data { get; set; }
    }
}
