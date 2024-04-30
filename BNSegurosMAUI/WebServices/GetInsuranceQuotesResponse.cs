using System;
using System.Collections.Generic;
using BNSegurosMAUI.Models;

namespace BNSegurosMAUI.WebServices
{
    public class GetInsuranceQuotesResponse : BaseResponse
    {
        public List<InsuranceQuote> Data { get; set; }
        public string Message { get; set; }
    }
}
