using System;
using System.Collections.Generic;
using System.Linq;
using EcStoreLib;
using NSubstitute;
using Xunit;

namespace EcStoreTests
{
    public class OrderServiceTests
    {
//            var orderService = new OrderService();
        private readonly FakeOrderService _orderService = new FakeOrderService();
        private IBookDao _bookDao = Substitute.For<IBookDao>();

        
        [Fact]
        public void sync_book_orders_when_2_book_orders_of_3_orders()
        {
            InitOrderService();
            
            GivenOrders(
                CreateOrder(type: "Book"),
                CreateOrder(type: "CD"),
                CreateOrder(type: "Book")
            );

            _orderService.SyncBookOrders();

            BookDaoShouldInsertTimes(times: 2);
        }

        private void BookDaoShouldInsertTimes(int times)
        {
            _bookDao.Received(times).Insert(Arg.Is<Order>(o => o.Type == "Book"));
        }

        private void InitOrderService()
        {
            _orderService.SetBookDao(_bookDao);
        }

        private static Order CreateOrder(string type)
        {
            return new Order() {Type = type};
        }

        private void GivenOrders(params Order[] orders)
        {
            _orderService.SetOrders(orders.ToList());
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