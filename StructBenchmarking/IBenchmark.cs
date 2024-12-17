using System;

namespace StructBenchmarking;

public partial interface IBenchmark
{
	double MeasureDurationInMs(ITask task, TimeSpan time);
}