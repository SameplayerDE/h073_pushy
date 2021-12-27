using System.Xml.Linq;
using HxManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace h073_pushy
{
    public class Switch
    {
        
        private string _textureKey;
        private Texture2D _texture = null;
        private GenericRequest<Texture2D> _request;
        private Point _position;
        
        public bool Toggled = false;
        public Door Door = null;

        public void Update(GameTime gameTime)
        {
            if (Door != null)
            {
                Door.IsOpen = Toggled;
            }
        }
        
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (_request.Success == false)
            {
                return;
            }
            spriteBatch.Draw(_request.Result, _position.ToVector2() * 32f, null, Color.White, 0f, new Vector2(16, 16), 1f, SpriteEffects.None, 0f);
        }
        
    }
}