using editor.Texture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace editor.UI
{
    public class HScrollBar
    {
        public int Max = 0, Value = 0;
        public Point Position;
        public Point Size = new Point(10, 10);

        public HScrollBar(Point position, Point size)
        {
            Position = position;
            Size = size;
        }
        
        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw("p_w", new Rectangle(Position, Size), CColor.Dark);
            NineTileRenderer.DrawSheet(spriteBatch, NineTileResourceLoader.Instance.Find("scrollbar"), new Rectangle(new Point(Position.X, Position.Y * Value), Size));
        }
    }
}