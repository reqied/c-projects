using System;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking;

public class Benchmark : IBenchmark
{
    public double MeasureDurationInMs(ITask task, TimeSpan time)
    {
        task.Run();
        GC.Collect();
        GC.WaitForPendingFinalizers();

        var counter = 0;
        var stopwatch = Stopwatch.StartNew();
        while (time >= stopwatch.Elapsed)
        {
            task.Run();
            counter++;
        }

        stopwatch.Stop();
        return (double) stopwatch.ElapsedMilliseconds / counter;
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
        var time = new TimeSpan(5);
        var stringConstructorDuration = benchmark.MeasureDurationInMs(stringConstructorTask, time);
        var stringBuilderDuration = benchmark.MeasureDurationInMs(stringBuilderTask, time);
        Assert.Less(stringConstructorDuration, stringBuilderDuration);
    }
}