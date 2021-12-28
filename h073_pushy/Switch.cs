using System.Xml.Linq;
using HxManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace h073_pushy
{
    public class Switch : StageObject
    {
        private Door _door;
        private bool _isActive = false;

        public Switch(int x = 0, int y = 0, Door door = null) : base(x, y)
        {
            _door = door;
        }

        public override void Update(GameTime gameTime)
        {
            if (_door != null)
            {
                if (_stage.IsMovable(_position.X, _position.Y))
                {
                    _isActive = true;
                }
                else
                {
                    _isActive = false;
                }
                _door.IsOpen = _isActive;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(TextureContentLoader.Instance.Find(!_isActive ? "switch_open" : "switch_closed"),
                _position.ToVector2() * 32f, null, _color, _direction.ToRotation(), new Vector2(16, 16), 1f,
                SpriteEffects.None, 0f);
        }
    }
}