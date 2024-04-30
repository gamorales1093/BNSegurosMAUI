using System;
using System.Collections.Generic;
using BNSegurosMAUI.Models;

namespace BNSegurosMAUI.WebServices
{
    public class GetInsurancesResponse : BaseResponse
    {
        public List<Insurance> Data { get; set; }
    }
}
