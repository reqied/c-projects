namespace GeometryTasks;

public class Vector
{
    public double X;
    public double Y;

    public double GetLength()
    {
        return Geometry.GetLength(this);
    }

    public Vector Add(Vector vector2)
    {
        return Geometry.Add(this, vector2);
    }

    public bool Belongs( Segment segment)
    {
        return Geometry.IsVectorInSegment(this, segment);
    }
}

public class Segment
{
    public Vector Begin;
    public Vector End;
    
    public double GetLength()
    {
        return Geometry.GetLength(this);
    }
    
    public bool Contains(Vector vector)
    {
        return Geometry.IsVectorInSegment(vector, this);
    }
}

public class Geometry
{
    public static double GetLength(Vector vector)
    {
        return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
    }

    public static Vector Add(Vector firstVector, Vector secondVector)
    {
        var vectorialSum = new Vector
        {
            X = firstVector.X + secondVector.X,
            Y = firstVector.Y + secondVector.Y
        };
        return vectorialSum;
    }
    public static double GetLength(Segment segment)
    {
        var deltaX = segment.End.X - segment.Begin.X;
        var deltaY = segment.End.Y - segment.Begin.Y;
        return GetLength(new Vector(){X = deltaX, Y = deltaY});
    }

    public static bool IsVectorInSegment(Vector vector, Segment segment)
    {
        var AB = GetLength(segment);
        var AX = GetLength(new Segment
        {
            Begin = new Vector { X = segment.Begin.X, Y =  segment.Begin.Y},
            End = vector
        });
        var XB = GetLength(new Segment()
        {
            Begin = vector,
            End = new Vector {X = segment.End.X, Y = segment.End.Y}
        });
        return Math.Abs(AX + XB - AB) < 1e-9;
    }
}


