using System;

namespace Names;

internal static class HeatmapTask
{
    public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
    {
        var arrayDaysInMonth = new string[30];
        var arrayAmountOfMonth = new string[12];
        var heat = new double[arrayDaysInMonth.Length, arrayAmountOfMonth.Length];
        arrayDaysInMonth = AddDatesInArray(arrayDaysInMonth, 2, arrayDaysInMonth.Length + 2);
        arrayAmountOfMonth = AddDatesInArray(arrayAmountOfMonth, 1, arrayAmountOfMonth.Length + 1);
        foreach (var name in names)
        {
            if (name.BirthDate.Day != 1)
            {
                heat[name.BirthDate.Day - 2, name.BirthDate.Month - 1]++;
            }
        }
        return new HeatmapData(
            "Пример карты интенсивностей",
            heat, 
            arrayDaysInMonth, 
            arrayAmountOfMonth);
    }
    public static string[] AddDatesInArray(string[] array, int start, int end)
    {
        for (var i = start; i < end; i++)
        {
            array[i - start] = i.ToString();
        }
        return array;
    }
}