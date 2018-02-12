using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FluentAssertions;

namespace AssertionSample
{
    [TestClass]
    public class AssertExceptionSample
    {
        [TestMethod]
        public void Divide_positive()
        {
            var calculator = new Calculator();
            var actual = calculator.Divide(5, 2);
            Assert.AreEqual(2.5m, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(YouShallNotPassException))]
        public void Divide_91_2()
        {
            var calculator = new Calculator();
            var actual = calculator.Divide(5, 91);
        }

        [TestMethod]
        public void Divide_91()
        {
            var calculator = new Calculator();
            Action action = () => { calculator.Divide(5, 91); };

            action.ShouldThrow<YouShallNotPassException>();
        }
    }

    public class Calculator
    {
        public decimal Divide(decimal first, decimal second)
        {
            if (second == 0 || second == 91)
            {
                throw new YouShallNotPassException();
            }
            return first / second;
        }
    }

    public class YouShallNotPassException : Exception
    {
    }
}