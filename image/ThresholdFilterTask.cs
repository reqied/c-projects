using System;
using System.Collections.Generic;

namespace Recognizer;

public static class ThresholdFilterTask
{
	private const double White = 1.0;
	private const double Black = 0.0;

	public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
	{
		var width = original.GetLength(0);
		var height = original.GetLength(1);
		var threshold = EvaluateThreshold(original, whitePixelsFraction, width, height);
		var blackAndWhiteOriginal = new double[width, height];
		for (var i = 0; i < width; i++)
		{
			for (var j = 0; j < height; j++)
			{
				blackAndWhiteOriginal[i, j] = original[i, j] >= threshold ? White : Black;
			}
		}
		return blackAndWhiteOriginal;
	}

	private static double EvaluateThreshold(double[,] original, double whitePixelsFraction, int width, int height)
	{
		var allPixelsInPicture = new List<double>();
		for (var i = 0; i < width; i++)
		{
			for (var j = 0; j < height; j++)
			{
				allPixelsInPicture.Add(original[i, j]);
			}
		}
		allPixelsInPicture.Sort();
		var amountOfWhite = (int)Math.Floor(whitePixelsFraction * (allPixelsInPicture.Count));
		var threshold = (amountOfWhite != 0) ? allPixelsInPicture[original.Length - amountOfWhite] : double.MaxValue;
		return threshold;
	}
}