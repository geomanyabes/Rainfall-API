using RainfallApi.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainfallApi.Application.Service
{
    public class RainfallReadingService : IRainfallReadingService
    {
        public Task GetRainfallReadings(string stationId, int count)
        {
            throw new NotImplementedException();
        }
    }
}
