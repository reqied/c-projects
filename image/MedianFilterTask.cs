using System;
using System.Collections.Generic;

namespace Recognizer;

internal static class MedianFilterTask
{ 
	public static double[,] MedianFilter(double[,] original)
	{
		var width = original.GetLength(0);
		var height = original.GetLength(1);
		var median = new double[width, height];

		for (var i = 0; i < width; i++)
		{
			for (var j = 0; j < height; j++)
			{
				median[i, j] = EvaluateMedian(original, i, j);
			}
		}
		return median;
	}

	private static double EvaluateMedian(double[,] original, int x, int y)
	{
		var neighbours = FindNeighbours(original, x, y);
		var neighbourLength = neighbours.Count;
		if (neighbourLength % 2 == 0)
		{
			return (neighbours[neighbourLength / 2 - 1] + neighbours[neighbourLength / 2]) / 2;
		}
		return neighbours[(neighbourLength / 2)];
	}

	private static List<double> FindNeighbours(double[,] original, int x, int y)
	{
		var width = original.GetLength(0);
		var height = original.GetLength(1);
		var neighbours = new List<double>();
		for (var i = -1; i <= 1; i++)
		{
			for (var j = -1; j <= 1; j++)
			{
				if (x + i >= 0 && x + i < width && y + j >= 0 && y + j < height)
				{
					neighbours.Add(original[i + x, j + y]);
				}
			}
		}
		neighbours.Sort();
		return neighbours;
	}
}
