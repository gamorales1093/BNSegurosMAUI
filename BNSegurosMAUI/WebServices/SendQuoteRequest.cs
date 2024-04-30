using System;
namespace BNSegurosMAUI.WebServices
{
    public class SendQuoteRequest : IRequestBody
    {
        public string QuotationId { get; set; }
    }
}
