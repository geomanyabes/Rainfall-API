using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainfallApi.Domain.Model.Dto
{
    public class ErrorDetail
    {
        public string? PropertyName { get; set; }
        public string? Message { get; set; }
    }
}
