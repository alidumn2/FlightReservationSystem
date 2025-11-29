using FlightReservation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core
{
    public class Airplane
    {
        public string Model { get; set; }

        public int Capacity { get; private set; }

        public List<Seat> Seats { get; private set; }


        public Airplane(string model, int capacity)
        {
            Model = model;
            Capacity = capacity;
            Seats = new List<Seat>();

            // Kapasite kadar koltuk döngü ile oluşturulup listeye eklenir.
            for (int i = 1; i <= Capacity; i++)
            {
                Seats.Add(new Seat { SeatNumber = i.ToString() });
            }
        }
    }

}
    
