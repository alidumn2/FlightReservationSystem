using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core.Entities
{
    public class Airport
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public override string ToString()
        {
            return $"{City} - {Name} ({Code})";
        }
    }
}
