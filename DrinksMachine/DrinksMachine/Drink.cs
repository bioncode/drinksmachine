using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksMachine
{
    public abstract class Drink
    {
        public string Name { get; set; }

        public int Cost { get; set; }

        public int AvailableQuantity { get; set; }

        public int OrderQuantity { get; set; }

        public Drink()
        {
        }

        public Drink(string name, int cost, int quantity)
        {
            this.Name = name;
            this.Cost = cost;
            this.AvailableQuantity = quantity;
            this.OrderQuantity = 0;
        }
        public override string ToString()
        {
            return AvailableQuantity + " drinks available, Cost = " + Cost;
        }
    }
}
