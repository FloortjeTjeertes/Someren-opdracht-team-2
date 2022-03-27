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
        //query to get all drinks
        public List<Drink> GetAllDrinks()
        {
            string query = "SELECT Id, name, stock, salesPrice, alcoholic, nrOfSales FROM Drinks WHERE stock>1 AND salesPrice>1 AND name!='Water' AND name!='Orangeade' AND name!='Cherry juice' ORDER BY stock, salesPrice, nrOfSales;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        //query to add a drink to the Drinks database table
        public void Add(Drink drink)
        {
            string query = "INSERT INTO Drinks (name, stock, salesPrice, alcoholic, nrOfSales) VALUES (@name, @stock, @salesPrice, @alcoholic, @nrOfSales);";
            SqlParameter[] sqlParameters = new SqlParameter[5]
            {
                new SqlParameter("@name", drink.Name),
                new SqlParameter("@stock", drink.Stock),
                new SqlParameter("@salesPrice", drink.SalesPrice),
                new SqlParameter("@alcoholic", drink.Alcoholic),
                new SqlParameter("@nrOfSales", drink.NrOfSales)
            };
            ExecuteEditQuery(query, sqlParameters);
        }

        //query to update a selected drink (oldDrink) to an inserted drink (newDrink)
        public void Update(Drink oldDrink, Drink newDrink)
        {
            string query = "UPDATE Drinks SET name=@newName, stock=@newStock, salesPrice=@newSalesPrice, alcoholic=@newAlcoholic, nrOfSales=@newNrOfSales WHERE Id=@oldId;";
            SqlParameter[] sqlParameters = new SqlParameter[6]
            {
                new SqlParameter("@newName", newDrink.Name),
                new SqlParameter("@newStock", newDrink.Stock),
                new SqlParameter("@newSalesPrice", newDrink.SalesPrice),
                new SqlParameter("@newAlcoholic", newDrink.Alcoholic),
                new SqlParameter("@newNrOfSales", newDrink.NrOfSales),
                new SqlParameter("@oldId", oldDrink.Id)
            };
            ExecuteEditQuery(query, sqlParameters);
        }

        //query to delete a selected drink
        public void Delete(Drink drink)
        {
            string query = "DELETE FROM Drinks WHERE Id=@Id";
            SqlParameter[] sqlParameters = new SqlParameter[1]
            {
                new SqlParameter("@Id", drink.Id)
            };
            ExecuteEditQuery(query, sqlParameters);
        }

        //method to read the data from the query that the sql database returns
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
