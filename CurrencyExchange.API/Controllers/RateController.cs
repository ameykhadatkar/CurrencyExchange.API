using CurrencyExchange.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CurrencyExchange.API.Controllers
{
    [RoutePrefix("api/v1/rate")]
    public class RateController : ApiController
    {
        Rates ratesManager;

        public RateController()
        {
            ratesManager = new Rates();
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage GetCurrencyRates()
        {
            try
            {
                var response = ratesManager.GetRates(Request.Content.ReadAsStringAsync().Result);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                //TODO: Log the error

                return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "Server error. Please try again later." });
            }

        }
    }
}
