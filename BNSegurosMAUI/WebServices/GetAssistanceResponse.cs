using System;
using System.Collections.Generic;
using BNSegurosMAUI.Models;

namespace BNSegurosMAUI.WebServices
{
    public class GetAssistanceResponse : BaseResponse
    {
        public List<Assistance> Data { get; set; }
    }
}
