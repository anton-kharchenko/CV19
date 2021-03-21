using System.Collections.Generic;
using CV19.Models;

namespace CV19.Services.Interfaces
{
    internal interface IDataService
    {
        IEnumerable<CountryInfo> GetData();
    }
}