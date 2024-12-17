using System;

namespace DistanceTask;

public static class DistanceTask
{
    public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
    {
        var acLenth = (x - ax) * (x - ax) + (y - ay) * (y - ay);
        var bcLenth = (x - bx) * (x - bx) + (y - by) * (y - by);
        var abLenth = (bx - ax) * (bx - ax) + (by - ay) * (by - ay);
        if (CompareDoubleNumbers(ax, ay) && CompareDoubleNumbers(ay, bx) && CompareDoubleNumbers(bx, by) &&
        CompareDoubleNumbers(by, x) && CompareDoubleNumbers(x, y)) return 0; 
        if (CompareDoubleNumbers(ax, bx) && CompareDoubleNumbers(ay, by)) return Math.Sqrt(acLenth);

        var partOfDistanceFormula = ((x - ax) * (bx - ax) + (y - ay) * (by - ay)) / 
                                        abLenth; 
        partOfDistanceFormula = Math.Max(0, Math.Min(partOfDistanceFormula, 1));
        var aFactor = (ax - x + (bx - ax) * partOfDistanceFormula) *
                         (ax - x + (bx - ax) * partOfDistanceFormula);
        var bFactor = ((ay - y + (by - ay) * partOfDistanceFormula) * 
                        (ay - y + (by - ay) * partOfDistanceFormula));
        return Math.Sqrt(aFactor + bFactor);
    }

    public static bool CompareDoubleNumbers(double firstNum, double secondNum)
    {
        bool temp = Math.Abs(firstNum - secondNum) < 0.0001 ? true: false;
        return temp;
    }  
}