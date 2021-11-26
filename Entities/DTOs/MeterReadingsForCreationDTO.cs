using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class MeterReadingsForCreationDTO
    {

        public DateTime MeterReadingDateTime { get; set; }

        public string MeterReadValue { get; set; }

        public int AccountId { get; set; }
    }
}
