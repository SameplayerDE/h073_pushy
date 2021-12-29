using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace h073_pushy
{
    public class Table : StageObject
    {

        public Table(int x = 0, int y = 0) : base(x, y)
        {
            _textureKey = "table";
            _isBlocking = true;
        }
        
        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(TextureContentLoader.Instance.Find(_textureKey), _position.ToVector2() * 32f, null, _color,
                _direction.ToRotation(), new Vector2(16, 16), 1f, SpriteEffects.None, 0f);
        }
    }
}