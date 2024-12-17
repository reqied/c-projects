namespace StructBenchmarking;

public partial interface IBenchmark
{
	double MeasureDurationInMs(ITask task, int repetitionCount);
}