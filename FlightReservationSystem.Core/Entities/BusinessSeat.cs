using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core.Entities
{
    public class BusinessSeat : Seat
    {
        public override decimal CalculatePrice(decimal baseFlightPrice)
        {
            return baseFlightPrice * 1.5m;
        }
    }
}
