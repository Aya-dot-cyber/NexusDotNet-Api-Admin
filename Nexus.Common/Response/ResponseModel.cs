using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watan.Recruting.Common.Response
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


        ResponseModel IResponseModel.Response(int StatusCode, string Message, dynamic Data)
        {
            return new ResponseModel { StatusCode = StatusCode, Data = Data, Message = Message };
        }
    }
}
