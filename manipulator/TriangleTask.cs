using System;
using NUnit.Framework;

namespace Manipulation;

public class TriangleTask
{
    public static double GetABAngle(double a, double b, double c)
    {
        if (a <= 0 || b <= 0 || c <= 0 || a > b + c || b > a + c || c > a + b) 
        {
            return double.NaN;
        }
        var angle = Math.Acos((a * a + b * b - c * c) / (2 * a * b));
        
        return angle;
    }
}

[TestFixture]
public class TriangleTask_Tests
{
    [TestCase(0, 0, 0, double.NaN)]
    [TestCase(1, 0, 0, double.NaN)]
    [TestCase(0, 1, 0, double.NaN)]
    [TestCase(0, 0, 1, double.NaN)]
    [TestCase(1, 1, 2, Math.PI)]
    [TestCase(5, 5, 10, Math.PI)]
    [TestCase(3, 6, 9, Math.PI)]
    [TestCase(3, 4, 5, Math.PI / 2)]
    [TestCase(1, 1, 1, Math.PI / 3)]
    [TestCase(7, 24, 25, Math.PI / 2)] 
    [TestCase(8, 15, 17, Math.PI / 2)] 
    public void TestGetABAngle(double a, double b, double c, double expectedAngle)
    {
        var actualAngle = TriangleTask.GetABAngle(a, b, c);
        Assert.AreEqual(expectedAngle, actualAngle, 1e-5, "AB angle");
    }
}