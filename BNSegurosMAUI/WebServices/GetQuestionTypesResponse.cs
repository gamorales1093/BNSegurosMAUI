using System;
using System.Collections.Generic;
using BNSegurosMAUI.Models;

namespace BNSegurosMAUI.WebServices
{
    public class GetQuestionTypesResponse : BaseResponse
    {
        public List<QuestionType> Data { get; set; }
    }
}
