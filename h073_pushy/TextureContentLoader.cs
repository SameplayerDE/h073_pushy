using HxManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace h073_pushy
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
            Add("pushy", contentManager.Load<Texture2D>("pushy"));
            Add("ball", contentManager.Load<Texture2D>("ball"));
            Add("wall", contentManager.Load<Texture2D>("wall"));
            Add("floor", contentManager.Load<Texture2D>("floor"));
            Add("parser", contentManager.Load<Texture2D>("parser"));
            Add("house", contentManager.Load<Texture2D>("house"));
            
            Add("door_open", contentManager.Load<Texture2D>("door_open"));
            Add("door_closed", contentManager.Load<Texture2D>("door_closed"));
            
            Add("switch_open", contentManager.Load<Texture2D>("switch_open"));
            Add("switch_closed", contentManager.Load<Texture2D>("switch_close"));
            
            Add("inventory_slot", contentManager.Load<Texture2D>("inventory_slot"));
            Add("inventory_missing", contentManager.Load<Texture2D>("inventory_missing"));
            Add("inventory_key", contentManager.Load<Texture2D>("inventory_key"));

            for (var i = 1; i <= 16; i++)
            {
                Add($"laser_{i}", contentManager.Load<Texture2D>($"laser/laser_{i}"));
            }
        }
    }
}