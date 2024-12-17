namespace Names;

public class PieChartData
{
	public PieChartData(string title, string[] titleOfEachPie, double[] amountOfSmth)
	{
		TitleOfEachPie = titleOfEachPie;
		AmountOfSmth = amountOfSmth;
		Title = title;
	}

	public string[] TitleOfEachPie { get; }
	public double[] AmountOfSmth { get; }
	public string Title { get; }
}