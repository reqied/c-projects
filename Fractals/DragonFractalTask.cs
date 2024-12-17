using System;

namespace Fractals;

internal static class DragonFractalTask
{
    public static double Angle45 = Math.PI * 45 / 180;
    public static double Angle135 = Math.PI * 135 / 180;

    public static (double X, double Y) DrawFractalWithAngle45(double x, double y)
    {
        return ((x * Math.Cos(Angle45) - y * Math.Sin(Angle45)) / Math.Sqrt(2),
        (x * Math.Sin(Angle45) + y * Math.Cos(Angle45)) / Math.Sqrt(2));
    }
    
    public static (double X, double Y) DrawFractalWithAngle135(double x, double y)
    {
        return((x * Math.Cos(Angle135) - y * Math.Sin(Angle135)) / Math.Sqrt(2) + 1,
        (x * Math.Sin(Angle135) + y * Math.Cos(Angle135)) / Math.Sqrt(2));
    }
    
    public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
    {
        var points = (1.0, 0.0);
        var random = new Random(seed);
        for (int i = 0; i < iterationsCount; i++)
        {
            var nextNumber = random.Next(2);
            points = nextNumber == 1 ? DrawFractalWithAngle45(points.Item1, points.Item2)
            : DrawFractalWithAngle135(points.Item1, points.Item2);
            pixels.SetPixel(points.Item1, points.Item2);
        }
    }
}