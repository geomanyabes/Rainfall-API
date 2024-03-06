using AutoMapper;
using RainfallApi.Application.Interface;
using RainfallApi.DataAccess.Interface;
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
        private readonly IRainfallReadingRepository _externalRepository;

        public RainfallReadingService(IRainfallReadingRepository externalRepository)
        {
            _externalRepository = externalRepository;
        }
        public async Task<List<RainfallReading>?> GetRainfallReadings(string stationId, int count)
        {
            var data = await _externalRepository.GetReadingByStation(stationId, count);
            var result = new List<RainfallReading>();
            if(data?.Any() == true)
            {
                foreach (var d in data)
                {
                    result.Add(new()
                    {
                        DateMeasured = d?.LatestReading?.DateTime,
                        AmountMeasured = d?.LatestReading?.Value
                    });
                }
            }

            return result;
        }
    }
}
