using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecurringAccountTransfer.Core.BusinessObject.DTO;

namespace RecurringAccountTransfer.Api.Controllers
{
    public class SetupController : ApiController
    {
        [System.Web.Http.RoutePrefix("api/account/recurring/")]
        public class BankBvnController : ApiController
        {
            [System.Web.Http.HttpPost]
            [System.Web.Http.Route("setup")]
            public BankBvnResponse CreateRecurring(RecurringAccountRequest bankBvnRequest)
            {
                return KycLogic.GetCustomerBvnDetails(bankBvnRequest);
            }

            [System.Web.Http.HttpGet]
            [System.Web.Http.Route("get-frequencies")]
            public BankBvnResponse GetAllFrequencies()
            {
                return KycLogic.GetCustomerBvnDetails(bankBvnRequest);
            }
            //search by alias and by id 
            //disable and enable recurring 

        }
    }
}
