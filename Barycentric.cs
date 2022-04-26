using System;

namespace Barycentric_coordinates_visualization
{
    public class Barycentric
    {
        private static double[] Xcrds = new double[3];
        private static double[] Ycrds = new double[3];
        private static double S2;
        private static double CalculateS2() => Xcrds[0] * Ycrds[1] + Xcrds[1] * Ycrds[2] + Xcrds[2] * Ycrds[0] 
            - Xcrds[0] * Ycrds[2] - Xcrds[1] * Ycrds[0] - Xcrds[2] * Ycrds[1];

        static public double Coord1(double x, double y) => (x * (Ycrds[1] - Ycrds[2]) + y * (Xcrds[2] - Xcrds[1]) 
            + Xcrds[1] * Ycrds[2] - Xcrds[2] * Ycrds[1]) / S2;
        static public double Coord2(double x, double y) => (x * (Ycrds[2] - Ycrds[0]) + y * (Xcrds[0] - Xcrds[2])
            + Xcrds[2] * Ycrds[0] - Xcrds[0] * Ycrds[2]) / S2;
        static public double Coord3(double x, double y) => (x * (Ycrds[0] - Ycrds[1]) + y * (Xcrds[1] - Xcrds[0]) 
            + Xcrds[0] * Ycrds[1] - Xcrds[1] * Ycrds[0]) / S2;
        static public double CenterOf(double _x1, double _x2) => (Math.Max(_x1, _x2) + Math.Min(_x1, _x2)) / 2;

        /// <summary>
        /// Get x1,y1,x2,y2,x3,y3 massive
        /// </summary>
        public static void SetCoordinates(double[] _coordinates)
        {
            Array.Copy(_coordinates, 0, Xcrds, 0, 3);
            Array.Copy(_coordinates, 3, Ycrds, 0, 3);
            S2 = CalculateS2();
        }
    }
}
