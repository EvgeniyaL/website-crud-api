using System;
using TitanGate.Website.Api.Contracts;
using TitanGate.Website.Api.Contracts.Response;

namespace TitanGate.Website.Api.Handlers
{
    public class Utility
    {
        public static Result<int, ErrorResponse> ErrorResponseInt()
        {
            return new Result<int, ErrorResponse>
            {
                IsSuccess = false,
                Fail = new ErrorResponse
                {
                    ErroMessage = "The resource is not found in the records."
                }
            };
        }

        public static Result<WebsiteResponse, ErrorResponse> ErrorResponse()
        {
            return new Result<WebsiteResponse, ErrorResponse>
            {
                IsSuccess = false,
                Fail = new ErrorResponse
                {
                    ErroMessage = "The resource is not found in the records."
                }
            };
        }
    }
}
