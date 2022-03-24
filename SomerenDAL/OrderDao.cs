using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenModel;

namespace SomerenDAL
{
    public class OrderDao : BaseDao
    {
        public bool MakeOrder(Order o)
        {
            string query = "INSERT INTO Orders(ForeignStudent,ForeignDrink)"
                + "VALUES(@student, @drink)" +
                " UPDATE  Drinks" +
                " SET stock = stock-1" +
                " WHERE Id = @drink AND stock > 0";
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@student", o.CustomerId);
            sqlParameters[1] = new SqlParameter("@drink", o.DrinkId);
            ExecuteEditQuery(query, sqlParameters);
            return true;
        }
        private List<Order> ReadOrder(DataTable dataTable)
        {
            List<Order> orders = new List<Order>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Order Order = new Order()
                {
                    id = (int)dr["Id"],
                    CustomerId = (int)dr["ForeignStudent"],
                    DrinkId = (int)dr["ForeignDrink"]
                };
                orders.Add(Order);
            }
            return orders;
        }
    }
}