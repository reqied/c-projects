using System;
using System.Collections.Generic; 
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Accord.Math;
using Accord.MachineLearning;
using Accord.Math.Decompositions;
using Accord.Statistics;

class Program
{
    static void Do()
    {
        var str =
            "Possible answers=ellipse,rectangle,triangle|(163,122) (217,136) (227,126) (140,123) (152,122) (214,125) (141,121) (141,126) (146,145) (176,123) (140,121) (151,164) (235,127) (188,149) (142,132) (140,125) (220,135) (144,140) (143,135) (209,140) (223,134) (180,153) (151,122) (144,139) (232,126) (147,151) (149,157) (149,156) (146,121) (226,126) (146,144) (197,124) (153,122) (140,122) (188,124) (142,130) (139,121) (198,145) (230,131) (216,137) (148,152) (148,153) (143,134) (237,127) (185,124) (175,123) (204,125) (151,165) (148,155) (143,121) (150,160) (209,125) (150,162) (207,141) (151,163) (162,161) (178,123) (170,123) (187,124) (162,122) (153,165) (154,122) (229,126) (176,155) (142,129) (182,124) (200,144) (157,122) (223,126) (143,136) (173,123) (156,122) (206,125) (145,141) (160,162) (179,123) (207,125) (206,141) (158,163) (184,151) (149,122) (156,164) (163,161) (165,122) (183,124) (234,129) (215,137) (181,152) (205,142) (184,124) (155,122) (146,146) (208,140) (147,121) (164,122) (171,123) (211,139) (173,156) (202,143) (190,148) (210,125) (174,123) (174,156) (221,126) (175,155) (166,123) (226,132) (191,148) (177,154) (145,143) (178,154) (198,124) (170,157) (182,152) (154,165) (159,122) (167,123) (183,152) (215,125) (147,148) (225,126) (199,125) (229,131) (150,159) (185,151) (186,124) (238,127) (212,139) (236,127) (233,129) (191,124) (144,137) (189,149) (190,124) (211,125) (187,150) (222,126) (232,130) (192,148) (147,149) (193,147) (205,125) (142,121) (144,121) (195,146) (227,132) (201,144) (141,127) (219,135) (148,154) (160,122) (208,125) (140,124) (192,124) (145,121) (201,125) (210,140) (202,125) (172,156) (203,125) (161,161) (169,123) (204,142) (141,128) (196,124) (168,123) (203,143) (142,131) (144,138) (199,144) (194,147) (214,138) (213,138) (213,125) (219,126) (152,165) (171,157) (221,135) (155,164) (147,150) (225,133) (165,160) (150,161) (218,136) (166,159) (216,126) (231,126) (143,133) (157,163) (218,126) (234,127) (212,125) (189,124) (236,128) (172,123) (186,150) (158,122) (177,123) (222,134) (161,122) (148,121) (168,158) (224,133) (220,126) (233,127) (169,158) (231,130) (179,153) (145,142) (235,128) (167,159) (200,125) (181,123) (230,126) (159,162) (217,126) (194,124) (180,123) (195,124) (224,126) (150,122) (196,146) (146,147) (228,126) (193,124) (149,158) (228,131) (197,145) (164,160)";
        Start(str);
    }

    static string Start(string str)
    {
            var points = GenerateCirclePoints(str);
    
            var shape = DetermineShape(points);
            Console.WriteLine(shape);
            return shape;
    }

    public static string DetermineShape(List<Point> points)
    {
        if (points.Count < 3)
            return "Недостаточно точек";

        var hull = ConvexHull(points);

        Console.WriteLine($"Точек на выпуклой оболочке: {hull.Count}");

        if (hull.Count == 3)
        {
            return "Треугольник";
        }
        if (hull.Count == 4 && IsRectangle(hull))
        {
            return "Прямоугольник";
        }
        if (IsEllipse(points))
        {
            return "Эллипс";
        }

        return "Неизвестная фигура";
    }

    private static List<Point> ConvexHull(List<Point> points)
    {
        var hull = new List<Point>();
        var start = points.Aggregate((p1, p2) => p1.X < p2.X ? p1 : p2);
        var current = start;

        do
        {
            hull.Add(current);
            var next = points[0];

            foreach (var p in points)
            {
                double cross = CrossProduct(current, next, p);
                if (cross > 0 || (cross == 0 && Distance(current, p) > Distance(current, next)))
                {
                    next = p;
                }
            }

            current = next;

        } while (current != start);

        return hull;
    }

    private static double CrossProduct(Point a, Point b, Point c)
    {
        return (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
    }

    private static double Distance(Point a, Point b)
    {
        return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
    }

    private static bool IsRectangle(List<Point> hull)
    {
        if (hull.Count != 4) return false;

        var lengths = new double[4];
        for (var i = 0; i < 4; i++)
        {
            lengths[i] = Distance(hull[i], hull[(i + 1) % 4]);
        }
        Console.WriteLine($"Стороны прямоугольника: {string.Join(", ", lengths)}");
        return Math.Abs(lengths[0] - lengths[2]) < 0.01 &&
               Math.Abs(lengths[1] - lengths[3]) < 0.01;
    }

    private static bool IsEllipse(List<Point> points)
    {
        var data = new double[points.Count, 2];
        for (var i = 0; i < points.Count; i++)
        {
            data[i, 0] = points[i].X;
            data[i, 1] = points[i].Y;
        }
        var covarianceMatrix = data.Transpose().Covariance();

        var eigenDecomposition = new EigenvalueDecomposition(covarianceMatrix);
        var eigenValues = eigenDecomposition.RealEigenvalues;

        Console.WriteLine(string.Join(", ", eigenValues));

        var ratio = eigenValues.Max() / eigenValues.Min();
        Console.WriteLine(ratio);
        return ratio > 1.2 && ratio < 3.0;
    }

    private static List<Point> GenerateCirclePoints(string str)
    {
        var split = str.Split('|');
        var splitChars = new char[]{'(', ')'};
        var stringPoints = split[1].Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
        var points = new List<Point>();
        for (var i = 0; i < stringPoints.Length; i++)
        {
            var xy = stringPoints[i].Split(',');
            if (xy.Length == 1)
            {
                continue;
                
            }
            var x = double.Parse(xy[0]);
            var y = double.Parse(xy[1]);
            points.Add(new Point(x, y));
        }
        return points;
    }
}

public class Point
{
    public double X { get; set; }
    public double Y { get; set; }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}
