using System;
using System.Collections.Generic;
using System.Linq;


namespace Names;

internal static class PieChartTask
{
    public static PieChartData GetTopFivePopularNameInYear(NameData[] names, string year)
    {
        var titleOfEachPie = new string[5];
        var amountOfSmth = new double[5];
        var nameToAmount = new Dictionary<string, int>();
        foreach(var name in names)
        {
            if (name.BirthDate.Year == int.Parse(year))
            {
                if (!nameToAmount.TryAdd(name.Name, 1))
                {
                    nameToAmount[name.Name]++;
                }           
            }
        }
        var res = nameToAmount.OrderByDescending(pair => pair.Value);


        var i = 0;
        foreach (var (key, value) in res)
        {
            titleOfEachPie[i] = key;
            amountOfSmth[i] = value;
            i++;
            if(i == 5)
            {
                break;
            }
        }

        return new PieChartData(
            $"Топ 5 по популярности имен в {year} году ", 
            titleOfEachPie, 
            amountOfSmth);
    } 
}