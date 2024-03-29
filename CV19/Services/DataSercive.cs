﻿using CV19.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CV19.Services.Interfaces;

namespace CV19.Services
{
    internal class DataService : IDataService
    {
        public DataService()
        {
        }

        public static IDataService Instance { get; } = new DataService();

        private const string _DataSourceAddress =
            @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(
                _DataSourceAddress,
                HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        }

        private static IEnumerable<string> GetDataLines()
        {
            using var dataStream = (SynchronizationContext.Current is null ? GetDataStream() : Task.Run(GetDataStream))
                .Result;
            using var dataReader = new StreamReader(dataStream);
            while (!dataReader.EndOfStream)
            {
                var line = dataReader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                yield return line
                    .Replace("Korea,", "Korea -")
                    .Replace("United Kingdom", "")
                    .Replace("Bonaire,", "Bonaire -");
            }
        }

        private static DateTime[] GetDateTimes()
        {
            return GetDataLines()
                .First()
                .Split(',')
                .Skip(4)
                .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
                .ToArray();
        }

        public static IEnumerable<(string Province, string Country, (double Lat, double Lon) Place, int[] Counts)>
            GetCountriesData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));

            foreach (var row in lines)
            {
                var province = row[0].Trim();
                var countryName = row[1].Trim(' ', '"');
                var latitude = double.Parse((row[2] == "" ? "0" : row[2]), CultureInfo.InvariantCulture);
                var longitude = double.Parse((row[3] == "" ? "0" : row[3]), CultureInfo.InvariantCulture);
                var counts = row.Skip(5).Select(int.Parse).ToArray();

                yield return (province, countryName, (latitude, longitude), counts);
            }
        }

        public IEnumerable<CountryInfo> GetData()
        {
            var dates = GetDateTimes();

            var data = GetCountriesData().GroupBy(d => d.Country);

            foreach (var country_info in data)
            {
                var country = new CountryInfo
                {
                    Name = country_info.Key,
                    Provinces = country_info.Select(c => new PlaceInfo
                    {
                        Name = c.Province,
                        Location = new Point(c.Place.Lat, c.Place.Lon),
                        Counts =
                            dates.Zip(c.Counts, (date, count) => new ConfirmedCount { Date = date, Count = count })
                    })
                };
                yield return country;
            }
        }
    }
}