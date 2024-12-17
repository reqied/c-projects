using System;
using System.Collections.Generic;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;

namespace Names.UI;

internal static class Charts
{
	public static void ShowHistogram(HistogramData stats, string title)
	{
		var model = new PlotModel { Title = stats.Title };
		model.Legends.Add(new Legend
		{
			LegendPlacement = LegendPlacement.Outside, LegendPosition = LegendPosition.RightTop,
			LegendOrientation = LegendOrientation.Vertical
		});

		model.Axes.Add(new CategoryAxis { Position = AxisPosition.Left, ItemsSource = stats.XLabels });
		model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, AbsoluteMinimum = 0 });

		model.Series.Add(new BarSeries { ItemsSource = stats.YValues.Select(v => new BarItem(v)).ToList() });

		MainWindow.Tabs.Add(new TabItemModel(title, model));
		MainWindow.w.InitializeComponent();
	}

	public static void ShowHeatmap(HeatmapData stats, string title)
	{
		var model = new PlotModel { Title = stats.Title };

		var x = new CategoryAxis
			{ Position = AxisPosition.Bottom, Key = "XLabels", ItemsSource = stats.XLabels };
		var y = new CategoryAxis
			{ Position = AxisPosition.Left, Key = "YLabels", ItemsSource = stats.YLabels.Reverse() };
		model.Axes.Add(x);
		model.Axes.Add(y);

		var barSeriesManager = new BarSeriesManager(x, x, new[] { new BarSeries() });
		barSeriesManager.Update();
		barSeriesManager = new BarSeriesManager(y, y, new[] { new BarSeries() });
		barSeriesManager.Update();

		var heatMapSeries = new HeatMapSeries
		{
			X0 = 0,
			X1 = stats.XLabels.Length - 1,
			Y1 = 0,
			Y0 = stats.YLabels.Length - 1,
			XAxisKey = "XLabels",
			YAxisKey = "YLabels",
			RenderMethod = HeatMapRenderMethod.Rectangles,
			LabelFontSize = 0.2,
			Data = stats.Heat,
		};
		var values = stats.Heat.Cast<double>().ToList();
		var avgHeat = values.Average();
		var sigma = Math.Sqrt(values.Average(x => (x - avgHeat) * (x - avgHeat)));

		model.Axes.Add(new CustomColorAxis
		{
			Position = AxisPosition.Right,
			GetColorFunc = value =>
			{
				var sigmaValue = (value - avgHeat) / sigma;
				var colorValue = (byte)Math.Min(255, (int)(200 * Math.Abs(sigmaValue)));
				var color = sigmaValue >= 0
					? OxyColor.FromArgb(255, (byte)(255 - colorValue), 255, (byte)(255 - colorValue))
					: OxyColor.FromArgb(255, 255, (byte)(255 - colorValue), (byte)(255 - colorValue));
				return color;
			},
		});

		model.Series.Add(heatMapSeries);

		MainWindow.Tabs.Add(new TabItemModel(title, model));
		MainWindow.w.InitializeComponent();
	}

	public static void ShowPieChart(PieChartData stats, string title)
	{
		var modelP1 = new PlotModel { Title = stats.Title};

		dynamic seriesP1 = new PieSeries { StrokeThickness = 2.0, InsideLabelPosition = 0.8, AngleSpan = 360, StartAngle = 0 };
		var redColors = new OxyColor[5] {OxyColors.Firebrick, OxyColors.IndianRed, OxyColors.Salmon, OxyColors.MistyRose, OxyColors.WhiteSmoke};
		var greenColors = new OxyColor[5] {OxyColors.DarkGreen, OxyColors.ForestGreen, OxyColors.MediumSeaGreen, OxyColors.LightGreen, OxyColors.WhiteSmoke};
		var colorNeeded = stats.AmountOfSmth[0] > 300 ? redColors : greenColors;
		seriesP1.Slices.Add(new PieSlice(stats.TitleOfEachPie[0], stats.AmountOfSmth[0]) { IsExploded = false, Fill = colorNeeded[0] });
		seriesP1.Slices.Add(new PieSlice(stats.TitleOfEachPie[1], stats.AmountOfSmth[1]) { IsExploded = false, Fill = colorNeeded[1] });
		seriesP1.Slices.Add(new PieSlice(stats.TitleOfEachPie[2], stats.AmountOfSmth[2]) { IsExploded = true, Fill = colorNeeded[2] });
		seriesP1.Slices.Add(new PieSlice(stats.TitleOfEachPie[3], stats.AmountOfSmth[3]) { IsExploded = true, Fill = colorNeeded[3] });
		seriesP1.Slices.Add(new PieSlice(stats.TitleOfEachPie[4], stats.AmountOfSmth[4]) { IsExploded = true, Fill = colorNeeded[4] });

		modelP1.Series.Add(seriesP1);
		MainWindow.Tabs.Add(new TabItemModel(title, modelP1));
		MainWindow.w.InitializeComponent();
	}
}