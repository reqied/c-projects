namespace Mazes;

public static class DiagonalMazeTask
{
    public static void MoveOut(Robot robot, int width, int height)
    {
        var availableForWalkWidth = width - 2;
        var availableForWalkHeight = height - 2;
        if(height > width)
        {
            MoveTillTheEnd(robot, availableForWalkWidth, availableForWalkHeight, Direction.Down, Direction.Right);
        }
        else
        {
            MoveTillTheEnd(robot, availableForWalkHeight, availableForWalkWidth, Direction.Right, Direction.Down);
        }
    }

    public static void MoveToAvoidWall(
        Robot robot, 
        int availableForWalkWidth,
        int availableForWalkHeight,
        Direction direction1,
        Direction direction2)
    {
        for (var j = 0; j < availableForWalkHeight/availableForWalkWidth; j++)
        {
            robot.MoveTo(direction1);
        }

        if(!robot.Finished)
        {
            robot.MoveTo(direction2);
        }
    }

    public static void MoveTillTheEnd(
        Robot robot, 
        int availableForWalkWidth,
        int availableForWalkHeight,
        Direction direction1,
        Direction direction2)
    {
        for (var i = 0; i < availableForWalkWidth; i++)
        {
            MoveToAvoidWall(robot, availableForWalkWidth, availableForWalkHeight, direction1, direction2);
        }
    }
}