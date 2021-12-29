using System;
using System.Collections.Generic;
using HxSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace h073_pushy
{
    
    public class Cutscene
    {

        public static Cutscene DoorOpen = new Cutscene().AddRange("cutscene/door/door_", 1, 7);
        
        public List<string> Scenes;
        public int Index = 0;
        public string CurrentFrame => Scenes[Index] ?? string.Empty;
        public TimeSpan LastUpdate;
        public TimeSpan Start = TimeSpan.Zero;
        public EventHandler<EventArgs> OnStop;

        public Cutscene()
        {
            Scenes = new List<string>();
        }

        public Cutscene Add(string scene)
        {
            Scenes.Add(scene);
            return this;
        }

        public Cutscene AddRange(string scene, int min, int max)
        {
            for (var i = min; i <= max; i++)
            {
                Scenes.Add($"{scene}{i}");
            }

            return this;
        }

        public void Update(GameTime gameTime)
        {
            

            if (Start == TimeSpan.Zero)
            {
                Start = gameTime.TotalGameTime;
                LastUpdate = Start;
            }

            if (gameTime.TotalGameTime - LastUpdate < new TimeSpan(0, 0, 0, 1, 0)) return;
            LastUpdate = gameTime.TotalGameTime;
            Index++;

            if (Index < Scenes.Count) return;
            Index = 0;
            Start = TimeSpan.Zero;
            OnStop?.Invoke(this, EventArgs.Empty);

        }
        
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Start != TimeSpan.Zero)
            {
                Hx.Instance.GraphicsDevice.Clear(new Color(32, 70, 49));
                spriteBatch.Draw(TextureContentLoader.Instance.Find("p_w"), new Rectangle(0, 0, Hx.Instance.ViewportWidth, Hx.Instance.ViewportHeight),new Color(32, 70, 49));
                spriteBatch.Draw(TextureContentLoader.Instance.Find(CurrentFrame), Hx.Instance.ViewportCenter.ToVector2(), null, Color.White, 0f, new Vector2(128, 128), 1f, SpriteEffects.None, 0f); 
            }
            
        }
        
    }
}