using NUnit.Framework;
using AreaCalculator;
using Figures;
using System;

namespace NUnitTest
{
    [TestFixture]
    public class NUnitTest1
    {
        [TestCase(2)]
        [TestCase(4.3)]
        [TestCase(1)]
        [TestCase(7.1)]
        [TestCase(-3.33)]
        [TestCase(-4.3)]
        public static void RunTestsCircle(double radius)
        {
            double expectedResult = Math.PI * Math.Pow(radius, 2);
            var result = new Circle(radius).Area();
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(5, 3, 5, 7.1545440106270926)]
        public static void RunTestsTriangle(double a, double b, double c, double expectedResult)
        {
            var result = new Triangle(a, b, c).Area();
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(11, 1.2, 0.2)]
        public void CreateTriangleWithWrongParameters(double a, double b, double c)
        {
            try
            {
                new Triangle(a, b, c);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }

        [TestCase(3, 4, 5)]
        public void CalculateTriangleAreaTest(double a, double b, double c)
        {
            Triangle triangle = new Triangle(a, b, c);
            double area = triangle.Area();

            Assert.IsTrue(area <= 6.0 && area > 5.99);
        }

        [TestCase(3, 4, 5)]
        public void CheckIfTriangleIsRight(double a, double b, double c)
        {
            Triangle triangle = new Triangle(a, b, c);
            bool testResult = triangle.CheckingRightTriangle();

            Assert.IsTrue(testResult);
        }
    }

    [TestFixture]
    public class NUnitTest2
    {
        [TestCase(2)]
        [TestCase(4.3)]
        [TestCase(1)]
        [TestCase(7.1)]
        [TestCase(-3.33)]
        [TestCase(-4.3)]
        public void TestAreaCalculatorCircle(double radius)
        {
            double expectedResult = Math.PI * Math.Pow(radius, 2);
            var result = AreaCalculator<Circle>.Calculate(new Circle(radius));
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(5, 3, 5, 7.1545440106270926)]
        public static void TestAreaCalculatorTriangle(double a, double b, double c, double expectedResult)
        {
            var result = AreaCalculator<Triangle>.Calculate(new Triangle(a,b,c));
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(3, 4, 5)]
        public void CalculateTriangleAreaTest(double a, double b, double c)
        {
            Triangle triangle = new Triangle(a, b, c);
            double area = AreaCalculator<Triangle>.Calculate(triangle);

            Assert.IsTrue(area <= 6.0 && area > 5.99);
        }
    }
}