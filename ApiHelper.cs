using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Evesting
{
    public static class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();

            //use this if only using one uri per instance, but tihs may use differnt apis for data
            //ApiClient.BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/");

            ApiClient.DefaultRequestHeaders.Accept.Clear();
            //checks for data that is json only
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
    }
}
