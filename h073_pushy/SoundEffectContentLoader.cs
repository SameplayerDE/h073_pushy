using HxManager;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace h073_pushy
{
    public class SoundEffectContentLoader : GenericContentLoader<string, SoundEffect>
    {
        public static SoundEffectContentLoader Instance { get; } = new SoundEffectContentLoader();

        static SoundEffectContentLoader()
        {
            
        }

        private SoundEffectContentLoader()
        {
            
        }

        public override void LoadContent(ContentManager contentManager)
        {
            Add("pickup", contentManager, "sounds/pickup");
            Add("open", contentManager, "sounds/open");
        }
    }
}