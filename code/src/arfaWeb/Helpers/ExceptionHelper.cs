
using arfa.Interface.Exceptions;
using arfaWeb.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arfaWeb.Helpers
{
    public class ExceptionHelper
    {
        public static TResponse CreateResponse<TResponse>(Exception e)  where TResponse : Response, new()
        {
            var response = new TResponse();

            if (e.GetType() == typeof(ArfaException))
            {
                var arfaException = e as ArfaException;
                response.status = arfaException.ErrorCode;
                response.message = arfaException.Message;
            }
            else
            {
                response.status = "FAILED";
                response.message = "Unknown exception occurred at controller: " + e.Message;
            }

            return response;
        }
    }
}
