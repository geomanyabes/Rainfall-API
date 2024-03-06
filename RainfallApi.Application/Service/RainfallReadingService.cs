using RainfallApi.Application.Interface;
using RainfallApi.Domain.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainfallApi.Application.Service
{
    public class RainfallReadingService : IRainfallReadingService
    {

        Task<List<RainfallReading>> IRainfallReadingService.GetRainfallReadings(string stationId, int count)
        {
            throw new NotImplementedException();
        }
    }
}
