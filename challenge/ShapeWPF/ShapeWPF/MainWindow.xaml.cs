using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ShapeWPF
{
    public partial class MainWindow : Window
    {
        public Dictionary<string, string> Shapes = new() {{"ellipse", "e"}, {"rectangle", "r"}, {"triangle", "t"}};
        public MainWindow()
        {
            InitializeComponent();
            var question = "";
            var split = question.Split('|');
            var figures = split[0].Split('=')[1].Split(',');
            Shapes["ellipse"] = figures[0];
            Shapes["rectangle"] = figures[1];
            Shapes["triangle"] = figures[2];
            var stringPoints = split[1].Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            var points = GenerateCirclePoints(stringPoints);
            DrawTriangle(points.Count > 200 ? points.Slice(0, 200) : points);
        }

        private void DrawTriangle(List<Point> points)
        {
            var pointCol = new PointCollection();
            foreach (var point in points)
            {
                pointCol.Add(new Point(point.X, point.Y));
            }
            var shape = new Polygon
            {
                Stroke = Brushes.Black,
                Fill = Brushes.LightBlue,
                StrokeThickness = 2,
                Points = pointCol
            };
            DrawingCanvas.Children.Add(shape);
        }
        
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            ResponseLabel.Content = "ellipse";
            Console.WriteLine(Shapes["ellipse"]);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            ResponseLabel.Content = "rectangle";
            Console.WriteLine(Shapes["rectangle"]);
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            ResponseLabel.Content = "triangle";
            Console.WriteLine(Shapes["triangle"]);
        }
        private static List<Point> GenerateCirclePoints(string[] stringPoints)
        {
        
            var points = new List<Point>();
            foreach (var sp in stringPoints)
            {
                var xy = sp.Split(',');
                if (xy.Length != 2) continue;
                var x = double.Parse(xy[0]);
                var y = double.Parse(xy[1]);
                points.Add(new Point(x, y));
            }
            return points;
        }
    }
    
}
