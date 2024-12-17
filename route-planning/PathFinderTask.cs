using System;
using System.Drawing;

namespace RoutePlanning;

public static class PathFinderTask
{
    public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
    {
        var bestOrder = MakeTrivialPermutation(checkpoints.Length);
        var bestPath = new int[checkpoints.Length];
        var bestPathLength = double.MaxValue;

        FindAllPaths(bestOrder, 1, checkpoints, new int[checkpoints.Length], 0, bestPath,
            bestPathLength);
        return bestPath;
    }

    public static double FindAllPaths(int[] bestOrder,
        int position,
        Point[] checkpoints,
        int[] currentPath,
        double currentLength,
        int[] bestPath,
        double bestPathLength)
    {
        if (position == bestOrder.Length)
        {
            return BestPathLength(currentPath, currentLength, bestPath, bestPathLength);
        }
        for (var i = 0; i < bestOrder.Length; i++)
        {
            if (Array.IndexOf(currentPath, i, 0, position) != -1)
            {
                continue;
            }
            currentPath[position] = i;
            var newDistance = position > 0
                ? checkpoints[currentPath[position - 1]].DistanceTo(checkpoints[currentPath[position]])
                : 0;
            var newPathLength = currentLength + newDistance - 1;
            if (newPathLength < bestPathLength)
                bestPathLength = FindAllPaths(bestOrder, position + 1, checkpoints,
                    currentPath, newPathLength, bestPath, bestPathLength);
            currentPath[position] = -1;
        }
        return bestPathLength;
    }

    private static double BestPathLength(int[] currentPath, double currentLength, int[] bestPath, double bestPathLength)
    {
        if (!(currentLength < bestPathLength)) return bestPathLength;
        bestPathLength = currentLength;
        Array.Copy(currentPath, bestPath, currentPath.Length);
        return bestPathLength;
    }

    private static int[] MakeTrivialPermutation(int size)
    {
        var bestOrder = new int[size];
        for (var i = 0; i < bestOrder.Length; i++)
            bestOrder[i] = i;
        return bestOrder;
    }
}