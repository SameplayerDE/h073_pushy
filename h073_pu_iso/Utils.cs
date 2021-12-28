using Microsoft.Xna.Framework;

namespace h073_pu_iso
{
    public static class Utils
    {
        public static Point ToIsometric(int x, int y, int w = 64, int h = 32)
        {
            return new Point(x * 1 * w + y * -1 * h, (int)(x * 0.5 * w + y * -0.5 * h));
        }
    }
}