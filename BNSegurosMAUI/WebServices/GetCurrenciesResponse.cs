using System;
using System.Collections.Generic;
using BNSegurosMAUI.Models;

namespace BNSegurosMAUI.WebServices
{
    public class GetCurrenciesResponse : BaseResponse
    {
        public List<Currency> Data { get; set; }
    }
}
