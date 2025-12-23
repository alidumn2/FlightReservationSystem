using FlightReservation.Core.Enums;
using FlightReservation.Core.Helpers;
using FlightReservation.Core.Helpers.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core.Entities
{
    public class Customer : User
    {
        public string TCKimlikNo { get; set; }

        public override bool Login(string username, string password)
        {
            if (this.UserName != username) return false;

            ////girilen şifreyi hashliyoruz
            string inputHash = SecurityHelper.HashPassword(password);

            // hashlenmiş şifre, veritabanındaki kayıtlı şifreyle aynı mı?
            return this.Password == inputHash;

        }

        public Reservation MakeReservation(Flight flight, Seat seat, int currentOccupancyCount)
        {


            PriceCalculatorEngine calculator = new PriceCalculatorEngine();

            // doluluk kuralını o anki doluluk sayısıyla oluşturup motora ekleriz
            calculator.AddRule(new OccupancyRule(currentOccupancyCount));

            // Fiyatı Hesaplar
            decimal finalPrice = calculator.CalculateFinalPrice(flight, seat);

            return new Reservation
            {
                Pnr = Guid.NewGuid().ToString().Substring(0, 6).ToUpper(),
                ReservationDate = DateTime.Now,
                PricePaid = finalPrice,
                ReservedCustomer = this,
                ReservedFlight = flight,
                SelectedSeat = seat
            };
        }

        public void CancelReservation(Reservation reservation)
        {

            if (reservation != null)
            {
                if (reservation.SelectedSeat != null)
                {
                    reservation.SelectedSeat.Status = SeatStatus.Available;
                }
            }
        }
    }
}
