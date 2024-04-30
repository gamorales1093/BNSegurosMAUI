using System;
using System.Collections.Generic;
using BNSegurosMAUI.Resources.Text;

namespace BNSegurosMAUI.WebServices
{
    public abstract class BaseResponse : IResponse
    {
        public List<Error> Errors { get; set; }

        public bool IsSuccessful
        {
            get
            {
                return Errors == null || Errors.Count == 0;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return (Errors != null && Errors.Count > 0) ? Errors[0].Message : null;
            }
        }

        public void SetDefaultErrorState(int status)
        {
            var error = new Error()
            {
                Status = status,
                Title = TextResources.AlertErrorTitle,
                Message = TextResources.AlertDefaultError
            };
            Errors = new List<Error>() { error };
        }
    }
}
