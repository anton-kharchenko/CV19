using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CV19.Models;

namespace CV19.Services
{
    internal class DataService
    {
        private const string _DataSourceAddress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_DataSourceAddress,
                HttpCompletionOption.ResponseHeadersRead);
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
                yield return line
                    .Replace("Korea,", "Korea -")
                    .Replace("Bonaire", "Bonaire -");
            }
        }

        private static DateTime[] GetDateTimes() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();

        public static IEnumerable<(string Province, string Country, (double Lat, double Lon), int[] Counts)> GetCountriesData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));

            foreach (var row in lines)
            {
                var province = row[0].Trim();
                var country_name = row[1].Trim(' ', '"');
                var latidude = double.Parse(row[2]);
                var longidude = double.Parse(row[3]);
                var counts = row.Skip(5).Select(int.Parse).ToArray();

                yield return (province, country_name, (latidude, longidude), counts);
            }
        }

        public IEnumerable<CountryInfo> GetData()
        {
            var dates = GetDateTimes();

            var data = GetCountriesData().GroupBy(d => d.Country);

            foreach (var country_info in data)
            {
                var country = new CountryInfo()
                {
                    Name = country_info.Key,
                    ProvincesCount = country_info.Select(c => new PlaceInfo
                    {
                        Name = c.Province,
                        Location = new Point(c.Item3.Lat, c.Item3.Lon),
                        Counts =
                            dates.Zip(c.Counts, (data, count) => new ConfirmedCount { Date = data, Count = count })
                    })
                };
                yield return country;
            }
        }
    }
}