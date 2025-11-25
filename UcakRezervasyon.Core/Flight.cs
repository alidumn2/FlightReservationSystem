using FlightReservation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core
{
    public class Flight
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime DepartureTime { get; set; }
        public Airplane Airplane { get; set; }
        public decimal BasePrice { get; set; }

        public override string ToString()
        {
        return $"{FlightNumber} | {DepartureCity} -> {ArrivalCity} | {DepartureTime.ToShortDateString()} | {BasePrice} TL";
        }

    }
}
