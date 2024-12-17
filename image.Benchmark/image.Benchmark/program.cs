using System.Drawing;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;

namespace Benchmarks;

public class GetLrngthBenchmark
{
    public static void Main(string[] args)
    {
        var config = ManualConfig.CreateMinimumViable()
            .AddDiagnoser(new MemoryDiagnoser(new MemoryDiagnoserConfig()));
        BenchmarkRunner.Run<Benchmarks>(config);
    }
    
}

public class Benchmarks
{
    private Pixel[,] image;
    [GlobalSetup]
    public void Setup()
    {
        image = new Pixel[512, 512];
        var rand = new Random();
        for (var i = 0; i < 512; i++)
        {
            for (var j = 0; j < 512; j++)
            {
                image[i, j] = new Pixel(Color.Aqua);
            }
        }
    }

    [Benchmark]
    public double[,] Tograyscale_GetLengthBeforeIterations() =>
        GrayscaleTask.ToGrayscale_GetLengthBeforIterations(image);
    
    [Benchmark]
    public double[,] Tograyscale_GetLengthEachIterations() =>
        GrayscaleTask.ToGrayscale_GetLengthEachIterations(image);
    
    [Benchmark]
    public double[,] Tograyscale_GetLengthWithoutFormulaMethod() =>
        GrayscaleTask.ToGrayscale_GetLengthWithoutFormulaMethod(image);
    
    [Benchmark]
    public double[,] Tograyscale_GetLengthWithFormulaMethod() =>
        GrayscaleTask.ToGrayscale_GetLengthWithFormulaMethod(image);
}