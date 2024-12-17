using System;
using System.Diagnostics;
using NUnit.Framework;

namespace StructBenchmarking;

[TestFixture]
public class BenchmarkTests
{
	public class MockTask : ITask
	{
		private readonly double firstCallDelay;
		private readonly double delay;
		public int CallsCount { get; private set; }

		public MockTask(double firstCallDelay, double delay)
		{
			this.firstCallDelay = firstCallDelay;
			this.delay = delay;
		}

		public void Run()
		{
			var realDelay = CallsCount == 0 ? firstCallDelay : delay;
			CallsCount++;
			var sw = Stopwatch.StartNew();
			while (true)
				if (sw.ElapsedMilliseconds >= realDelay)
					return;
		}
	}

	[Test]
	public void HasExtraWarmingCall([Values(10, 20, 30)] TimeSpan time)
	{
		var runner = new MockTask(firstCallDelay: 0, delay: 0);
		var benchmark = new Benchmark();
		benchmark.MeasureDurationInMs(runner, time);
		Assert.AreEqual(time.Add(new TimeSpan(1)), runner.CallsCount);
	}

	[TestCase(1, 10)]
	[TestCase(5, 20)]
	public void MeasureTimeExceptWarmingCall(TimeSpan time, int delay)
	{
		var runner = new MockTask(firstCallDelay: 10 * delay, delay: delay);
		var benchmark = new Benchmark();
		var measure = benchmark.MeasureDurationInMs(runner, time);
		Assert.AreEqual(delay, measure, delay / 4.0);
	}
}