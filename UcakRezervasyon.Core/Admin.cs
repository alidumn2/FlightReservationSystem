using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core
{
    public class Admin : User
    {
        public override bool Login(string username, string password)
        {
            return (this.UserName == username && this.Password == password);

        }

        public void AddFlight(Flight newFlight, List<Flight> flightList)
        {
            flightList.Add(newFlight);
        }

        public void DeleteFlight(Flight flight, List<Flight> flightList)
        {
            if (flightList.Contains(flight))
            {
                flightList.Remove(flight);
            }
        }

    }

}