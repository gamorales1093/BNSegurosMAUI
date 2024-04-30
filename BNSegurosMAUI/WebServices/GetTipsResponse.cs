using System;
using System.Collections.Generic;
using BNSegurosMAUI.Models;

namespace BNSegurosMAUI.WebServices
{
    public class GetTipsResponse : BaseResponse
    {
        public List<Tip> Data { get; set; }
    }
}
