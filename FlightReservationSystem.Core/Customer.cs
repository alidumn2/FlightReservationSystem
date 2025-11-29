using FlightReservation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core
{
    public class Customer : User
    {
        public string TCKimlikNo { get; set; }

        public List<Reservation> Reservations { get; private set; } // Müşterinin rezervasyon geçmişi

        public Customer()
        {
            Reservations = new List<Reservation>(); //Yapıcı metotla yeni müşteri oluştuğunda rezervasyon listesi oluşur
        }

        public override bool Login(string username, string password)
        {
            return (this.UserName == username && this.Password == password);

        }

        public Reservation MakeReservation(Flight flight, Seat seat)
        {


            seat.Status = SeatStatus.Occupied;

            var newReservation = new Reservation
            {
                Id = Reservations.Count + 1, // Bellek-içi liste için basit ID

                // Benzersiz bir ID (GUID) üretir, ilk 6 karakteri alır ve büyük harfe çevirir
                Pnr = Guid.NewGuid().ToString().Substring(0, 6).ToUpper(), 
                ReservedFlight = flight,
                ReservedCustomer = this,
                SelectedSeat = seat,
                ReservationDate = DateTime.Now,
                PricePaid = flight.BasePrice * seat.PriceMultiplier // Basit fiyatlandırma
            };

            Reservations.Add(newReservation);
            return newReservation; // Oluşan rezervasyonu geri döndür (UI'da göstermek için).
        }

        public void CancelReservation(string pnr)
        {

            // PNR koduna göre ilgili rezervasyonu listede bul
            var reservationToCancel = Reservations.FirstOrDefault(r => r.Pnr == pnr);

            if (reservationToCancel != null)
            {
                // Koltuğun durumunu tekrar "Boş" yap
                reservationToCancel.SelectedSeat.Status = SeatStatus.Available;

                // Rezervasyonu müşterinin listesinden kaldır
                Reservations.Remove(reservationToCancel);
                
              
            }

            
            
        }
    }
}
