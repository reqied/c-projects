using System.Drawing;

namespace Benchmarks{}


public class GrayscaleTask
{

    public const double RCoefficient = 0.299;
    public const double GCoefficient = 0.587;
    public const double BCoefficient = 0.114;

    public static double[,] ToGrayscale(Pixel[,] original)
    {
        var width = original.GetLength(0);
        var height = original.GetLength(1);
        var bright = new double[width, height];
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                var pixelArray = original[i, j];
                bright[i, j] = (RCoefficient * pixelArray.R +
                                GCoefficient * pixelArray.G +
                                BCoefficient * pixelArray.B) / 255;
            }
        }

        return bright;
    }

    public static double[,] ToGrayscale_GetLengthBeforIterations(Pixel[,] image)
    {
        var width = image.GetLength(0);
        var height = image.GetLength(1);
        var bright = new double[width, height];
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                var pixelArray = image[i, j];
                bright[i, j] = (RCoefficient * pixelArray.R +
                                GCoefficient * pixelArray.G +
                                BCoefficient * pixelArray.B) / 255;
            }
        }

        return bright;
    }

    public static double[,] ToGrayscale_GetLengthEachIterations(Pixel[,] image)
    {
        var width = image.GetLength(0);
        var height = image.GetLength(1);
        var bright = new double[width, height];
        for (var i = 0; i < image.GetLength(0); i++)
        {
            for (var j = 0; j < image.GetLength(1); j++)
            {
                var pixelArray = image[i, j];
                bright[i, j] = (RCoefficient * pixelArray.R +
                                GCoefficient * pixelArray.G +
                                BCoefficient * pixelArray.B) / 255;
            }
        }
        return bright;
    }

    public static double[,] ToGrayscale_GetLengthWithoutFormulaMethod(Pixel[,] image)
    {
        var width = image.GetLength(0);
        var height = image.GetLength(1);
        var bright = new double[width, height];
        for (var i = 0; i < image.GetLength(0); i++)
        {
            for (var j = 0; j < image.GetLength(1); j++)
            {
                var pixelArray = image[i, j];
                bright[i, j] = (RCoefficient * pixelArray.R +
                                GCoefficient * pixelArray.G +
                                BCoefficient * pixelArray.B) / 255;
            }
        }
        return bright;
    }

    public static double[,] ToGrayscale_GetLengthWithFormulaMethod(Pixel[,] image)
    {
        var width = image.GetLength(0);
        var height = image.GetLength(1);
        var bright = new double[width, height];
        for (var i = 0; i < image.GetLength(0); i++)
        {
            for (var j = 0; j < image.GetLength(1); j++)
            {
                var pixelArray = image[i, j];
                bright[i, j] = Formula(pixelArray);
            }
        }
        return bright;
    }

    private static double Formula(Pixel pixelArray)
    {
        return (RCoefficient * pixelArray.R +
                GCoefficient * pixelArray.G +
                BCoefficient * pixelArray.B) / 255;
    }
}
public class Pixel
{
    public Pixel(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }

    public Pixel(Color color)
    {
        R = color.R;
        G = color.G;
        B = color.B;
    }

    public byte R { get; }
    public byte G { get; }
    public byte B { get; }

    public override string ToString()
    {
        return $"Pixel({R}, {G}, {B})";
    }
}
