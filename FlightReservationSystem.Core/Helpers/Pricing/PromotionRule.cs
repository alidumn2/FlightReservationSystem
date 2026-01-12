using FlightReservation.Core.Entities;
using FlightReservation.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Core.Helpers.Pricing
{
    public class PromotionRule : IPriceRule
    {
        private readonly string _enteredCode;

        public PromotionRule(string enteredCode)
        {
            _enteredCode = enteredCode?.Trim().ToUpper();
        }

        public decimal Calculate(decimal currentPrice, Flight flight, Seat seat)
        {
            // Örnek kuponlar ve indirim oranları
            if (_enteredCode == "PROMO10")
                return currentPrice * 0.90m;

            if (_enteredCode == "UCAK20")
                return currentPrice * 0.80m;

            return currentPrice;
        }
    }
}
