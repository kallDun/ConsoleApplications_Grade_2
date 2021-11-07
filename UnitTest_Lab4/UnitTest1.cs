using Lab_4.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;

namespace UnitTest_Lab4
{
    [TestClass]
    public class TestFracNum
    {
        [TestMethod]
        public void ToStringTest()
        {
            FracNum fracNum1 = new(5, 10);
            Assert.AreEqual(fracNum1.ToString(), "5/10");
            FracNum fracNum2 = new(6, 18);
            Assert.AreEqual(fracNum2.ToString(), "6/18");
            FracNum fracNum3 = new(1, 15);
            Assert.AreEqual(fracNum3.ToString(), "1/15");
            FracNum fracNum4 = new(5, 1);
            Assert.AreEqual(fracNum4.ToString(), "5/1");
        }

        [TestMethod]
        public void AddTest()
        {
            FracNum fracNum1 = new(5, 10); 
            FracNum fracNum2 = new(1, 15);
            var added1 = fracNum1.Add(fracNum2);
            Assert.AreEqual(added1.ToString(), "85/150");
            Assert.AreEqual(added1.ToString(), (fracNum1 + fracNum2).ToString());

            FracNum fracNum3 = new(2, 8); 
            FracNum fracNum4 = new(3, 9);
            var added2 = fracNum3.Add(fracNum4);
            Assert.AreEqual(added2.ToString(), "42/72");
            Assert.AreEqual(added2.ToString(), (fracNum3 + fracNum4).ToString());

            FracNum fracNum5 = new(5, 10);
            FracNum fracNum6 = new(6, 12);
            Assert.AreEqual(fracNum5.Add(fracNum6).ToString(), fracNum6.Add(fracNum5).ToString());
        }

        [TestMethod]
        public void SubtractTest()
        {
            FracNum fracNum1 = new(5, 10);
            FracNum fracNum2 = new(1, 15);
            var subtracted1 = fracNum1.Subtract(fracNum2);
            Assert.AreEqual(subtracted1.ToString(), "65/150"); 
            Assert.AreEqual(subtracted1.ToString(), (fracNum1 - fracNum2).ToString());

            FracNum fracNum3 = new(2, 8);
            FracNum fracNum4 = new(3, 9);
            var subtracted2 = fracNum3.Subtract(fracNum4);
            Assert.AreEqual(subtracted2.ToString(), "-6/72");
            Assert.AreEqual(subtracted2.ToString(), (fracNum3 - fracNum4).ToString());

            FracNum fracNum5 = new(5, 10);
            FracNum fracNum6 = new(7, 12);
            Assert.AreEqual(fracNum5.Subtract(fracNum6).ToString(), "-" + fracNum6.Subtract(fracNum5).ToString());
        }

        [TestMethod]
        public void MultiplyTest()
        {
            FracNum fracNum1 = new(5, 10);
            FracNum fracNum2 = new(1, 15);
            var multiplied1 = fracNum1.Multiply(fracNum2);
            Assert.AreEqual(multiplied1.ToString(), "5/150");
            Assert.AreEqual(multiplied1.ToString(), (fracNum1 * fracNum2).ToString());

            FracNum fracNum3 = new(2, 8);
            FracNum fracNum4 = new(3, 9); 
            var multiplied2 = fracNum3.Multiply(fracNum4);
            Assert.AreEqual(multiplied2.ToString(), "6/72");
            Assert.AreEqual(multiplied2.ToString(), (fracNum3 * fracNum4).ToString()); 
            
            FracNum fracNum5 = new(5, 10);
            FracNum fracNum6 = new(7, 12);
            Assert.AreEqual(fracNum5.Multiply(fracNum6).ToString(), fracNum6.Multiply(fracNum5).ToString());
        }

        [TestMethod]
        public void DivideTest()
        {
            FracNum fracNum1 = new(5, 10);
            FracNum fracNum2 = new(1, 15);
            var divided1 = fracNum1.Divide(fracNum2);
            Assert.AreEqual(divided1.ToString(), "75/10");
            Assert.AreEqual(divided1.ToString(), (fracNum1 / fracNum2).ToString());

            FracNum fracNum3 = new(2, 8);
            FracNum fracNum4 = new(3, 9);
            var divided2 = fracNum3.Divide(fracNum4);
            Assert.AreEqual(divided2.ToString(), "18/24");
            Assert.AreEqual(divided2.ToString(), (fracNum3 / fracNum4).ToString());
        }

        [TestMethod]
        public void LeadToCommonDenominatorTest()
        {
            FracNum fracNum1 = new(5, 10);
            FracNum fracNum2 = new(1, 15);
            var (common_1, common_2) = fracNum1.LeadToCommonDenominator(fracNum2);
            Assert.AreEqual(common_1.ToString(), "75/150");
            Assert.AreEqual(common_2.ToString(), "10/150");

            FracNum fracNum3 = new(2, 8);
            FracNum fracNum4 = new(3, 9); 
            var (common_3, common_4) = fracNum3.LeadToCommonDenominator(fracNum4);
            Assert.AreEqual(common_3.ToString(), "18/72");
            Assert.AreEqual(common_4.ToString(), "24/72");
        }

        [TestMethod]
        public void CompareTest()
        {
            FracNum fracNum1 = new(5, 10);
            FracNum fracNum2 = new(1, 15);
            Assert.AreEqual(fracNum1.CompareTo(fracNum2), 1);

            FracNum fracNum3 = new(2, 8);
            FracNum fracNum4 = new(3, 9);
            Assert.AreEqual(fracNum3.CompareTo(fracNum4), -1);

            FracNum fracNum5 = new(2, 4);
            FracNum fracNum6 = new(3, 6);
            Assert.AreEqual(fracNum5.CompareTo(fracNum6), 0);
        }

        [TestMethod]
        public void DoubleValueTest()
        {
            FracNum fracNum1 = new(5, 10);
            Assert.AreEqual(fracNum1.DoubleValue.ToString(), "0.5");

            FracNum fracNum2 = new(6, 18);
            Assert.AreEqual(string.Format("{0:0.0000}", fracNum2.DoubleValue), "0.3333");

            FracNum fracNum3 = new(BigInteger.Parse("10000000000000000000"), BigInteger.Parse("40000000000000000000"));
            Assert.AreEqual(string.Format("{0:0.00}", fracNum3.DoubleValue), "0.25");
        }
    }

    [TestClass]
    public class TestComplexNum
    {
        [TestMethod]
        public void ToStringTest()
        {
            ComplexNum complexNum1 = new(5.2, 10.1);
            Assert.AreEqual(complexNum1.ToString(), "5.2+10.1i");
            ComplexNum complexNum2 = new(6.1, 18.5);
            Assert.AreEqual(complexNum2.ToString(), "6.1+18.5i");
            ComplexNum complexNum3 = new(1, 15.2);
            Assert.AreEqual(complexNum3.ToString(), "1+15.2i");
            ComplexNum complexNum4 = new(5.3, 1.5);
            Assert.AreEqual(complexNum4.ToString(), "5.3+1.5i");
        }

        [TestMethod]
        public void AddTest()
        {
            ComplexNum complexNum1 = new(5.2, 10.1);
            ComplexNum complexNum2 = new(6.1, 18.5);
            ComplexNum added1 = complexNum1.Add(complexNum2);
            Assert.AreEqual(added1.ToString(), "11.3+28.6i");
            Assert.AreEqual(added1.ToString(), (complexNum1 + complexNum2).ToString());

            ComplexNum complexNum3 = new(1, 15.2);
            ComplexNum complexNum4 = new(5.3, 1.5);
            ComplexNum added2 = complexNum3.Add(complexNum4);
            Assert.AreEqual(added2.ToString(), "6.3+16.7i");
            Assert.AreEqual(added2.ToString(), (complexNum3 + complexNum4).ToString());

            Assert.AreEqual(complexNum3.Add(complexNum4).ToString(), complexNum4.Add(complexNum3).ToString());
            Assert.AreEqual((complexNum3 + complexNum4).ToString(), (complexNum4 + complexNum3).ToString());
        }

        [TestMethod]
        public void SubtractTest()
        {
            ComplexNum complexNum1 = new(5.3, 10.1);
            ComplexNum complexNum2 = new(6.1, 9.5);
            ComplexNum subtracted1 = complexNum1.Subtract(complexNum2);
            Assert.AreEqual(subtracted1.ToString(), "-0.8+0.6i");
            Assert.AreEqual(subtracted1.ToString(), (complexNum1 - complexNum2).ToString());

            ComplexNum complexNum3 = new(1, 15.2);
            ComplexNum complexNum4 = new(5.3, 1.5);
            ComplexNum subtracted2 = complexNum3.Subtract(complexNum4);
            Assert.AreEqual(subtracted2.ToString(), "-4.3+13.7i");
            Assert.AreEqual(subtracted2.ToString(), (complexNum3 - complexNum4).ToString());
        }

        [TestMethod]
        public void MultiplyTest()
        {
            ComplexNum complexNum1 = new(5.3, 10.1);
            ComplexNum complexNum2 = new(6.1, 9.5);
            ComplexNum multiplied1 = complexNum1.Multiply(complexNum2);
            Assert.AreEqual(multiplied1.ToString(), "-63.62+111.96i");
            Assert.AreEqual(multiplied1.ToString(), (complexNum1 * complexNum2).ToString());
            Assert.AreEqual((complexNum1 * complexNum2).ToString(), (complexNum2 * complexNum1).ToString());
        }

        [TestMethod]
        public void DivideTest()
        {
            ComplexNum complexNum1 = new(1, 10);
            ComplexNum complexNum2 = new(2, 5);
            ComplexNum divided1 = complexNum1.Divide(complexNum2);
            Assert.AreEqual(divided1.ToString(), "1.79310344827586+0.517241379310345i");
            Assert.AreEqual(divided1.ToString(), (complexNum1 / complexNum2).ToString());
        }
    }
}
