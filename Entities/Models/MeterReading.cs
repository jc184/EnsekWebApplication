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
        public int Id { get; set; } 

        public DateTime MeterReadingDateTime { get; set; }

        public int MeterReadValue { get; set; }

        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }

        public Account Account { get; set; }
    }
}
