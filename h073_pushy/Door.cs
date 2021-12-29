using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace h073_pushy
{
    public class Door : StageObject
    {
        public bool IsOpen = false;

        public Door(int x = 0, int y = 0) : base(x, y)
        {
        }

        public override void Update(GameTime gameTime)
        {
            _isBlocking = !IsOpen;
        }

        public bool Open()
        {
            if (IsOpen) return false;
            IsOpen = true;
            SoundEffectContentLoader.Instance.Find("open").Play();
            return true;

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(TextureContentLoader.Instance.Find(IsOpen ? "door_open" : "door_closed"), _position.ToVector2() * 32f, null, _color, _direction.ToRotation(), new Vector2(16, 16), 1f, SpriteEffects.None, 0f);
        }
    }
}