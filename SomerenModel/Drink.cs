using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Drink
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Stock { get; set; }
        
        public decimal SalesPrice { get; set; }

        public bool Alcoholic { get; set; }

        public int NrOfSales { get; set; }

        public override string ToString()
        {
            return $"{Name} : {SalesPrice} : {Stock}";
        }
    }
}
