using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace h073_pu_iso
{
    public abstract class StageObject
    {
        protected string _textureKey;
        protected Point _position;
        protected Color _color;
        protected bool _isBlocking;
        protected Direction _direction;
        protected Stage _stage = null;

        public bool IsBlocking => _isBlocking;
        public Point Position => _position;
        
        public StageObject(int x = 0, int y = 0)
        {
            _color = Color.White;
            _position.X = x;
            _position.Y = y;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        public void SetStage(Stage stage)
        {
            _stage = stage;
        }
    }
}