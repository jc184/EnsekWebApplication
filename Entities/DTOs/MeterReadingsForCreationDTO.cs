using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class MeterReadingsForCreationDTO
    {
        public int Id { get; set; }

        public DateTime MeterReadingDateTime { get; set; }

        public int MeterReadingValue { get; set; }
    }
}
