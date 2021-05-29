using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksMachine
{
    class CustomerChange
    {
        public int Penny { get; set; }
        public int Nickel { get; set; }
        public int Dime { get; set; }
        public int Quarter { get; set; }

        public CustomerChange(int penny, int nickel, int dime, int quarter)
        {
            this.Penny = penny;
            this.Nickel = nickel;
            this.Dime = dime;
            this.Quarter = quarter;
        }
    }
}
