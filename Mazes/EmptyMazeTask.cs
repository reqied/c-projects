using System.Runtime.CompilerServices;

namespace Mazes;

public static class EmptyMazeTask
{
	public static void MoveOut(Robot robot, int width, int height)
	{	
		MakeSomeStepsToDirection(robot, width - 3, Direction.Right);
		if (!robot.Finished)
		{
			MakeSomeStepsToDirection(robot, height - 3, Direction.Down);
		}
	}
	
	public static void MakeSomeStepsToDirection(Robot robot, int amountOfSteps, Direction direction)
	{
		for(var i = 0; i < amountOfSteps; i++)
		{
			robot.MoveTo(direction);
		}
	}
}