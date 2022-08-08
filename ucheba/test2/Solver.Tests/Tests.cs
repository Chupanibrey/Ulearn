using NUnit.Framework;

namespace Solver.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void SingleTest()
        {
            Assert.AreEqual(2, Class1.Solve(1,1,1));
        }
        [TestCase(1,-3, 2, 2,1)]
        [TestCase(1, 1, 1, 0)]
        public void TestCases(int a, int b, int c, int[] expectedResult)
        {
            Assert.AreEqual(expectedResult, Class1.Solve(a, b, c));
        }
    }
}
 