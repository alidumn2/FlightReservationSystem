using FlightReservation.Core.Entities;
using FlightReservation.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core.Helpers.Pricing
{
    public class SeatTypeRule : IPriceRule
    {
        public decimal Calculate(decimal currentPrice, Flight flight, Seat seat)
        {
            if (seat is BusinessSeat)
            {
                return currentPrice * 1.50m;
            }
            return currentPrice;
        }
    }
}
