using System;

namespace Names;

internal static class HistogramTask
{
    public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
    {
        var arrayDaysInMonth = new string[31];
        var arrayAmountOfPeopleInExactDay = new double[31];
        for (var i = 1; i <= arrayDaysInMonth.Length; i++)
        {
            arrayDaysInMonth[i - 1] = i.ToString();
        }
        for (var i = 1; i <= arrayAmountOfPeopleInExactDay.Length; i++)
        {
            var count = 0;
            for (var j = 0; j < names.Length; j++)
            {
                if (name == names[j].Name && names[j].BirthDate.Day == i && names[j].BirthDate.Day != 1)
                {
                    count++;
                }
            }
            arrayAmountOfPeopleInExactDay[i - 1] = count;
        }
        return new HistogramData(
            $"Рождаемость людей с именем '{name}'", 
            arrayDaysInMonth, 
            arrayAmountOfPeopleInExactDay);
    }
}