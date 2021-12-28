using HxManager;
using Microsoft.Xna.Framework.Graphics;

namespace h073_pu_iso
{
    public class SpritesheetResourceLoader : GenericResourceLoader<string, Spritesheet>
    {
        public static SpritesheetResourceLoader Instance { get; } = new SpritesheetResourceLoader();

        static SpritesheetResourceLoader()
        {

        }

        private SpritesheetResourceLoader()
        {

        }

        public override void LoadResources()
        {
            base.LoadResources();
        }
    }
}