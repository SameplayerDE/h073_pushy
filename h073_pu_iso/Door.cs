using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace h073_pu_iso
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

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(SpritesheetResourceLoader.Instance.Find("iso").Sprites[44], Utils.ToIsometric(_position.X, _position.Y).ToVector2(), null, _color, _direction.ToRotation(), new Vector2(16, 16), 1f, SpriteEffects.None, 0f);
        }
    }
}