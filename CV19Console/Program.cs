using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CV19Console
{
    internal class Program
    {
        private const string data =
            @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        private static void Main(string[] args)
        {
            // WebClient client = new WebClient();
            var webClient = new HttpClient();
            var response = webClient.GetAsync(data).Result;
            var result = response.Content.ReadAsStringAsync().Result;

            Console.ReadLine();
        }
    }
}