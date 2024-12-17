namespace Mazes;

public static class PyramidMazeTask
{
	public static void MoveOut(Robot robot, int width, int height)
	{
		var counter = 0;
		var stepCount = width - 2;
		for(var i = stepCount; i > 0; i = i - 2)
		{
			counter++;
			var currentDirection = counter % 2 != 0 ? Direction.Right : Direction.Left;
			MoveWhilePossible(robot, i - 1, currentDirection);

            if (robot.Finished)
            {
                break;
            }
            MoveWhilePossible(robot, 2, Direction.Up);
        }
	}

	public static void MoveWhilePossible(Robot robot, int steps, Direction direction)
	{
		for(var i = 0; i < steps; i++)
		{
			if (!robot.Finished)
			{
				robot.MoveTo(direction);
			}
		}
	}
}
