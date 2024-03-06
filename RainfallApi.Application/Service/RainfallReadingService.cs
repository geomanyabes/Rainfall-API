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
        private readonly IMapper _mapper;

        public RainfallReadingService(IRainfallReadingRepository externalRepository, IMapper mapper)
        {
            _externalRepository = externalRepository;
            _mapper = mapper;
        }
        public async Task<List<RainfallReading>> GetRainfallReadings(string stationId, int count)
        {
            var data = await _externalRepository.GetReadingByStation(stationId, count);

            throw new NotImplementedException();
        }
    }
}
