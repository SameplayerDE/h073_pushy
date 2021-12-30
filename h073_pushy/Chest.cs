using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace h073_pushy
{
    public class Chest : StageObject
    {

        public bool IsOpen = false;
        public InventoryItem Content = null;

        public Chest(int x = 0, int y = 0) : base(x, y)
        {
            _isBlocking = true;
        }

        public void SetContent(InventoryItem item)
        {
            Content = item;
        }
        
        public override void Update(GameTime gameTime)
        {
            
        }

        public bool Open(Pushy pushy)
        {
            if (IsOpen) return false;
            IsOpen = true;
            SoundEffectContentLoader.Instance.Find("open").Play();
            if (Content != null)
            {
                pushy.Inventory.Add(Content);
                SoundEffectContentLoader.Instance.Find("pickup").Play();
            }
            return true;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(TextureContentLoader.Instance.Find("chest"), _position.ToVector2() * 32f, new Rectangle(32 * (IsOpen ? 1 : 0), 0, 32, 32), _color, _direction.ToRotation(), new Vector2(16, 16), 1f, SpriteEffects.None, 0f);
        }
    }
}