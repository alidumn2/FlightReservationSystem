using FlightReservation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UcakRezervasyon.Core
{
    public class Customer : User
    {
        public string TCKimlikNo { get; set; }

        public List<Reservation> Reservations { get; private set; }

        public Customer()
        {
            Reservations = new List<Reservation>();
        }

        public override bool Login(string username, string password)
        {
            if (this.UserName == username && this.PasswordHash == password)
            {
                return true;

            }
            else
            {
                throw new ArgumentException("Kullanıcı adı veya şifre hatalı!");

            }


        }

        public Reservation MakeReservation(Flight flight, Seat seat)
        {

            if (seat.Status == SeatStatus.Occupied)
            {
                throw new ArgumentException("Bu Koltuk Dolu Lütfen Başka Koltuk Seçiniz");
               
            }

            seat.Status = SeatStatus.Occupied;

            var newReservation = new Reservation
            {
                Id = Reservations.Count + 1, // Bellek-içi liste için basit ID
                Pnr = Guid.NewGuid().ToString().Substring(0, 6).ToUpper(), // Benzersiz PNR
                ReservedFlight = flight,
                ReservedCustomer = this,
                SelectedSeat = seat,
                ReservationDate = DateTime.Now,
                PricePaid = flight.BasePrice * seat.PriceMultiplier // Basit fiyatlandırma
            };

            Reservations.Add(newReservation);
            return newReservation;
        }

        public bool CancelReservation(string pnr)
        {
            var reservationToCancel = Reservations.FirstOrDefault(r => r.Pnr == pnr);

            if (reservationToCancel != null)
            {
                // 1. Koltuğun durumunu tekrar "Boş" yap
                reservationToCancel.SelectedSeat.Status = SeatStatus.Available;

                // 2. Rezervasyonu müşterinin listesinden kaldır
                Reservations.Remove(reservationToCancel);
                throw new ArgumentException("Rezervasyon Başarıyla silindi");
                return true;
            }

            throw new ArgumentException("Rezervasyon Bulunamadı!");
            return false;
        }
    }
}
