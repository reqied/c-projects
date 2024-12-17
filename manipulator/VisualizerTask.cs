using System;
using System.Globalization;
using Avalonia;
using Avalonia.Input;
using Avalonia.Media;


namespace Manipulation;

public static class VisualizerTask
{
	public static double SlowDownTheWheel = 0.2;
	public static double AngleDelta = 0.1;
	public static double X = 220;
	public static double Y = -100;
	public static double Alpha = 0.05;
	public static double Wrist = 2 * Math.PI / 3;
	public static double Elbow = 3 * Math.PI / 4;
	public static double Shoulder = Math.PI / 2;

	public static Brush UnreachableAreaBrush = new SolidColorBrush(Color.FromArgb(255, 255, 230, 230));
	public static Brush ReachableAreaBrush = new SolidColorBrush(Color.FromArgb(255, 230, 255, 230));
	public static Pen ManipulatorPen = new Pen(Brushes.Black, 3);
	public static Brush JointBrush = new SolidColorBrush(Colors.Gray);

	public static void KeyDown(Visual visual, KeyEventArgs e)
	{
		switch (e.Key)
		{
			case Key.Q:
				Shoulder += AngleDelta;
				break;
			case Key.A:
				Shoulder -= AngleDelta;
				break;
			case Key.W:
				Elbow += AngleDelta;
				break;
			case Key.S:
				Elbow -= AngleDelta;
				break;
		}
		
		Wrist = - Alpha - Shoulder - Elbow;
		visual.InvalidateVisual();
	}

	public static void MouseMove(Visual visual, PointerEventArgs e)
	{
		var windowPoint = new Point(e.GetPosition(visual).X, e.GetPosition(visual).Y);
		var shoulderPos = GetShoulderPos(visual);
		var mathPoint = ConvertWindowToMath(windowPoint, shoulderPos);
		
		X = mathPoint.X;
		Y = mathPoint.Y;

		UpdateManipulator();
		visual.InvalidateVisual();
	}

	public static void MouseWheel(Visual visual, PointerWheelEventArgs e)
	{
		var wheel = e.Delta.Y * SlowDownTheWheel;
		Alpha += wheel;
		UpdateManipulator();
		visual.InvalidateVisual();
	}

	public static void UpdateManipulator()
	{
		var allAngles = ManipulatorTask.MoveManipulatorTo(X, Y, Alpha);
		if (!double.IsNaN(allAngles[0]))
		{
			Shoulder = allAngles[0];
		}

		if (!double.IsNaN(allAngles[1]))
		{
			Elbow = allAngles[1];
		}

		if (!double.IsNaN(allAngles[2]))
		{
			Wrist = allAngles[2];
		}
	}

	public static void DrawManipulator(DrawingContext context, Point shoulderPos)
	{
		var joints = AnglesToCoordinatesTask.GetJointPositions(Shoulder, Elbow, Wrist);

		DrawReachableZone(context, ReachableAreaBrush, UnreachableAreaBrush, shoulderPos, joints);

		var formattedText = new FormattedText(
			$"X={X:0}, Y={Y:0}, Alpha={Alpha:0.00}",
			CultureInfo.InvariantCulture,
			FlowDirection.LeftToRight,
			Typeface.Default,
			18,
			Brushes.DarkRed
		)
		{
			TextAlignment = TextAlignment.Center
		};
		context.DrawText(formattedText, new Point(10, 10));
		ActualDraw(context, shoulderPos);
	}

	private static void ActualDraw(DrawingContext context, Point shoulderPos)
	{
		var positions = AnglesToCoordinatesTask.GetJointPositions(Shoulder, Elbow, Wrist);
		var elbowPos = ConvertMathToWindow(positions[0], shoulderPos);
		var wristPos = ConvertMathToWindow(positions[1], shoulderPos);
		var palmPos = ConvertMathToWindow(positions[2], shoulderPos);
		context.DrawLine(ManipulatorPen, shoulderPos, elbowPos);
		var radius = 3;
		context.DrawEllipse(JointBrush, null, elbowPos, radius, radius);
		context.DrawLine(ManipulatorPen, elbowPos, wristPos);
		context.DrawEllipse(JointBrush, null, wristPos, radius, radius);
		context.DrawLine(ManipulatorPen, wristPos, palmPos);
		context.DrawEllipse(JointBrush, null, palmPos, radius, radius);
	}

	private static void DrawReachableZone(
		DrawingContext context,
		Brush reachableBrush,
		Brush unreachableBrush,
		Point shoulderPos,
		Point[] joints)
	{
		var rmin = Math.Abs(Manipulator.UpperArm - Manipulator.Forearm);
		var rmax = Manipulator.UpperArm + Manipulator.Forearm;
		var mathCenter = new Point(joints[2].X - joints[1].X, joints[2].Y - joints[1].Y);
		var windowCenter = ConvertMathToWindow(mathCenter, shoulderPos);
		context.DrawEllipse(reachableBrush,
			null,
			new Point(windowCenter.X, windowCenter.Y),
			rmax, rmax);
		context.DrawEllipse(unreachableBrush,
			null,
			new Point(windowCenter.X, windowCenter.Y),
			rmin, rmin);
	}

	public static Point GetShoulderPos(Visual visual)
	{
		return new Point(visual.Bounds.Width / 2, visual.Bounds.Height / 2);
	}

	public static Point ConvertMathToWindow(Point mathPoint, Point shoulderPos)
	{
		return new Point(mathPoint.X + shoulderPos.X, shoulderPos.Y - mathPoint.Y);
	}

	public static Point ConvertWindowToMath(Point windowPoint, Point shoulderPos)
	{
		return new Point(windowPoint.X - shoulderPos.X, shoulderPos.Y - windowPoint.Y);
	}
}