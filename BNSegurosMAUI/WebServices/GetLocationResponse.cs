using System;
using System.Collections.Generic;
using BNSegurosMAUI.Models;
using Location = BNSegurosMAUI.Models.Location;

namespace BNSegurosMAUI.WebServices
{
    public class GetLocationResponse : BaseResponse
    {
        public List<Location> Data { get; set; }
    }
}
