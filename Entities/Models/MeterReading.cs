using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class MeterReading
    {
        public DateTime MeterReadingDateTime { get; set; }
        public string MeterReadValue { get; set; }
        public int AccountId { get; set; }

    }
}
