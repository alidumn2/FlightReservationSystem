using FlightReservation.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core.Entities
{
    public class Airplane
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Capacity { get; private set; }
        public List<Seat> Seats { get; private set; }

        public Airplane()
        {
            Seats = new List<Seat>();
        }

        public Airplane(string model, int capacity)
        {
            Model = model;
            Capacity = capacity;
            Seats = new List<Seat>();

            //kapasitenin %10'u kadar business koltuk olsun
            int businessCount = (int)(capacity * 0.10);
            //koltuk düzeni 6'şarlı olduğu için business koltuk sayısını 6'nın katına yuvarlıyoruz
            while (businessCount % 6 != 0)
            {                 
                businessCount++;
            }

            for (int i = 1; i <= Capacity; i++)
            {
                Seat seat;

                if (i <= businessCount)
                {
                    seat = new BusinessSeat
                    {
                        SeatNumber = i.ToString(),
                        Status = SeatStatus.Available,
                    };
                }
                else
                {
                    seat = new Seat
                    {
                        SeatNumber = i.ToString(),
                        Status = SeatStatus.Available
                    };
                }

                Seats.Add(seat);
            }
        }

    }

}
    
