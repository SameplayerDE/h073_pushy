using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace h073_pushy
{
    public static class CutsceneManager
    {
        public static Cutscene Current = null;

        public static void Set(Cutscene cutscene)
        {
            Current = cutscene;
            Current.OnStop += (sender, args) => Clear();
        }

        private static void Clear()
        {
            Current = null;
        }

        public static void Update(GameTime gameTime)
        {
            Current?.Update(gameTime);
        }
        
        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Current?.Draw(spriteBatch, gameTime);
        }
        
    }
}