using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core.Entities
{
    public class Flight
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public int DepartureAirportId { get; set; }
        public virtual Airport DepartureAirport { get; set; }
        public int ArrivalAirportId { get; set; }
        public virtual Airport ArrivalAirport { get; set; }
        public DateTime DepartureTime { get; set; }
        public int AirplaneId { get; set; }
        public Airplane Airplane { get; set; }
        public decimal BasePrice { get; set; }

        public override string ToString()
        {
        return $"{FlightNumber} | {DepartureAirport} -> {ArrivalAirport} | {DepartureTime.ToShortDateString()} | {BasePrice} TL";
        }

    }
}
