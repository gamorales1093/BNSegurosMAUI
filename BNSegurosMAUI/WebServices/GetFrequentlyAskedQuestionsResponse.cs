using System;
using System.Collections.Generic;
using BNSegurosMAUI.Models;

namespace BNSegurosMAUI.WebServices
{
    public class GetFrequentlyAskedQuestionsResponse : BaseResponse
    {
        public List<FrequentlyAskedQuestion> Data { get; set; }
    }
}
