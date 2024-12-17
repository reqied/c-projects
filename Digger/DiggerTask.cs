using System;
using Avalonia.Input;
using Digger.Architecture;

namespace Digger;

public class Terrain : ICreature
{
    public string GetImageFileName() => "Terrain.png";

    public int GetDrawingPriority() => 5;

    public CreatureCommand Act(int x, int y) => new() { DeltaX = 0, DeltaY = 0 };

    public bool DeadInConflict(ICreature? conflictedObject) => conflictedObject is Player;
}

public class Player : ICreature
{
    public string GetImageFileName() => "Player.png";

    public int GetDrawingPriority() => -1;

    public CreatureCommand Act(int x, int y)
    {
        var deltaX = 0;
        var deltaY = 0;
        var deltas = ChooseDeltas();
        deltaX = deltas[0];
        deltaY = deltas[1];
        var targetX = x + deltaX;
        var targetY = y + deltaY;
        if (targetX < 0 || targetX >= Game.MapWidth || targetY < 0 || targetY >= Game.MapHeight)
        {
            return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        }

        var targetCell = Game.Map[targetX, targetY];
        return targetCell is Sack 
            ? new CreatureCommand { DeltaX = 0, DeltaY = 0 } 
            : new CreatureCommand { DeltaX = deltaX, DeltaY = deltaY };
    }

    private static int[] ChooseDeltas()
    {
        var deltas = new int[2];
        switch (Game.KeyPressed)
        {
            case Key.Up:
                deltas[0] = 0;
                deltas[1] = -1;
                break;
            case Key.Down:
                deltas[0] = 0;
                deltas[1] = 1;
                break;
            case Key.Right:
                deltas[0] = 1;
                deltas[1] = 0;
                break;
            case Key.Left:
                deltas[0] = -1;
                deltas[1] = 0;
                break;
        }
        return deltas;
    }

    public bool DeadInConflict(ICreature? conflictedObject) => conflictedObject is Monster or Sack;
}



public class Sack : ICreature
{
    public int FallDistance;
    public string GetImageFileName() => "Sack.png";

    public int GetDrawingPriority() => 4;

    public CreatureCommand Act(int x, int y)
    {
        if (y + 1 < Game.MapHeight)
        {
            var belowObject = Game.Map[x, y + 1];

            if (ActWithBelowObject(x, y, belowObject, out var act))
                return act;
        }

        if (FallDistance > 1)
        {
            TransformToGold(x, y);
            return CreateGoldCommand();
        }

        FallDistance = 0;
        return new CreatureCommand {DeltaX = 0, DeltaY = 0};
    }

    private static void TransformToGold(int x, int y)
    {
        Game.Map[x, y] = new Gold();
    }

    private static CreatureCommand CreateGoldCommand()
    {
        return new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
    }

    private bool ActWithBelowObject(int x, int y, ICreature? belowObject, out CreatureCommand? act)
    {
        switch (belowObject)
        {
            case null:
            case Player or Monster when FallDistance > 0:
                Game.Map[x, y + 1] = null;
                FallDistance++;
                act = new CreatureCommand { DeltaX = 0, DeltaY = 1 };
                return true;
        }
        act = null;
        return false;
    }

    public bool DeadInConflict(ICreature? conflictedObject)
    {
        return conflictedObject switch
        {
            Player or Monster => true,
            _ => false
        };
    }
}

public class Gold : ICreature
{
    public string GetImageFileName() => "Gold.png";

    public int GetDrawingPriority() => 1;

    public CreatureCommand Act(int x, int y) => new() {DeltaX = 0, DeltaY = 0};

    public bool DeadInConflict(ICreature? conflictedObject)
    {
        switch (conflictedObject)
        {
            case Player:
                Game.Scores += 10;
                return true;
            case Monster:
                return conflictedObject is Monster;
            default:
                return false;
        }
    }
}

public class Monster : ICreature
{
    public string GetImageFileName() => "Monster.png";

    public int GetDrawingPriority() => 2;

    public CreatureCommand Act(int x, int y)
    {
        var diggerCommand = new CreatureCommand
        {
            DeltaX = 0,
            DeltaY = 0,
            TransformTo = this
        };
        var directions = new (int dx, int dy, int x1, int y1, int x2, int y2)[]
        {
            (-1, 0, 0, 0, x, Game.MapHeight),
            (1, 0, x + 1, 0, Game.MapWidth, Game.MapHeight),
            (0, -1, 0, 0, Game.MapWidth, y),
            (0, 1, 0, y + 1, Game.MapWidth, Game.MapHeight)
        };

        foreach (var (dx, dy, x1, y1, x2, y2) in directions)
        {
            if (!IsPlayerInSection(x1, y1, x2, y2) || !CanMoveTo(x + dx, y + dy)) continue;
            diggerCommand.DeltaX = dx;
            diggerCommand.DeltaY = dy;
            break;
        }
        return diggerCommand;
    }
    
    private static bool IsPlayerInSection(int x0, int y0,
        int x1, int y1)
    {
        for (var x = x0; x < x1; x++)
        for (var y = y0; y < y1; y++)
            if (Game.Map.GetValue(x, y) is Player)
                return true;
        return false;
    }

    public bool DeadInConflict(ICreature? conflictedObject)
    {
        switch (conflictedObject)
        {
            case Monster:
            case Sack sack when sack.FallDistance > 0:
                return true;
            default:
                return false;
        }
    }

    private static bool CanMoveTo(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Game.MapWidth || y >= Game.MapHeight)
            return false;
        var targetCell = Game.Map[x, y];
        return targetCell is null or Gold or Player;
    }
}