using HxSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace editor
{
    public class NineTile
    {
        public Texture2D Texture2D;
        public Spritesheet Spritesheet;

        public Rectangle
            a,
            b,
            c,
            d,
            e,
            f,
            g,
            h,
            i;

        public NineTile(Texture2D texture2D, int x0, int x1, int x2, int y0, int y1, int y2)
        {
            Texture2D = texture2D;
            Spritesheet = new Spritesheet(Hx.Instance.GraphicsDevice, Texture2D, x0, y0);
            a = new Rectangle(0, 0, x0, y0); // TopLeft
            b = new Rectangle(x0, 0, x1, y0); // Top
            c = new Rectangle(x0 + x1, 0, x2, y0); // TopRight
            d = new Rectangle(0, y0,x0, y1); // Left
            e = new Rectangle(x0, y0,x1, y1); // Center
            f = new Rectangle(x0 + x1, y0,x2, y1); // Right
            g = new Rectangle(0, y0 + y1,x0, y2); // BottomLeft
            h = new Rectangle(x0, y0 + y1,x1, y2); // Bottom
            i = new Rectangle(x0 + x1, y0 + y1,x2, y2); // BottomRight
        }
        
        public NineTile(Texture2D texture2d, int x0, int y0) : this(texture2d, x0, x0, x0, y0, y0, y0)
        {
            
        }
        
        public NineTile(Texture2D texture2D, int z) : this(texture2D, z, z)
        {
            
        }
    }
}