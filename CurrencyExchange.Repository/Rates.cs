using CurrencyExchange.Common;
using CurrencyExchange.DomainObject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Repository
{
    public class Rates
    {
        RateRepository repository;

        public Rates()
        {
            repository = new RateRepository();
        }

        public Response GetRates(string requestData)
        {
            var postedData = JsonConvert.DeserializeObject<RatesRequest>(requestData);
            try
            {
                var acceptedCurrencyCodes = AppConfiguration.CurrenciesNeeded.Split(',');
                if (!acceptedCurrencyCodes.Contains(postedData.CurrencyCode))
                {
                    throw new Exception("Invalid currency code");
                }
                if (postedData.Amount <= 0)
                {
                    throw new Exception("Amount should be greater than zero.");
                }
                var currentRate = repository.GetCurrencyRates(postedData.CurrencyCode);

                var requestedRate = (1 / currentRate) * postedData.Amount;
                var response = new Response
                {
                    Amount = postedData.Amount,
                    ConversionRate = currentRate,
                    Err = AppConfiguration.SuccessMessage,
                    ReturnCode = 1,
                    SourceCurrency = postedData.CurrencyCode,
                    TimeStamp = DateTime.Now.ToString(),
                    Total = requestedRate
                };

                return response;
            }
            catch (Exception ex)
            {
                var response = new Response
                {
                    Amount = postedData.Amount,
                    Err = ex.Message,
                    ReturnCode = 2,
                    SourceCurrency = postedData.CurrencyCode,
                    TimeStamp = DateTime.Now.ToString(),
                    Total = 0
                };
                return response;
            }
        }
    }
}
