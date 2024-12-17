using System;
using System.Collections.Generic;
using System.Drawing;

namespace RoutePlanning;

public static class PathFinderTask
{
    public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
    {
        var bestOrder = new List<int>(){0};
        return ChooseClosestPoint(checkpoints, bestOrder).ToArray();
    }

    public static List<int> ChooseClosestPoint(Point[] checkpoints, List<int> bestOrder)
    {
        for (var j = 0; j < checkpoints.Length - 1; j ++)
        {
            var minWay = double.MaxValue;
            var next = -1;
            for (var i = 1; i < checkpoints.Length; i++)
            {
                var indexOfThisPoint = Array.IndexOf(bestOrder.ToArray(), i, 0, bestOrder.Count);
                if (indexOfThisPoint > 0) continue;
                var wayLength = checkpoints[bestOrder[j]].DistanceTo(checkpoints[i]);
                if (wayLength > minWay) continue;
                minWay = wayLength;
                next = i;
            }
            bestOrder.Add(next);
        }
        return bestOrder;
    }
}