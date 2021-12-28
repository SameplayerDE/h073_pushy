using Microsoft.Xna.Framework;

namespace h073_pu_iso
{
    public static class Utils
    {
        public static Point ToIsometric(int x, int y, int w = 64, int h = 64)
        {
            var xHalf = new Vector2(1, 0.5f);
            var yHalf = new Vector2(-1, 0.5f);
            var position = new Vector2(x, y);

            //return (position * xHalf * yHalf).ToPoint();
            //-16 + x * 16 - (y * 16), -16 + y * 8 + (x * 8)
            return new Point(x * 1 * w / 2 + y * -1 * h / 2, (int)(x * 0.5 * w / 2 + y * 0.5 * h / 2));
        }
    }
}