using System;
using System.Collections.Generic;
using BNSegurosMAUI.Models;
using Contact = BNSegurosMAUI.Models.Contact;

namespace BNSegurosMAUI.WebServices
{
    public class GetContactsResponse : BaseResponse
    {
        public List<Contact> Data { get; set; }
    }
}
