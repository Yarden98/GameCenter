﻿using GameCenter.Project.CurrencyConverter.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace GameCenter.Project.CurrencyConverter.Services
{
    class CurrencyService
    {
        private const string BaseApiEndPoint = "http://api.exchangeratesapi.io/latest";
        private const string ApiKey = "f3b6c3b722d37fc98d416092bbc76d62";
        private HttpClient Http_Client = new HttpClient();



        public async Task< Dictionary<string,double>> GetExchangeRatesAsync()
        {
            string requestUrl = $"{BaseApiEndPoint}?access_key={ApiKey}";
            string response =   await Http_Client.GetStringAsync(requestUrl);


            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            ExchangeResponse exchangeData = JsonSerializer.Deserialize<ExchangeResponse>(response,options);
            if (exchangeData == null || exchangeData.Rates == null)
                throw new InvalidOperationException("Failed to fetch exchange rates.");

            return exchangeData.Rates;

        }
    }
}
 