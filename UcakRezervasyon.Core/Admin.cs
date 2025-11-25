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
            if (this.UserName == username && this.PasswordHash == password)
            {
                return true;

            }
            else
            {
                return false;

            }


        }
    }

}