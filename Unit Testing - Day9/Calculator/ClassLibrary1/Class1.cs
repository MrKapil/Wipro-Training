using CalculatorTest;
using NUnit.Framework;
using System;
namespace ClassLibrary1
{
    [TestFixture]
    public class CalculatorTest
    {
        [Test]
        public void shouldaddtwoNumbers()
        {
            var c = new Calculator();
            var result = c.Add(2, 3);

            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void shouldSubtractTwoNumbers()
        {
            var c = new Calculator();
            var result = c.Subtract(3, 2);

            Assert.That(result, Is.EqualTo(1));
        }
        [Test]
        public void shouldMultiplyTwoNumber()
        {
            var c = new Calculator();
            var result = c.Multiply(2, 3);

            Assert.That(result, Is.EqualTo(6));
        }
        [Test]
        public void shouldDivideTwoNumber()
        {
            var c = new Calculator();
            var result = c.Divide(4, 2);

            Assert.That(result, Is.EqualTo(2));
        }

    }

}


