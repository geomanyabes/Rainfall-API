using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainfallApi.Domain.Model.Entity
{
    public class RainfallReading
    {
        public string? DateMeasured { get; set; }
        public decimal? AmountMeasured { get; set; }
    }
}
