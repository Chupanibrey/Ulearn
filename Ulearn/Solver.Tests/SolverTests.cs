using NUnit.Framework;

namespace Solver.Tests
{
    public class Test
    {
        [TestCase(1, -3, 2, 2, 1)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 1, -1)]
        public void TestEquation(double a, double b, double c, params double[] expectedResult)
        {
            var result = QuadraticEquationsSolver.Solve(a, b, c);

            Assert.AreEqual(expectedResult.Length, result.Length);

            for (int i = 0; i < result.Length; i++)
                Assert.AreEqual(expectedResult[i], result[i]);
        }
    }
}