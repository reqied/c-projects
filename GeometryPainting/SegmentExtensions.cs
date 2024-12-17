using System.Runtime.CompilerServices;
using Avalonia.Media;
using GeometryTasks;

namespace GeometryPainting;

public static class SegmentExtensions
{
    private static ConditionalWeakTable<Segment, ColorExtensions> DicionaryOfColor = new ();

    public static void SetColor(this Segment segment, Color color)
    {
        DicionaryOfColor.GetOrCreateValue(segment).Value = color;
    }

    public static Color GetColor(this Segment segment)
    {
        return !DicionaryOfColor.TryGetValue(segment, out var color) ? Colors.Black: color.Value;
    }
}

public class ColorExtensions
{
    public Color Value;
}