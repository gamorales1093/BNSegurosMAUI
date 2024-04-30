using System;
using System.Collections.Generic;
using BNSegurosMAUI.Models;

namespace BNSegurosMAUI.WebServices
{
    public class GetAgentsResponse : BaseResponse
    {
        public List<Agent> Data { get; set; }
    }
}
