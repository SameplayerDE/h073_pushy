using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace editor
{
    public static class NineTileRenderer
    {
        
        public static void Draw(SpriteBatch spriteBatch, string key, Vector2 position, int width, int height, float alpha = 1f)
        {
            Draw(spriteBatch, NineTileResourceLoader.Instance.Find(key), new Rectangle((int) position.X, (int)position.Y, width, height), alpha);
        }

        public static void Draw(SpriteBatch spriteBatch, string key, Rectangle rectangle, float alpha = 1f)
        {
            Draw(spriteBatch, NineTileResourceLoader.Instance.Find(key), new Rectangle(rectangle.Location, rectangle.Size), alpha);
        }

        public static void Draw(SpriteBatch spriteBatch, NineTile nineTile, Rectangle rectangle, float alpha = 1f)
        {
            
            // TopLeft
            
            spriteBatch.Draw(
                nineTile.Texture2D,
                new Rectangle(rectangle.X, rectangle.Y, nineTile.a.Width, nineTile.a.Height),
                nineTile.a, 
                Color.White * alpha);
            
            //Top
            
            spriteBatch.Draw(
                nineTile.Texture2D,
                new Rectangle(rectangle.X + nineTile.a.Width, rectangle.Y, rectangle.Width - nineTile.a.Width - nineTile.c.Width, nineTile.b.Height),
                nineTile.b, 
                Color.White * alpha);
            
            //TopRight
            
            spriteBatch.Draw(
                nineTile.Texture2D,
                new Rectangle(rectangle.X + (rectangle.Width - nineTile.c.Width), rectangle.Y, nineTile.c.Width, nineTile.c.Height),
                nineTile.c, 
                Color.White * alpha);
            
            
            // Left
            
            spriteBatch.Draw(
                nineTile.Texture2D,
                new Rectangle(rectangle.X, rectangle.Y + nineTile.a.Height, nineTile.d.Width, rectangle.Height - nineTile.a.Height - nineTile.g.Height),
                nineTile.d, 
                Color.White * alpha);
            
            //Center
            
            spriteBatch.Draw(
                nineTile.Texture2D,
                new Rectangle(rectangle.X + nineTile.a.Width, rectangle.Y + nineTile.a.Height, rectangle.Width - nineTile.a.Width - nineTile.c.Width, rectangle.Height - nineTile.a.Height - nineTile.g.Height),
                nineTile.e, 
                Color.White * alpha);
            
            // Right
            
            spriteBatch.Draw(
                nineTile.Texture2D,
                new Rectangle(rectangle.X + (rectangle.Width - nineTile.f.Width), rectangle.Y + nineTile.c.Height, nineTile.c.Width, rectangle.Height - nineTile.a.Height - nineTile.g.Height),
                nineTile.f, 
                Color.White * alpha);
            
            
            // Left
            
            spriteBatch.Draw(
                nineTile.Texture2D,
                new Rectangle(rectangle.X, rectangle.Y + (rectangle.Height - nineTile.g.Height), nineTile.g.Width, nineTile.g.Height),
                nineTile.g, 
                Color.White * alpha);
            
            //Center
            
            spriteBatch.Draw(
                nineTile.Texture2D,
                new Rectangle(rectangle.X + nineTile.g.Width, rectangle.Y + (rectangle.Height - nineTile.h.Height), rectangle.Width - nineTile.g.Width - nineTile.i.Width, nineTile.h.Height),
                nineTile.h, 
                Color.White * alpha);
            
            // Right
            
            spriteBatch.Draw(
                nineTile.Texture2D,
                new Rectangle(rectangle.X + (rectangle.Width - nineTile.i.Width), rectangle.Y + (rectangle.Height - nineTile.i.Height), nineTile.i.Width, nineTile.i.Height),
                nineTile.i, 
                Color.White * alpha);


        }
        
        public static void DrawSheet(SpriteBatch spriteBatch, NineTile nineTile, Rectangle rectangle, float alpha = 1f)
        {
            
            // TopLeft
            
            spriteBatch.Draw(
                nineTile.Spritesheet.Sprites[0],
                new Rectangle(rectangle.X, rectangle.Y, nineTile.a.Width, nineTile.a.Height),
                null, 
                Color.White * alpha);
            
            //Top
            
            spriteBatch.Draw(
                nineTile.Spritesheet.Sprites[1],
                new Rectangle(rectangle.X + nineTile.a.Width, rectangle.Y, rectangle.Width - nineTile.a.Width - nineTile.c.Width, nineTile.b.Height),
                null, 
                Color.White * alpha);
            
            //TopRight
            
            spriteBatch.Draw(
                nineTile.Spritesheet.Sprites[2],
                new Rectangle(rectangle.X + (rectangle.Width - nineTile.c.Width), rectangle.Y, nineTile.c.Width, nineTile.c.Height),
                null, 
                Color.White * alpha);
            
            
            // Left
            
            spriteBatch.Draw(
                nineTile.Spritesheet.Sprites[3],
                new Rectangle(rectangle.X, rectangle.Y + nineTile.a.Height, nineTile.d.Width, rectangle.Height - nineTile.a.Height - nineTile.g.Height),
                null, 
                Color.White * alpha);
            
            //Center
            
            spriteBatch.Draw(
                nineTile.Spritesheet.Sprites[4],
                new Rectangle(rectangle.X + nineTile.a.Width, rectangle.Y + nineTile.a.Height, rectangle.Width - nineTile.a.Width - nineTile.c.Width, rectangle.Height - nineTile.a.Height - nineTile.g.Height),
                null, 
                Color.White * alpha);
            
            // Right
            
            spriteBatch.Draw(
                nineTile.Spritesheet.Sprites[5],
                new Rectangle(rectangle.X + (rectangle.Width - nineTile.f.Width), rectangle.Y + nineTile.c.Height, nineTile.c.Width, rectangle.Height - nineTile.a.Height - nineTile.g.Height),
                null, 
                Color.White * alpha);
            
            
            // Left
            
            spriteBatch.Draw(
                nineTile.Spritesheet.Sprites[6],
                new Rectangle(rectangle.X, rectangle.Y + (rectangle.Height - nineTile.g.Height), nineTile.g.Width, nineTile.g.Height),
                null, 
                Color.White * alpha);
            
            //Center
            
            spriteBatch.Draw(
                nineTile.Spritesheet.Sprites[7],
                new Rectangle(rectangle.X + nineTile.g.Width, rectangle.Y + (rectangle.Height - nineTile.h.Height), rectangle.Width - nineTile.g.Width - nineTile.i.Width, nineTile.h.Height),
                null, 
                Color.White * alpha);
            
            // Right
            
            spriteBatch.Draw(
                nineTile.Spritesheet.Sprites[8],
                new Rectangle(rectangle.X + (rectangle.Width - nineTile.i.Width), rectangle.Y + (rectangle.Height - nineTile.i.Height), nineTile.i.Width, nineTile.i.Height),
                null, 
                Color.White * alpha);


        }
        
    }
}