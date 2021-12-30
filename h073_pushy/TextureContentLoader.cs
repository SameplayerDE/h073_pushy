using HxGraphics;
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
            Add("p_w", Graphics.Instance.GenerateTexture2D(1, 1, Color.White));
            Add("pushy", contentManager.Load<Texture2D>("pushy"));
            Add("ball", contentManager.Load<Texture2D>("ball"));
            Add("wall", contentManager.Load<Texture2D>("wall"));
            Add("floor", contentManager.Load<Texture2D>("floor"));
            Add("parser", contentManager.Load<Texture2D>("parser"));
            Add("house", contentManager.Load<Texture2D>("house"));
            Add("sign", contentManager.Load<Texture2D>("sign"));
            Add("table", contentManager.Load<Texture2D>("table"));
            Add("chest", contentManager.Load<Texture2D>("chest"));
            
            Add("door_open", contentManager.Load<Texture2D>("door_open"));
            Add("door_closed", contentManager.Load<Texture2D>("door_closed"));
            
            Add("switch_open", contentManager.Load<Texture2D>("switch_open"));
            Add("switch_closed", contentManager.Load<Texture2D>("switch_close"));
            
            Add("inventory_slot", contentManager.Load<Texture2D>("inventory_slot"));
            Add("inventory_missing", contentManager.Load<Texture2D>("inventory_missing"));
            
            Add("inventory_key", contentManager.Load<Texture2D>("inventory_key"));
            Add("key", contentManager.Load<Texture2D>("key"));
            Add("up", contentManager.Load<Texture2D>("up"));
            Add("down", contentManager.Load<Texture2D>("down"));
            Add("door_key", contentManager.Load<Texture2D>("door_key"));

            for (var i = 1; i <= 16; i++)
            {
                Add($"laser_{i}", contentManager.Load<Texture2D>($"laser/laser_{i}"));
            }
            
            for (var i = 1; i <= 4; i++)
            {
                Add($"cutscene/door/door_{i}", contentManager, $"cutscene/door/door_{i}");
            }
            
            for (var i = 1; i <= 4; i++)
            {
                Add($"cutscene/chest/chest_{i}", contentManager, $"cutscene/chest/chest_{i}");
            }
            
        }
    }
}