﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Hole3
{
    public class TakeHomeCalculator {

        private readonly int percent;

        public TakeHomeCalculator(int percent) {
            this.percent = percent;
        }

        public Money NetAmount(Money first, params Money[] rest) {

            List<Money> pairs = rest.ToList();

            Money total = first;

            foreach (Money next in pairs) {
                if (!next.currency.Equals(total.currency)) {
                    throw new Incalculable();
                }
            }

            foreach (Money next in pairs) {
                total = new Money(total.value + next.value, next.currency);
            }

            Double amount = total.value * (percent / 100d);
            Money tax = new Money(Convert.ToInt32(amount), first.currency);

            if (!total.currency.Equals(tax.currency)) {
                throw new Incalculable();
            }
            return new Money(total.value - tax.value, first.currency);
        }

        public class Money {
            public readonly int value;
            public readonly String currency;

            public Money(int value, String currency) {
                this.value = value;
                this.currency = currency;
            }

        }
    }
    
    public class Incalculable : Exception {

    }
}