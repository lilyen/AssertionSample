using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ExpectedObjects;


namespace AssertionSample
{
    [TestClass]
    public class AssertionSample
    {
        private CustomerRepo customerRepo = new CustomerRepo();

        [TestMethod]
        public void CompareCustomer()
        {
            var actual = customerRepo.Get();
            var expect = new Customer()
            {
                Id = 2,
                Age = 18,
                Birthday = new DateTime(1990, 1, 26)
            }.ToExpectedObject();

            expect.ShouldEqual(actual);
        }

        [TestMethod]
        public void CompareCustomerList()
        {
            var actual = customerRepo.GetAll();

            var expect = new List<Customer>
            {
                new Customer()
                {
                    Id = 3,
                    Age = 20,
                    Birthday = new DateTime(1993, 1, 2)
                },

                new Customer()
                {
                    Id = 4,
                    Age = 21,
                    Birthday = new DateTime(1993, 1, 3)
                },
            };

            expect.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void CompareComposedCustomer()
        {
            var actual = customerRepo.GetComposedCustomer();

            var expect = new Customer()
            {
                Age = 30,
                Id = 11,
                Birthday = new DateTime(1999, 9, 9),
                Order = new Order { Id = 19, Price = 91 },
            }.ToExpectedObject();

            expect.ShouldEqual(actual);
        }

        //部分比較使用匿名型別
        [TestMethod]
        public void PartialCompare_Customer_Birthday_And_Order_Price()
        {
            var actual = customerRepo.GetComposedCustomer();

            var expected = new
            {
                Birthday = new DateTime(1999, 9, 9),
                Order = new { Price = 91 },
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }
    }

    public class CustomerRepo
    {
        public Customer Get()
        {
            return new Customer
            {
                Id = 2,
                Age = 18,
                Birthday = new DateTime(1990, 1, 26)
            };
        }

        public List<Customer> GetAll()
        {
            return new List<Customer>
            {
                new Customer()
                {
                    Id=3,
                    Age=20,
                    Birthday = new DateTime(1993,1,2)
                },

                new Customer()
                {
                    Id=4,
                    Age=21,
                    Birthday = new DateTime(1993,1,3)
                },
            };
        }

        public Customer GetComposedCustomer()
        {
            return new Customer()
            {
                Age = 30,
                Id = 11,
                Birthday = new DateTime(1999, 9, 9),
                Order = new Order { Id = 19, Price = 91 },
            };
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public int Price { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public Order Order { get; set; }
    }
}