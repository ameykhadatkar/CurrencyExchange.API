using CurrencyExchange.API.Filters;
using CurrencyExchange.Repository;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CurrencyExchange.API.Controllers
{
    [RoutePrefix("api/v0/rate")]
    public class RateController : ApiController
    {
        Rates ratesManager;

        public RateController()
        {
            ratesManager = new Rates();
        }

        /// <summary>
        /// This API returns the currency exchange rate for specific currencies for any amount entered.
        /// This API takes currencyCode and Amount as input
        /// </summary>
        /// <returns>HttpResponseMessage object which contains the rate details</returns>
        [HttpPost]
        [Route("")]
        [CustomCache(Duration = 120, VaryByParam = "currencyCode")]
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
