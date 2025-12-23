using FlightReservation.Core.Entities;
using FlightReservation.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core.Helpers.Pricing
{
    public class TimeRule : IPriceRule
    {
        public decimal Calculate(decimal currentPrice, Flight flight, Seat seat)
        {
            if ((flight.DepartureTime - DateTime.Now).TotalDays < 3)
            {
                return currentPrice * 1.20m;
            }
            return currentPrice;
        }
    }
}
