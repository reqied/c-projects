namespace Recognizer;

public static class GrayscaleTask
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
}
