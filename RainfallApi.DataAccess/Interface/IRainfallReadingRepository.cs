using RainfallApi.Domain.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainfallApi.DataAccess.Interface
{
    public interface IRainfallReadingRepository
    {
        Task<List<RainfallData>?> GetReadingByStation(string stationId, int limit);
    }
}
