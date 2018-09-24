using System;
using EcStoreLib;
using Xunit;

namespace EcStoreTests
{
    public class OrderServiceTests
    {
        [Fact]
        public void sync_book_orders_when_2_book_orders_of_3_orders()
        {
            var orderService = new OrderService();
            orderService.SyncBookOrders();
        }
        
    }
}