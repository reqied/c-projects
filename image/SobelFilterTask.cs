using System;

namespace Recognizer;
internal static class  SobelFilterTask
{
    public static double[,] SobelFilter(double[,] image, double[,] matrix)
    {
        var width = image.GetLength(0);
        var height = image.GetLength(1);
        var matrixLength = matrix.GetLength(0);
        
        var result = new double[width, height];
        var centerOfImage = (int)Math.Floor(matrixLength / 2.0);
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
                if (x >= centerOfImage && x < width - centerOfImage && y >= centerOfImage && y < height - centerOfImage)
                {
                    result[x, y] = GetGradientForSobel(image, matrix, x, y, centerOfImage);
                }
                else
                {
                    result[x, y] = 0;
                }
        }
        return result;
    }
      
    private static double GetGradientForSobel(double[,] image, double[,] matrix, int x, int y, int centerOfImage)
    {
        var xGradient = 0.0;
        var yGradient = 0.0;
        var length = matrix.GetLength(0);
        for (var i = 0; i < length; i++)
        {
            for (var j = 0; j < length; j++)
            {
                xGradient += image[x - centerOfImage + i, y - centerOfImage + j] * matrix[i, j];
                yGradient += image[x - centerOfImage + i, y - centerOfImage + j] * matrix[j, i];
            }
        }
        return Math.Sqrt(xGradient * xGradient + yGradient * yGradient);
    }
}