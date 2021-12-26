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
            Add("p_w", Graphics.Instance.GenerateTexture2D(2, 2, Color.White));
        }
    }
}