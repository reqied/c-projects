using SkiaSharp;

namespace Mazes;

public static class SnakeMazeTask
{
    public static void MoveOut(Robot robot, int width, int height)
    {
        while (!robot.Finished)
        {
            MoveOnSomeStepsToSomeDirection(robot, width - 3, Direction.Right);
            MoveOnSomeStepsToSomeDirection(robot, 2, Direction.Down);
            MoveOnSomeStepsToSomeDirection(robot, width - 3, Direction.Left);
            if (robot.Finished)
            {
                break;
            }
            MoveOnSomeStepsToSomeDirection(robot, 2, Direction.Down);
        }
    }

    public static void MoveOnSomeStepsToSomeDirection(Robot robot, int amountOfSteps, Direction direction)
    {
        for (var i = 0; i < amountOfSteps; i++)
        {
            robot.MoveTo(direction);
        }
    }
}
