using System;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking;

public class Benchmark : IBenchmark
{
    public double MeasureDurationInMs(ITask task, int repetitionCount)
    {
        task.Run();
        GC.Collect();
        GC.WaitForPendingFinalizers();
        
        var stopwatch = Stopwatch.StartNew();
        for (var i = 0; i < repetitionCount; i++)
        {
            task.Run();
        }

        stopwatch.Stop();
        return (double) stopwatch.ElapsedMilliseconds / repetitionCount;
    }
}

    public class StringConstructorTask : ITask
    {
        private string Str;
        private int Count { get; set; }
        public void Run()
        {
            Str = new string('a', Count);
        }
    }

    public class StringBuilderTask : ITask
    {
        private string Str;
        private int Count{get; set; }
        public void Run()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < Count; i++)
            {
                sb.Append('a');
            }
            Str = sb.ToString();
        }
    }

[TestFixture]
public class RealBenchmarkUsageSample
{
    [Test]
    public void StringConstructorFasterThanStringBuilder()
    {
        var benchmark = new Benchmark();
        var stringConstructorTask = new StringConstructorTask();
        var stringBuilderTask = new StringBuilderTask();
        const int repetitionCount = 10000;
        var stringConstructorDuration = benchmark.MeasureDurationInMs(stringConstructorTask, repetitionCount);
        var stringBuilderDuration = benchmark.MeasureDurationInMs(stringBuilderTask, repetitionCount);
        Assert.Less(stringConstructorDuration, stringBuilderDuration);
    }
}