using System;

namespace Barycentric_coordinates_visualization
{
    public class Barycentric
    {
        /// <summary>
        /// 0 is not used
        /// </summary>
        private static double[] Xcoords = new double[4];
        private static double[] Ycoords = new double[4];
        private static double S2;
        private static double CalculateS2() => Xcoords[0] * Ycoords[1] + Xcoords[1] * Ycoords[2] + Xcoords[2] * Ycoords[0]
            - Xcoords[0] * Ycoords[2] - Xcoords[1] * Ycoords[0] - Xcoords[2] * Ycoords[1];

        public static double Coord1(double x, double y) => (x * (Ycoords[2] - Ycoords[3]) + y * (Xcoords[3] - Xcoords[2])
            + Xcoords[2] * Ycoords[3] - Xcoords[3] * Ycoords[2]) / S2;
        public static double Coord2(double x, double y) => (x * (Ycoords[3] - Ycoords[1]) + y * (Xcoords[1] - Xcoords[3])
            + Xcoords[3] * Ycoords[1] - Xcoords[1] * Ycoords[3]) / S2;
        public static double Coord3(double x, double y) => (x * (Ycoords[1] - Ycoords[2]) + y * (Xcoords[2] - Xcoords[1])
            + Xcoords[1] * Ycoords[2] - Xcoords[2] * Ycoords[1]) / S2;
        public static double CenterOf(double _x1, double _x2) => (Math.Max(_x1, _x2) + Math.Min(_x1, _x2)) / 2;

        /// <summary>
        /// Get x1,x2,x3,y1,y2,y3 massive
        /// </summary>
        public static void SetCoordinates(double[] _coordinates)
        {
            Array.Copy(_coordinates, 0, Xcoords, 1, 3);
            Array.Copy(_coordinates, 3, Ycoords, 1, 3);
            S2 = CalculateS2();
        }

        public static double[] getXCoords() => Xcoords;
        public static double[] getYCoords() => Ycoords;
    }
}
