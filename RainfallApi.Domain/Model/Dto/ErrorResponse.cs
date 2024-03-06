using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainfallApi.Domain.Model.Dto
{
    public class ErrorResponse
    {
        public string? Message { get; set; }
        public List<ErrorDetail> Details { get; set; } = new();
    }
}
