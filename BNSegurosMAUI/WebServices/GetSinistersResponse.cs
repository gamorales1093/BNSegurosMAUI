using System;
using System.Collections.Generic;
using BNSegurosMAUI.Models;

namespace BNSegurosMAUI.WebServices
{
    public class GetSinistersResponse : BaseResponse
    {
        public List<Sinister> Data { get; set; }
    }
}
