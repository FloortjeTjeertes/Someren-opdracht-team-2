using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenDAL;
using SomerenModel;

namespace SomerenLogic
{
    public class OrderService
    {
        OrderDao OrderDao;
        public OrderService()
        {
            OrderDao = new OrderDao();
        }
        public void makeOrder(Order order)
        {
            OrderDao.MakeOrder(order);
        }
    }
}