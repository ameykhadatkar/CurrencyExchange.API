using CurrencyExchange.Common;
using CurrencyExchange.DomainObject;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.FixerIo
{
    public static class FixerIo
    {
        /// <summary>
        /// This method gets all the currency exchange rates which are needed based on the constants configured
        /// </summary>
        /// <returns>FixerIOResponse object which contains the currency rates of the required currencies</returns>
        public static FixerIOResponse GetCurrencyExchangeRates()
        {
            RestClient client = new RestClient(AppConfiguration.FixerIOUrl);
            RestRequest request = new RestRequest(string.Format(AppConfiguration.FixerIOCurrencyExchangeResource,AppConfiguration.CurrenciesNeeded, AppConfiguration.BaseCurrency), Method.GET);

            var response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //TODO: Logging mechanism
                return null;
            }
            var data = JsonConvert.DeserializeObject<FixerIOResponse>(response.Content);
            return data;
        }
    }
}
