using CurrencyExchange.Common;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace CurrencyExchange.Repository
{
    public class FetchAndSaveRates : Registry
    {
        RateRepository rateRepository;
        public FetchAndSaveRates()
        {
            rateRepository = new RateRepository();
            // Schedule a simple job to run at a specific time
            Schedule(() =>
            {
                StartWork();
            }).ToRunEvery(1).Minutes();

        }

        public void StartWork()
        {
            //HostingEnvironment.QueueBackgroundWorkItem(cancellationToken =>
            //{
            // Get rates
            var newRates = FixerIo.FixerIo.GetCurrencyExchangeRates();

            var currencyCodesSupported = AppConfiguration.CurrenciesNeeded.Split(',');

            foreach (var currencyCode in currencyCodesSupported)
            {
                var value = newRates.Rates.GetType().GetProperty(currencyCode).GetValue(newRates.Rates, null);
                rateRepository.SaveCurrencyRates(currencyCode, Convert.ToDouble(value));
            }
            //});
        }
    }
}
