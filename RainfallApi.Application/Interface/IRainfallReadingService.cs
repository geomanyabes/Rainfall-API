using RainfallApi.Domain.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainfallApi.Application.Interface
{
    public interface IRainfallReadingService
    {
        Task<List<RainfallReading>> GetRainfallReadings(string stationId, int count);
    }
}
