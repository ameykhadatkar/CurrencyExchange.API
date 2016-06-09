﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Common
{
    public static class AppConfiguration
    {
        public static string BaseCurrency = "INR";
        public static string CurrenciesNeeded = "USD,GBP,AUD,EUR,CAD,SGD";
        public static string SuccessMessage = "success";
        public static string FixerIOUrl = "http://api.fixer.io";
        public static string FixerIOCurrencyExchangeResource= "latest?symbols={0}&base={1}";

    }
}
