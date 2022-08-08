using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
	{
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            GC.Collect();                   
            GC.WaitForPendingFinalizers();

            task.Run();
            var watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < repetitionCount; i++)
                task.Run();
            watch.Stop();
            return watch.Elapsed.TotalMilliseconds / repetitionCount;
        }

        
	}

    public class StringBuilderTask : ITask
    {
        public void Run()
        {
            var str = new StringBuilder();
            for (int i = 0; i < 10000; i++)
                str.Append('а');
            str.ToString();
        }
    }

    public class StringTask : ITask
    {
        public void Run()
        {
            var str = new string('a', 10000);
        }
    }

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            var test = new Benchmark();

            var stringTime = test.MeasureDurationInMs(new StringTask(), 100000);
            var stringBuilderTime = test.MeasureDurationInMs(new StringBuilderTask(), 100000);

            Assert.Less(stringTime, stringBuilderTime);
        }
    }
}