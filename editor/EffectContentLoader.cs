using HxGraphics;
using HxManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace editor
{
    public class EffectContentLoader : GenericContentLoader<string, Effect>
    {
        public static EffectContentLoader Instance { get; } = new EffectContentLoader();

        static EffectContentLoader()
        {

        }

        private EffectContentLoader()
        {

        }

        public override void LoadContent(ContentManager contentManager)
        {
            Add("texture", contentManager.Load<Effect>("texture"));
        }
    }
}