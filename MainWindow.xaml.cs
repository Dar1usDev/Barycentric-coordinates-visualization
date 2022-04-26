using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Barycentric_coordinates_visualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PrepareCanvas()
        {
            canvas.Children.Clear();
            Barycentric.SetCoordinates(new double[6] { double.Parse(x1TextBox.Text), double.Parse(y1TextBox.Text), double.Parse(x2TextBox.Text),
                double.Parse(y2TextBox.Text),double.Parse(x3TextBox.Text),double.Parse(y3TextBox.Text)});
            DrawTriangle();
        }

        private void DrawTriangle()
        {
            Polyline triangle = new Polyline();
            triangle.Points = new PointCollection();
            triangle.Points.Add(new Point(double.Parse(x1TextBox.Text), double.Parse(y1TextBox.Text)));
            triangle.Points.Add(new Point(double.Parse(x2TextBox.Text), double.Parse(y2TextBox.Text)));
            triangle.Points.Add(new Point(double.Parse(x3TextBox.Text), double.Parse(y3TextBox.Text)));
            triangle.Points.Add(new Point(double.Parse(x1TextBox.Text), double.Parse(y1TextBox.Text)));
            triangle.Stroke = Brushes.Black;
            triangle.StrokeThickness = 5;
            canvas.Children.Add(triangle);
        }

        public void DrawPoint(double x, double y, double value)
        {
            PrepareCanvas();
            Ellipse point = new Ellipse();
            point.Margin = new Thickness(x, y, 0, 0);
            int r = 10;
            point.Width = r;
            point.Height = r;
            point.Fill = new SolidColorBrush(Color.FromArgb(255, (byte)(255 * value), 185, 0));
            canvas.Children.Add(point);
            ksiValueTextBox.Text = value.ToString();
        }

        private void drawPointButton_Click(object sender, RoutedEventArgs e)
        {
            double x = double.Parse(xTextBox.Text);
            double y = double.Parse(yTextBox.Text);
            DrawPoint(x, y, Barycentric.Coord1(x,y));
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
        }

        private void ksi1Button_Click(object sender, RoutedEventArgs e)
        {
            PrepareCanvas();
        }

        private void ksi2Button_Click(object sender, RoutedEventArgs e)
        {
            PrepareCanvas();
        }

        private void ksi3Button_Click(object sender, RoutedEventArgs e)
        {
            PrepareCanvas();
        }
    }
}
