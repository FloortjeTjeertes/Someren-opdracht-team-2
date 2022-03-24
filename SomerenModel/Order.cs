using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Order
    {
        public int id { get; set; }
        //maybe store Customer object?
        public int CustomerId { get; set; }

        //maybe store drinnk object?
        public int DrinkId { get; set; }

        //for less conections
        public Drink drink { get; set; }

    }
}