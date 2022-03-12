using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using SomerenModel;

namespace SomerenDAL
{
    public class DrinkDao : BaseDao
    {
        public List<Drink> GetAllDrinks()
        {
            string query = "SELECT Id, name, stock, salesPrice, alcoholic, nrOfSales FROM Drinks WHERE stock>1 AND salesPrice>1 AND name!='Water' AND name!='Orangeade' AND name!='Cherry juice' ORDER BY stock, salesPrice, nrOfSales;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        public void Add(Drink drink)
        {
            string query = "INSERT INTO Drinks (name, stock, salesPrice) VALUES (@name, @stock, @salesPrice);";
            SqlParameter[] sqlParameters = new SqlParameter[3]
            {
                new SqlParameter("@name", drink.Name),
                new SqlParameter("@stock", drink.Stock),
                new SqlParameter("@salesPrice", drink.SalesPrice)
            };
        }

        public void Update(Drink oldDrink, Drink newDrink)
        {

        }

        private List<Drink> ReadTables(DataTable dataTable)
        {
            List<Drink> drinks = new List<Drink>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Drink drink = new Drink()
                {
                    Id = (int)dr["Id"],
                    Name = (string)dr["name"],
                    SalesPrice = (decimal)dr["salesPrice"],
                    Stock = (int)dr["stock"],
                    Alcoholic = Convert.ToBoolean(dr["alcoholic"]),
                    NrOfSales = (int)dr["nrOfSales"]
                };
                drinks.Add(drink);
            }
            return drinks;
        }
    }
}
