using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CV19Console
{
    internal class Program
    {
        private const string data =
 @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(data, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = GetDataStream().Result;
            using var data_reader = new StreamReader(data_stream);
            while (!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if (string.IsNullOrEmpty(line)) continue;
                yield return line.Replace("Korea,", "Korea -");
            }
        }

        private static DateTime[] GetDateTimes() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<(string Contry, string Province, int[] Counts)> GetData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));

            foreach (var row in lines)
            {
                var province = row[0].Trim();
                var country_name = row[1].Trim(' ', '"');
                var counts = row
                    .Skip(5)
                    .Select(int.Parse)
                    .ToArray();
                yield return (country_name, province, counts);
            }
        }

        private static void Main(string[] args)
        {
            // WebClient client = new WebClient();
            // var webClient = new HttpClient();
            // var response = webClient.GetAsync(data).Result;
            // var result = response.Content.ReadAsStringAsync().Result;

            // foreach (var data_line in GetDataLines())
            // {
            //     Console.WriteLine(data_line);
            // }

            // var dates = GetDateTimes();
            // Console.WriteLine(string.Join("\r\n", dates));

            var Ukraine = GetData()
                .First(v => v.Contry.Equals("Ukraine", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(string.Join("\r\n",
                GetDateTimes().Zip(Ukraine.Counts, (date, count) => $"{date:dd:MM} - {count}")));

            Console.ReadLine();
        }
    }
}