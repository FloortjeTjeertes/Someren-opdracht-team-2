﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Drink
    {
        public string Name { get; set; }

        public int Id { get; set; }


        public int Stock { get; set; }

        public decimal SalesPrice { get; set; }

        public bool Alcoholic { get; set; }

        public int NrOfSales { get; set; }

        //tostring to retrieve the name of the drink
        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
