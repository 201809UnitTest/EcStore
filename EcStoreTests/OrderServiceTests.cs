using System;
using System.Collections.Generic;
using EcStoreLib;
using NSubstitute;
using Xunit;

namespace EcStoreTests
{
    public class OrderServiceTests
    {
        [Fact]
        public void sync_book_orders_when_2_book_orders_of_3_orders()
        {
//            var orderService = new OrderService();
            var orderService = new FakeOrderService();
            orderService.SetOrders(new List<Order>
            {
                new Order() {Type = "Book"},
                new Order() {Type = "CD"},
                new Order() {Type = "Book"},
            });

            var bookDao = Substitute.For<IBookDao>();
            orderService.SetBookDao(bookDao);
            orderService.SyncBookOrders();

            bookDao.Received(2).Insert(Arg.Is<Order>(o => o.Type == "Book"));
        }
    }

    internal class FakeOrderService : OrderService
    {
        private List<Order> _orders;
        private IBookDao _bookDao;

        internal void SetBookDao(IBookDao dao)
        {
            this._bookDao = dao;
        }

        protected override IBookDao GetBookDao()
        {
            return _bookDao;
        }

        internal void SetOrders(List<Order> orders)
        {
            this._orders = orders;
        }

        protected override List<Order> GetOrders()
        {
            return this._orders;
        }
    }
}