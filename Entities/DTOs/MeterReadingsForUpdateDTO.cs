using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class MeterReadingsForUpdateDTO
    {
        public DateTime MeterReadingDateTime { get; set; }

        public int MeterReadValue { get; set; }

        public int AccountId { get; set; }
    }
}
