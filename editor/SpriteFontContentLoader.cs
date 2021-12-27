using HxGraphics;
using HxManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace editor
{
    public class SpriteFontContentLoader : GenericContentLoader<string, SpriteFont>
    {
        public static SpriteFontContentLoader Instance { get; } = new SpriteFontContentLoader();

        static SpriteFontContentLoader()
        {

        }

        private SpriteFontContentLoader()
        {

        }

        public override void LoadContent(ContentManager contentManager)
        {
            Add("default", contentManager.Load<SpriteFont>("Font"), true);
        }
    }

    namespace Font
    {
        public static class Extension
        {
            public static void DrawString(this SpriteBatch spriteBatch, string text, string font, Vector2 position, Color color)
            {
                spriteBatch.DrawString(SpriteFontContentLoader.Instance.Find(font), text, position, color);
            }
            
            public static void DrawString(this SpriteBatch spriteBatch, string text, string font, Vector2 position, float scale, Color color)
            {
                spriteBatch.DrawString(SpriteFontContentLoader.Instance.Find(font), text, position, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }
    }
    
    
}