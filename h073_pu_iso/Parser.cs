using System.Windows.Forms;
using HxInput;
using HxManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace h073_pu_iso
{
    public class Parser
    {
        private string _textureKey;
        private Texture2D _texture = null;
        private GenericRequest<Texture2D> _request;
        private Point _position;
        private Color _color = Color.Red;
        
        private Direction _direction = Direction.Up;

        public Point Position => _position;

        private Stage _stage = null;

        public Parser(Stage stage)
        {
            _stage = stage;
            _textureKey = "parser";
            _request = TextureContentLoader.Instance.Request(_textureKey);
        }

        public bool Check(Ball ball)
        {
            return ball.Color == _color;
        }
        
        public void Teleport(Point amount)
        {
            Teleport(amount.X, amount.Y);
        }
        
        public void Teleport(int x, int y)
        {
            _position.X = x;
            _position.Y = y;
        }
        
        public void Move(Point amount)
        {
            Move(amount.X, amount.Y);
        }
        
        public bool Move(int x, int y)
        {
            if (!_stage.IsBlocked(_position.X + x, _position.Y + y))
            {
                if (!_stage.IsMovable(_position.X + x, _position.Y + y))
                {
                    _position.X += x;
                    _position.Y += y;
                    return true;
                }
            }

            return false;
        }

        public void Update(GameTime gameTime)
        {
            _texture ??= TextureContentLoader.Instance.Find(_textureKey);
            _request = TextureContentLoader.Instance.Request(_textureKey);
        }
        
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (_request.Success == false)
            {
                return;
            }
            spriteBatch.Draw(_request.Result, _position.ToVector2() * 32f, null, _color, _direction.ToRotation(), new Vector2(16, 16), 1f, SpriteEffects.None, 0f);
        }

        public void SetColor(Color color)
        {
            _color = color;
        }
    }
}