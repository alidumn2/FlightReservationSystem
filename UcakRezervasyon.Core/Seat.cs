using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core
{
    public class Seat
    {
        public string SeatNumber { get; set; }

        public SeatStatus Status { get; set; }

        public decimal PriceMultiplier { get; set; }


        public Seat()
        {
            Status = SeatStatus.Available;

            PriceMultiplier = 1.0m;
        }

        public override string ToString()
        {
                return $"Koltuk No: {SeatNumber} ({Status})";
        }
    }
}
