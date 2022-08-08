using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double price;
        private int nightsCount;
        private double discount;
        private double total;

        public double Price 
        {
            get { return price; }
            set
            {
                if (value < 0) throw new ArgumentException();
                price = value;
                UpdateTotal();
                Notify(nameof(Price));
            }
        }

        public int NightsCount
        {
            get { return nightsCount; }
            set
            {
                if (value <= 0) throw new ArgumentException();
                nightsCount = value;
                UpdateTotal();
                Notify(nameof(NightsCount));
            }
        }

        public double Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                if (discount != ((-1) * Total / (Price * NightsCount) + 1) * 100)
                    UpdateTotal();
                Notify(nameof(Discount));
            }
        }

        public double Total
        {
            get { return total; }
            set
            {
                if (value < 0) throw new ArgumentException();
                total = value;
                if (total != Price * NightsCount * (1 - Discount / 100))
                    UpdateDiscount();
                Notify(nameof(Total));
            }
        }

        public AccountingModel()
        {
            Price = 0;
            NightsCount = 1;
            Discount = 0;
            Total = 0;
        }

        private void UpdateTotal()
        {
            Total = Price * NightsCount * (1 - Discount / 100);
        }

        private void UpdateDiscount()
        {
            Discount = ((-1) * Total / (Price * NightsCount) + 1) * 100;
        }
    }
}