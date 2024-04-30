using System;
using BNSegurosMAUI.Models;
using System.Collections.Generic;

namespace BNSegurosMAUI.WebServices
{
	public class GetOnlineInsurancesResponse : BaseResponse
    {
        public List<OnlineInsurance> Data { get; set; }
    }
}
