using FlightReservation.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core.Entities
{
    public class Admin : User
    {
        public override bool Login(string username, string password)
        {
            if (this.UserName != username) return false;

            //girilen şifreyi hashliyoruz
            string inputHash = SecurityHelper.HashPassword(password);

            //hashlenmiş şifre, veritabanındaki kayıtlı şifreyle aynı mı?
            return this.Password == inputHash;

        }

        public Flight AddFlight(string ucusNo, int kalkisId, int varisId, DateTime zaman, int ucakId, decimal fiyat)
        {
            return new Flight
            {
                FlightNumber = ucusNo,
                DepartureAirportId = kalkisId,
                ArrivalAirportId = varisId,
                DepartureTime = zaman,
                AirplaneId = ucakId,
                BasePrice = fiyat
            };
        }

        public void UpdateFlight(Flight flight, string yeniNo, int kalkisId, int varisId, int ucakId, DateTime yeniTarih, decimal yeniFiyat)
        {
            flight.FlightNumber = yeniNo;
            flight.DepartureAirportId = kalkisId;
            flight.ArrivalAirportId = varisId;
            flight.AirplaneId = ucakId;
            flight.DepartureTime = yeniTarih;
            flight.BasePrice = yeniFiyat;
        }

    }

}