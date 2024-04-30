using System;
using System.Collections.Generic;
using BNSegurosMAUI.Models;

namespace BNSegurosMAUI.WebServices
{
    public class GetTermsAndConditionsResponse : BaseResponse
    {
        public List<TermsAndConditionsBody> Data { get; set; }

        public string GetDocumentUrl()
        {
            if (IsSuccessful && Data?.Count > 0)
            {
                return Data[0].Document;
            }
            return null;
        }

        public class TermsAndConditionsBody
        {
            public string Name { get; set; }
            public string Document { get; set; }
        }
    }
}
