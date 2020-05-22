using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecurringAccountTransfer.Core.BusinessObject.DTO;
using RecurringAccountTransfer.Core.Logic;

namespace RecurringAccountTransfer.Api.Controllers
{
 
        [System.Web.Http.RoutePrefix("api/account/recurring")]
        public class SetupController : ApiController
        {
            [System.Web.Http.HttpPost]
            [System.Web.Http.Route("setup")]
            public IHttpActionResult CreateRecurring(RecurringAccountRequest req)
            {
                var response =  RecurringAccountLogic.Create(req);
                return Ok(response);
            }
            [System.Web.Http.HttpPost]
            [System.Web.Http.Route("search")]
            public IHttpActionResult SearchRecurring(SearchRequest req)
            {
                var response = RecurringAccountLogic.Search(req).ToArray();
                return Ok(response);
            }
            [System.Web.Http.HttpGet]
            [System.Web.Http.Route("id")]
            public IHttpActionResult GetRecurringById(Int64 recurringId)
            {
            //later retrieve profileId from security token of all request
                var response = RecurringAccountLogic.GetById(recurringId);
                return Ok(response);
            }
            [System.Web.Http.HttpGet]
            [System.Web.Http.Route("all-by-profile-id")]
            public IHttpActionResult GetAllRecurringByProfileId(Int64 profileId)
            {
                //later retrieve profileId from security token of all request
                var response = RecurringAccountLogic.GetAllByProfileId(profileId);
                return Ok(response);
            }
            //search by alias and by id 
            //disable and enable recurring 

        }
    }

