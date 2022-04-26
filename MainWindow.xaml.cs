using System;
using System.Diagnostics;
using System.Globalization;
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
        private double pointDiameter = 5;
        public MainWindow()
        {
            InitializeComponent();
            ksi1RadioButton.IsChecked = true;
        }

        private void PrepareCanvas()
        {
            canvas.Children.Clear();
            Barycentric.SetCoordinates(new double[6] { double.Parse(x1TextBox.Text), double.Parse(x2TextBox.Text), double.Parse(x3TextBox.Text),
                double.Parse(y1TextBox.Text),double.Parse(y2TextBox.Text),double.Parse(y3TextBox.Text)});
            DrawTriangle();
        }

        private void DrawTriangle()
        {
            Polyline triangle = new();
            triangle.Points = new();
            triangle.Points.Add(new(double.Parse(x1TextBox.Text) - pointDiameter, double.Parse(y1TextBox.Text) + pointDiameter));
            triangle.Points.Add(new(double.Parse(x2TextBox.Text) + pointDiameter, double.Parse(y2TextBox.Text) + pointDiameter));
            triangle.Points.Add(new(double.Parse(x3TextBox.Text) + pointDiameter / 2, double.Parse(y3TextBox.Text) - pointDiameter));
            triangle.Points.Add(new(double.Parse(x1TextBox.Text) - pointDiameter, double.Parse(y1TextBox.Text) + pointDiameter));
            triangle.Stroke = Brushes.Black;
            triangle.StrokeThickness = 5;
            canvas.Children.Add(triangle);
        }

        public void DrawPoint(double x, double y, double value)
        {
            Ellipse point = new();
            point.Margin = new(x, y, 0, 0);
            point.Width = pointDiameter;
            point.Height = pointDiameter;
            point.Fill = new SolidColorBrush(Color.FromArgb(255, 0, (byte)(255 * value), 0));
            canvas.Children.Add(point);
        }

        private void drawPointButton_Click(object sender, RoutedEventArgs e)
        {
            double x = double.Parse(xTextBox.Text);
            double y = double.Parse(yTextBox.Text);
            PrepareCanvas();
            DrawPoint(x, y, Barycentric.Coord1(x, y));
        }

        private void ClearAll()
        {
            canvas.Children.Clear();
            ksi1TextBox.Text = string.Empty;
            ksi2TextBox.Text = string.Empty;
            ksi3TextBox.Text = string.Empty;
            xTextBox.Text = string.Empty;
            yTextBox.Text = string.Empty;
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void ksi1Button_Click(object sender, RoutedEventArgs e)
        {
            AnalizeKsi(1, ksi1TextBox.Text == "" ? null : double.Parse(ksi1TextBox.Text, CultureInfo.InvariantCulture));
        }

        private void ksi2Button_Click(object sender, RoutedEventArgs e)
        {
            AnalizeKsi(2, ksi2TextBox.Text == "" ? null : double.Parse(ksi2TextBox.Text, CultureInfo.InvariantCulture));
        }

        private void ksi3Button_Click(object sender, RoutedEventArgs e)
        {
            AnalizeKsi(3, ksi3TextBox.Text == "" ? null : double.Parse(ksi3TextBox.Text, CultureInfo.InvariantCulture));
        }

        private void AnalizeKsi(int i, double? ksiValue = null)
        {

            PrepareCanvas();

            double[] XCoords = Barycentric.getXCoords();
            double[] YCoords = Barycentric.getYCoords();

            int xStep = 2;
            int yStep = 2;

            for (double y = YCoords[3]; y <= YCoords[1]; y += yStep)
            {

                double startX = (XCoords[3] - XCoords[1]) * (y - YCoords[1]) / (YCoords[3] - YCoords[1]) + XCoords[1];
                double endX = (XCoords[3] - XCoords[2]) * (y - YCoords[2]) / (YCoords[3] - YCoords[2]) + XCoords[2];

                for (double x = startX; x < endX; x += xStep)
                {
                    double value = 0;
                    switch (i)
                    {
                        case 1:
                            value = Barycentric.Coord1(x, y);
                            break;
                        case 2:
                            value = Barycentric.Coord2(x, y);
                            break;
                        case 3:
                            value = Barycentric.Coord3(x, y);
                            break;
                    }

                    int errorValue = 1;

                    if (ksiValue == null)
                        DrawPoint(x, y, value);
                    else
                        if (value <= ksiValue * (1 + errorValue / 100.0) && value >= ksiValue * (1 - errorValue / 100.0))
                            DrawPoint(x, y, value);
                }
            }
            DrawTriangle();
        }

        private void canvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            double x = e.GetPosition(canvas).X - pointDiameter / 2;
            double y = e.GetPosition(canvas).Y - pointDiameter / 2;

#pragma warning disable CS8629 // Тип значения, допускающего NULL, может быть NULL.
            if ((bool)ksi1RadioButton.IsChecked)
                DrawPoint(x, y, Barycentric.Coord1(x, y));
            else if ((bool)ksi2RadioButton.IsChecked)
                DrawPoint(x, y, Barycentric.Coord2(x, y));
            else
                DrawPoint(x, y, Barycentric.Coord3(x, y));
#pragma warning restore CS8629 // Тип значения, допускающего NULL, может быть NULL.

            xTextBox.Text = x.ToString();
            yTextBox.Text = y.ToString();

            ksi1TextBox.Text = Barycentric.Coord1(x, y).ToString();
            ksi2TextBox.Text = Barycentric.Coord2(x, y).ToString();
            ksi3TextBox.Text = Barycentric.Coord3(x, y).ToString();
        }

        private void drawTriangleButton_Click(object sender, RoutedEventArgs e)
        {
            PrepareCanvas();
        }
    }
}
