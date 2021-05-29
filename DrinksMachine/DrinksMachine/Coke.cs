using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksMachine
{
    public class Coke : Drink
    {
        public const string description = " ";

        public Coke(int cost, int quantity) : base("Coke", cost, quantity)
        {
           
        }
    }
}
