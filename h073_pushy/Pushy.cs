using System.Windows.Forms;
using HxInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace h073_pushy
{
    public class Pushy : IInventoryHolder
    {
        private string _textureKey;
        private Texture2D _texture = null;
        private Point _position;

        private Inventory _inventory;
        private Direction _direction = Direction.Up;
        private Stage _stage = null;

        public int X => _position.X;
        public int Y => _position.Y;

        public Inventory Inventory => _inventory;
        
        public Pushy(Stage stage)
        {
            _stage = stage;
            _inventory = new Inventory(this);
            _textureKey = "pushy";
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
        
        public void Move(int x, int y)
        {
            if (_stage.IsBlocked(_position.X + x, _position.Y + y)) return;
            if (!_stage.IsMovable(_position.X + x, _position.Y + y))
            {
                _position.X += x;
                _position.Y += y;
            }
            else
            {
                //Collides With Movable
                var movable = _stage.GetMovable(_position.X + x, _position.Y + y);
                if (!movable.Move(x, y)) return;
                _position.X += x;
                _position.Y += y;
            }
        }

        public void Update(GameTime gameTime)
        {
            _texture ??= TextureContentLoader.Instance.Find(_textureKey);

            if (Input.Instance.IsKeyboardKeyDownOnce(Keys.Left))
            {
                Move(-1, 0);
                _direction = Direction.Left;
            }
            else if (Input.Instance.IsKeyboardKeyDownOnce(Keys.Down))
            {
                Move(0, 1);
                _direction = Direction.Down;
            }
            else if (Input.Instance.IsKeyboardKeyDownOnce(Keys.Right))
            {
                Move(1, 0);
                _direction = Direction.Right;
            }
            else if (Input.Instance.IsKeyboardKeyDownOnce(Keys.Up))
            {
                Move(0, -1);
                _direction = Direction.Up;
            }
        }
        
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_texture, _position.ToVector2() * 32f, null, Color.White, _direction.ToRotation(), new Vector2(16, 16), 1f, SpriteEffects.None, 0f);
        }
    }
}