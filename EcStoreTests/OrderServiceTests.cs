using System;
using System.Collections.Generic;
using System.Linq;
using EcStoreLib;
using Moq;
using Moq.Protected;
using NSubstitute;
//using NSubstitute;
using Xunit;

namespace EcStoreTests
{
    public class OrderServiceTests
    {
//            var orderService = new OrderService();
        private readonly Mock<OrderService> _orderService = new Mock<OrderService>();
        private Mock<IBookDao> _bookDao = new Mock<IBookDao>();


        [Fact]
        public void sync_book_orders_when_2_book_orders_of_3_orders()
        {
            InitOrderService();

            GivenOrders(
                CreateOrder(type: "Book"),
                CreateOrder(type: "CD"),
                CreateOrder(type: "Book")
            );

            _orderService.Object.SyncBookOrders();

            BookDaoShouldInsertTimes(times: 2);
        }

        private void BookDaoShouldInsertTimes(int times)
        {
//            _bookDao.Received(times).Insert(Arg.Is<Order>(o => o.Type == "Book"));
            _bookDao.Verify(x => x.Insert(It.Is<Order>(order => order.Type == "Book")), Times.Exactly(times));
        }

        private void InitOrderService()
        {
            _orderService.Protected()
                .Setup<IBookDao>("GetBookDao")
                .Returns(_bookDao.Object);
//            _orderService.SetBookDao(_bookDao);
        }

        private static Order CreateOrder(string type)
        {
            return new Order() {Type = type};
        }

        private void GivenOrders(params Order[] orders)
        {
//            _orderService.SetOrders(orders.ToList());
            _orderService.Protected()
                .Setup<List<Order>>("GetOrders")
                .Returns(orders.ToList());
        }
    }

//
//    internal class FakeOrderService : OrderService
//    {
//        private List<Order> _orders;
//        private IBookDao _bookDao;
//
//        internal void SetBookDao(IBookDao dao)
//        {
//            this._bookDao = dao;
//        }
//
//        protected override IBookDao GetBookDao()
//        {
//            return _bookDao;
//        }
//
//        internal void SetOrders(List<Order> orders)
//        {
//            this._orders = orders;
//        }
//
//        protected override List<Order> GetOrders()
//        {
//            return this._orders;
//        }
//    }
}