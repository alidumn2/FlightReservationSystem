using FlightReservation.Core.Entities;
using FlightReservation.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core.Helpers.Pricing
{
    public class OccupancyRule : IPriceRule
    {
        private readonly int _filledSeats; // Dolu koltuk sayısı

        public OccupancyRule(int filledSeats)
        {
            _filledSeats = filledSeats;
        }

        public decimal Calculate(decimal currentPrice, Flight flight, Seat seat)
        {
            if (flight.Airplane != null && flight.Airplane.Capacity > 0)
            {
                // Dolu koltuk oranını hesapla
                double occupancyRate = (double)_filledSeats / flight.Airplane.Capacity;
                if (occupancyRate > 0.60)
                {
                    return currentPrice * 1.30m;
                }
            }
            return currentPrice;
        }
    }
}
