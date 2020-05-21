using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RecurringAccountTransfer.Core.BusinessObject.DTO
{
   public class BaseResponse
    {
        public HttpStatusCode ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }
}
