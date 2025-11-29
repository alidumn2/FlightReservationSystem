using FlightReservation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Pnr { get; set; }
        public DateTime ReservationDate { get; set; }
        public decimal PricePaid { get; set; }
        public Customer ReservedCustomer { get; set; }
        public Flight ReservedFlight { get; set; }
        public Seat SelectedSeat { get; set; }

        public override string ToString()
        {
            return $"PNR: {Pnr}  |  Uçuş: {ReservedFlight.FlightNumber}  |  {ReservedFlight.DepartureCity}  ->  {ReservedFlight.ArrivalCity}  |  Koltuk: {SelectedSeat.SeatNumber}"; 
        }
       
        
    }
}
