using System;
using System.Collections.Generic;

namespace BNSegurosMAUI.WebServices
{
    public interface IResponse
    {
        List<Error> Errors { get; set; }
        bool IsSuccessful { get; }
        string ErrorMessage { get; }

        void SetDefaultErrorState(int status);
    }
}
