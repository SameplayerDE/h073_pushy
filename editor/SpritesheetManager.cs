using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace editor
{
    
    public abstract class SpritesheetLoader
    {
        public Dictionary<string, Spritesheet> Resources = new Dictionary<string, Spritesheet>();

        public bool Has(string name)
        {
            return Resources.ContainsKey(name);
        }

        public Spritesheet Find(string name)
        {
            if (Has(name))
            {
                return Resources[name];
            }
            return Resources["missing"];
        }

        public void Add(string key, Spritesheet value, bool overwrite = false)
        {
            if (overwrite)
            {
                Resources[key] = value;
            }
            else
            {
                Resources.Add(key, value);
            }
        }

        public abstract void LoadContent(ContentManager contentManager);
    }
    
    public class SpritesheetManager : SpritesheetLoader
    {
        public GraphicsDevice GraphicsDevice;
        public static SpritesheetManager Instance { get; } = new SpritesheetManager();

        static SpritesheetManager()
        {
            
        }
        
        private SpritesheetManager()
        {
            
        }
        
        public override void LoadContent(ContentManager contentManager)
        {
            //Add("window", new Spritesheet(GraphicsDevice, TextureManager.Instance.Find("window"), 10, 10));
            //Add("torterra", new Spritesheet(GraphicsDevice, TextureManager.Instance.Find("torterra"), 80, 79));
            //Add("bag", new Spritesheet(GraphicsDevice, TextureManager.Instance.Find("bag"), 64, 64));
        }
    }

    public static class SpritesheetManagerSpriteBatchExpander
    {
        public static void Draw(this SpriteBatch spriteBatch, string key, int id, Vector2 position, Color color)
        {
            var texture = SpritesheetManager.Instance.Find(key);
            spriteBatch.Draw(texture.Sprites[id], position, color);
        }
        
        public static void Draw(this SpriteBatch spriteBatch, string key, int id, Rectangle rectangle, Color color)
        {
            var texture = SpritesheetManager.Instance.Find(key);
            spriteBatch.Draw(texture.Sprites[id], rectangle, color);
        }
        
        public static void Draw(this SpriteBatch spriteBatch, Spritesheet spritesheet, int id, Rectangle rectangle, Color color)
        {
            spriteBatch.Draw(spritesheet.Sprites[id], rectangle, color);
        }
        
        public static void Draw(this SpriteBatch spriteBatch, Spritesheet spritesheet, int id, Rectangle destination, Rectangle source, Color color)
        {
            spriteBatch.Draw(spritesheet.Sprites[id], destination, source, color);
        }
    }
}