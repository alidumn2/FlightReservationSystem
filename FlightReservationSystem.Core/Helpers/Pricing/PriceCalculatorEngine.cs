using FlightReservation.Core.Entities;
using FlightReservation.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core.Helpers.Pricing
{
    public class PriceCalculatorEngine
    {
        // Kuralları tutan liste
        private List<IPriceRule> _rules;

        public PriceCalculatorEngine()
        {
            _rules = new List<IPriceRule>();

            // Varsayılan kuralları ekliyoruz
            _rules.Add(new TimeRule());
            _rules.Add(new SeasonRule());
            _rules.Add(new SeatTypeRule());
        }

        // Dinamik kural eklemek için addrule metodu tanımlıyoruz (Örn: Doluluk oranı her uçuşta değişir)
        public void AddRule(IPriceRule rule)
        {
            _rules.Add(rule);
        }

        public decimal CalculateFinalPrice(Flight flight, Seat seat)
        {
            decimal price = flight.BasePrice;

            // Tüm kuralları sırayla gez
            foreach (var rule in _rules)
            {
                // Her kural kendi mantığına göre fiyatı değiştirir
                price = rule.Calculate(price, flight, seat);
            }

            return Math.Round(price, 2);
        }
    }
}
