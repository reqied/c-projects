using System;
using NUnit.Framework;
using static Manipulation.Manipulator;

namespace Manipulation;

public static class ManipulatorTask
{
    public static double[] MoveManipulatorTo(double x, double y, double alpha)
    {
        var wristX = x + Palm * Math.Cos(Math.PI - alpha);
        var wristY = y + Palm * Math.Sin(Math.PI - alpha);
        var distanceToWrist = Math.Sqrt(wristX * wristX + wristY * wristY);
        if (distanceToWrist > UpperArm + Forearm || distanceToWrist < Math.Abs(UpperArm - Forearm))
        {
            return new[] { double.NaN, double.NaN, double.NaN };
        }
            
        var elbow = TriangleTask.GetABAngle(UpperArm, Forearm, distanceToWrist);
        var shoulderToWristAngle = Math.Atan2(wristY, wristX);
        var shoulder = TriangleTask.GetABAngle(UpperArm, distanceToWrist, Forearm) + shoulderToWristAngle;
        var wrist = -alpha - shoulder - elbow;

        return new[] { shoulder, elbow, wrist };
    }
}

[TestFixture]
public class ManipulatorTask_Tests
{
    public const int AmountOfTests = 1000;
    public const int TestSeed = 1000;
    public const double Distance = UpperArm + Forearm + Palm;
    public const int N = 1000;

    // Проверка достижимости точки
    private bool IsReachable(double x, double y)
    {
        var distanceToTarget = Math.Sqrt(x * x + y * y);
        var minReach = Math.Abs(UpperArm - Forearm - Palm);
        var maxReach = UpperArm + Forearm + Palm;
        return distanceToTarget >= minReach && distanceToTarget <= maxReach;
    }

    [Repeat(N)]
    public void TestMoveManipulatorTo()
    {
        var rng = new Random(TestSeed);
        var x = rng.NextDouble() * 2 * Distance - Distance;
        var y = rng.NextDouble() * 2 * Distance - Distance;
        var a = rng.NextDouble() * 2 * Math.PI;

        var angles = ManipulatorTask.MoveManipulatorTo(x, y, a);
        Assert.AreEqual(3, angles.Length, 
            "ManipulatorTask.MoveManipulatorTo did not return an array of 3 angles.");

        if (double.IsNaN(angles[0]))
        {
            Assert.IsFalse(IsReachable(x, y),
                $"MoveManipulatorTo returned NaN, but the point ({x}, {y}) is reachable.");
            return;
        }

        var joints = AnglesToCoordinatesTask.GetJointPositions(
            angles[0], angles[1], angles[2]);
        Assert.AreEqual(3, joints.Length,
            "AnglesToCoordinatesTask.GetJointPositions did not return 3 joint positions.");
        Assert.AreEqual(joints[2].X, x, 0.001, 
            $"X coordinate mismatch for point ({x}, {y}).");
        Assert.AreEqual(joints[2].Y, y, 0.001, 
            $"Y coordinate mismatch for point ({x}, {y}).");
    }
}



