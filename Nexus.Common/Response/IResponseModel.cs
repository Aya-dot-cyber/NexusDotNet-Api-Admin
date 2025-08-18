using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Common.Response
{
    public interface IResponseModel
    {
        ResponseModel Response(int StatusCode, string Message, dynamic Data);
    }

    public class ResponseModel : IResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public ResponseModel Response(int statusCode, string message, dynamic data)
        {
            return new ResponseModel
            {
                StatusCode = statusCode,
                Message = message,
                Data = data
            };
        }
    }
}

