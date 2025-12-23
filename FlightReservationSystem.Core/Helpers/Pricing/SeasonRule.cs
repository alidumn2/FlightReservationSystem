using FlightReservation.Core.Entities;
using FlightReservation.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core.Helpers.Pricing
{
    public class SeasonRule : IPriceRule
    {
        public decimal Calculate(decimal currentPrice, Flight flight, Seat seat)
        {
            int month = flight.DepartureTime.Month;
            if (month == 6 || month == 7 || month == 8)
            {
                return currentPrice * 1.35m;
            }
            return currentPrice;
        }
    }
}
