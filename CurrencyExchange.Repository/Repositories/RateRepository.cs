using CurrencyExchange.Repository.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Repository
{
    public class RateRepository
    {
        /// <summary>
        /// Returns the currency rates in comparison with INR.
        /// Throws an error if the currency code is invalid
        /// </summary>
        /// <param name="currencyCode">currency code passed by the user</param>
        /// <returns>total amount</returns>
        public double GetCurrencyRates(string currencyCode)
        {
            using (var db = new CurrencyExchangeEntities())
            {
                var currencyRatePerRupee = (from e in db.CurrencyRates
                                            where e.CurrencyCode == currencyCode
                                            select e).FirstOrDefault();
                if (currencyRatePerRupee == null)
                {
                    throw new Exception("Invalid currency");
                }

                var rate = (double)currencyRatePerRupee.Rate;

                return rate;
            }
        }

        /// <summary>
        /// This method saves the currency rate for the sent currencyCode
        /// </summary>
        /// <param name="currencyCode">currency code</param>
        /// <param name="rate">rate of the above mentioned currency code in comparison with INR</param>
        public void SaveCurrencyRates(string currencyCode, double rate)
         {
            using (var db = new CurrencyExchangeEntities())
            {
                var currencyRatePerRupee = (from e in db.CurrencyRates
                                            where e.CurrencyCode == currencyCode
                                            select e).FirstOrDefault();
                if (currencyRatePerRupee == null)
                {
                    CurrencyRate data = new CurrencyRate
                    {
                        Rate = (decimal)rate,
                        CurrencyCode = currencyCode
                    };

                    db.CurrencyRates.Add(data);
                }
                else
                {
                    currencyRatePerRupee.Rate = (decimal)rate;
                }
                db.SaveChanges();
            }
        }
    }
}
