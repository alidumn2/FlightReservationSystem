using FlightReservation.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core.Interfaces
{
    public interface IPriceRule
    {
        // Gelen fiyatı alır, kuralı uygular, yeni fiyatı döndürür
        decimal Calculate(decimal currentPrice, Flight flight, Seat seat);
    }
}
