using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainfallApi.Domain.Model.Dto
{
    public class RainfallReadingResponse
    {
        private List<RainfallReading> readings = new();

        public RainfallReadingResponse() { }
        public RainfallReadingResponse(List<RainfallReading> readings)
        {
            this.Readings = readings;
        }
        public List<RainfallReading> Readings { get => readings; set => readings = value; }
    }
}
