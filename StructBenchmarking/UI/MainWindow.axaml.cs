using System;
using System.Collections.Generic;
using Avalonia.Controls;

namespace StructBenchmarking.UI;

public partial class MainWindow : Window
{
	public static List<TabItemModel> Tabs = new();

	public MainWindow()
	{
		DataContext = Tabs;
		var chartBuilder = new ChartBuilder();
		var time = new TimeSpan(5);
		var bigTime = new TimeSpan(50);
		var arraysData = Experiments.BuildChartDataForArrayCreation(new Benchmark(), time);
		var callsData = Experiments.BuildChartDataForMethodCall(new Benchmark(), bigTime);
		var arraysChart = chartBuilder.CreateTimeOfObjectSizeChart(arraysData);
		var callsChart = chartBuilder.CreateTimeOfObjectSizeChart(callsData);
		Tabs.Add(new TabItemModel(arraysChart.Title, arraysChart));
		Tabs.Add(new TabItemModel(callsChart.Title, callsChart));
		
		InitializeComponent();
	}
}