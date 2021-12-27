using HxGraphics;
using HxManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace editor
{
    public class TextureContentLoader : GenericContentLoader<string, Texture2D>
    {
        public static TextureContentLoader Instance { get; } = new TextureContentLoader();

        static TextureContentLoader()
        {

        }

        private TextureContentLoader()
        {

        }

        public override void LoadContent(ContentManager contentManager)
        {
            Add("missing", contentManager.Load<Texture2D>("missing"), true);
            Add("grid", contentManager.Load<Texture2D>("grid"));
            Add("p_w", Graphics.Instance.GenerateTexture2D(2, 2, Color.White));
            
            //Icons
            Add("icons/place", contentManager.Load<Texture2D>("icons/place"));
            Add("icons/remove", contentManager.Load<Texture2D>("icons/remove"));
            Add("icons/edit", contentManager.Load<Texture2D>("icons/edit"));
            Add("icons/save", contentManager.Load<Texture2D>("icons/save"));
        }
    }

    namespace Texture
    {
        public static class Extension
        {
            public static void Draw(this SpriteBatch spriteBatch, string texture, Vector2 position, Color color)
            {
                spriteBatch.Draw(TextureContentLoader.Instance.Find(texture), position, color);
            }
            
            public static void Draw(this SpriteBatch spriteBatch, string texture, Rectangle rectangle, Color color)
            {
                spriteBatch.Draw(TextureContentLoader.Instance.Find(texture), rectangle, color);
            }
        }
    }
    
    
}