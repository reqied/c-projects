using System;
using Avalonia;
using NUnit.Framework;
using static Manipulation.Manipulator;

namespace Manipulation;

public static class AnglesToCoordinatesTask
{
	public static Point[] GetJointPositions(double shoulder, double elbow, double wrist)
	{
		var wristAngle = elbow - Math.PI + shoulder;
		var palmAngle = wrist - Math.PI + wristAngle;

		var elbowPos = UpperArm * new Point(Math.Cos(shoulder), Math.Sin(shoulder));
		var wristPos = elbowPos + Forearm * new Point(Math.Cos(wristAngle), Math.Sin(wristAngle));
		var palmEndPos = wristPos + Palm * new Point(Math.Cos(palmAngle), Math.Sin(palmAngle));

		return new[]
		{
			elbowPos,
			wristPos,
			palmEndPos
		};
	}
}

[TestFixture]
public class AnglesToCoordinatesTask_Tests
{
	[TestCase(0, 0, 0, UpperArm - Forearm + Palm, 0)]
	[TestCase(Math.PI * 2, Math.PI * 2, Math.PI * 2, UpperArm - Forearm + Palm, 0)]
	[TestCase(-Math.PI, Math.PI, Math.PI, -(UpperArm + Forearm + Palm), 0)]
	[TestCase(Math.PI / 2, -Math.PI / 2, Math.PI / 2, -Forearm, UpperArm  + Palm)]
	[TestCase(0, Math.PI / 2, Math.PI / 2, UpperArm - Palm, -Forearm)]
	[TestCase(Math.PI / 2, 0, 0, 0, UpperArm - Forearm + Palm)]
	[TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Forearm + Palm, UpperArm)]
	public void TestGetJointPositions(double shoulder, 
		double elbow, 
		double wrist, 
		double expectedPalmEndX, 
		double expectedPalmEndY)
	{
		var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);

		Assert.AreEqual(expectedPalmEndX, joints[2].X, 1e-5, "palm endX");
		Assert.AreEqual(expectedPalmEndY, joints[2].Y, 1e-5, "palm endY");

		Assert.AreEqual(UpperArm, Distance(new Point(0, 0), joints[0]), 1e-5, "Upper arm length");
		Assert.AreEqual(Forearm, Distance(joints[0], joints[1]), 1e-5, "Forearm length");
		Assert.AreEqual(Palm, Distance(joints[1], joints[2]), 1e-5, "Palm length");
	}

	private static double Distance(Point p1, Point p2)
	{
		return Point.Distance(p1, p2);
	}
}