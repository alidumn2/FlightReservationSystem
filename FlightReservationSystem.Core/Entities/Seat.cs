using FlightReservation.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core.Entities
{
    public class Seat
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; }
        public SeatStatus Status { get; set; }
        public int AirplaneId { get; set; }
        public virtual Airplane Airplane { get; set; }

        public Seat()
        {
            Status = SeatStatus.Available;
        }

        public virtual decimal CalculatePrice(decimal baseFlightPrice)
        {
            return baseFlightPrice;
        }

        public override string ToString()
        {
            return $"Koltuk No: {SeatNumber} ({Status})";
        }
    }
}
