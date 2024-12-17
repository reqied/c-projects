using System.Collections.Generic;

namespace StructBenchmarking;

public interface ITaskFactory
{
    ITask CreateClassTask(int fieldCount);
    ITask CreateStructTask(int fieldCount);
    string GetTitle();
}


public class ArrayCreationTaskFactory : ITaskFactory
{
    public ITask CreateClassTask(int fieldCount) => new ClassArrayCreationTask(fieldCount);
    public ITask CreateStructTask(int fieldCount) => new StructArrayCreationTask(fieldCount);
    public string GetTitle() => "Create array";
}

public class MethodCallTaskFactory : ITaskFactory
{
    public ITask CreateClassTask(int fieldCount) => new MethodCallWithClassArgumentTask(fieldCount);
    public ITask CreateStructTask(int fieldCount) => new MethodCallWithStructArgumentTask(fieldCount);
    public string GetTitle() => "Call method with argument";
}

public class Experiments
{
    private static ChartData BuildChartData(
        IBenchmark benchmark, int repetitionsCount, ITaskFactory taskFactory)
    {
        var classesTimes = new List<ExperimentResult>();
        var structuresTimes = new List<ExperimentResult>();

        foreach (var fieldCount in Constants.FieldCounts)
        {
            var classTask = taskFactory.CreateClassTask(fieldCount);
            var structTask = taskFactory.CreateStructTask(fieldCount);
            var classTime = benchmark.MeasureDurationInMs(classTask, repetitionsCount);
            var structTime = benchmark.MeasureDurationInMs(structTask, repetitionsCount);
            classesTimes.Add(new ExperimentResult(fieldCount, classTime));
            structuresTimes.Add(new ExperimentResult(fieldCount, structTime));
        }

        return new ChartData
        {
            Title = taskFactory.GetTitle(),
            ClassPoints = classesTimes,
            StructPoints = structuresTimes,
        };
    }

    public static ChartData BuildChartDataForArrayCreation(
        IBenchmark benchmark, int repetitionsCount)
    {
        return BuildChartData(benchmark, repetitionsCount, new ArrayCreationTaskFactory());
    }

    public static ChartData BuildChartDataForMethodCall(
        IBenchmark benchmark, int repetitionsCount)
    {
        return BuildChartData(benchmark, repetitionsCount, new MethodCallTaskFactory());
    }
}
