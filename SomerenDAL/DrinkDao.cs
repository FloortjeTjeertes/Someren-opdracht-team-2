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
            string query = "SELECT name, stock, salesPrice FROM Drinks WHERE stock>1 AND salesPrice>1 AND name!='Water' AND name!='Orangeade' AND name!='Cherry juice' ORDER BY stock, salesPrice, nrOfSales;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Drink> ReadTables(DataTable dataTable)
        {
            List<Drink> drinks = new List<Drink>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Drink drink = new Drink()
                {
                    Name = (string)dr["name"],
                    SalesPrice = (decimal)dr["salesPrice"],
                    Stock = (int)dr["stock"]
                    //Alcoholic = Convert.ToBoolean(dr["alcoholic"]),
                    //NrOfSales = (int)dr["nrOfSales"]
                };
                drinks.Add(drink);
            }
            return drinks;
        }
    }
}
