using System;

namespace EmergeReality.Color
{
    public static class Colors
    {
        private static double[] RGBtoLABColors(UnityEngine.Color c)
        {
            double r, g, b, X, Y, Z, xr, yr, zr;

            // D65/2°
            double Xr = 95.047;
            double Yr = 100.0;
            double Zr = 108.883;


            // --------- RGB to XYZ ---------//
            r = c.r;
            g = c.g;
            b = c.b;

            if (r > 0.04045)
                r = Math.Pow((r + 0.055) / 1.055, 2.4);
            else
                r /= 12.92;

            if (g > 0.04045)
                g = Math.Pow((g + 0.055) / 1.055, 2.4);
            else
                g /= 12.92;

            if (b > 0.04045)
                b = Math.Pow((b + 0.055) / 1.055, 2.4);
            else
                b /= 12.92;

            r *= 100;
            g *= 100;
            b *= 100;

            X = (0.4124 * r) + (0.3576 * g) + (0.1805 * b);
            Y = (0.2126 * r) + (0.7152 * g) + (0.0722 * b);
            Z = (0.0193 * r) + (0.1192 * g) + (0.9505 * b);


            // --------- XYZ to Lab --------- //
            xr = X / Xr;
            yr = Y / Yr;
            zr = Z / Zr;

            if (xr > 0.008856)
                xr = Math.Pow(xr, 0.3333333333);
            else
                xr = ((7.787 * xr) + (16 / 116.0));

            if (yr > 0.008856)
                yr = Math.Pow(yr, 0.3333333333);
            else
                yr = ((7.787 * yr) + (16 / 116.0));

            if (zr > 0.008856)
                zr = Math.Pow(zr, 0.3333333333);
            else
                zr = ((7.787 * zr) + (16 / 116.0));


            double[] lab = new double[3];

            lab[0] = (double)((116 * yr) - 16);
            lab[1] = (double)(500 * (xr - yr));
            lab[2] = (double)(200 * (yr - zr));

            return lab;
        }


        public static double CalculateDeltaE(UnityEngine.Color colorX, UnityEngine.Color colorY)
        {
            double[] labX = RGBtoLABColors(colorX);
            double[] labY = RGBtoLABColors(colorY);

            double l = (labX[0] - labY[0]) * (labX[0] - labY[0]);
            double a = (labX[1] - labY[1]) * (labX[1] - labY[1]);
            double b = (labX[2] - labY[2]) * (labX[2] - labY[2]);

            return (double)Math.Sqrt(l + a + b);
        }
    }
}
