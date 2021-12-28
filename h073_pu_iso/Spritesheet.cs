using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace h073_pu_iso
{
    public class Spritesheet
    {

        public static GraphicsDevice GraphicsDevice = null;

        public Texture2D[] Sprites;
        
        public Spritesheet(GraphicsDevice graphicsDevice, Texture2D texture, int spriteWidth, int spriteHeight)
        {

            int spritesPerRow = texture.Width / spriteWidth;
            int spritesPerCol = texture.Height / spriteHeight;

            Sprites = new Texture2D[spritesPerRow * spritesPerCol];
            
            for (int x = 0; x < spritesPerRow; x += 1)
            {
                for (int y = 0; y < spritesPerCol; y += 1)
                {
                    Rectangle sourceRectangle = new Rectangle(x * spriteWidth, y * spriteHeight, spriteWidth, spriteHeight);
                    Texture2D newTexture = new Texture2D(graphicsDevice, sourceRectangle.Width, sourceRectangle.Height);
                    Color[] data = new Color[sourceRectangle.Width * sourceRectangle.Height];
                    texture.GetData(0, sourceRectangle, data, 0, data.Length);
                    newTexture.SetData(data);
                    Sprites[spritesPerRow * y + x] = newTexture;
                }
            }
        }
    }
}