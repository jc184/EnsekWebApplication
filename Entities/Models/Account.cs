using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }  
        public string LastName { get; set;}
        public ICollection<MeterReading> MeterReadings { get; set; }
    }
}
