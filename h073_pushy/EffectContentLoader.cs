using HxManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace h073_pushy
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
            Add("default", contentManager, "default", true);
            Add("gba", contentManager, "GBA");
            Add("blur", contentManager, "Blur");
            Add("bloom", contentManager, "BloomExtract");
        }
    }
}