using System;
using System.Collections.Generic;
using BNSegurosMAUI.Models;

namespace BNSegurosMAUI.WebServices
{
    public class GetAdvertisementsResponse : BaseResponse
    {
        public List<Advertisement> Data { get; set; }
    }
}
