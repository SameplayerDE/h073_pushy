using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace editor
{

    public class TileChangeEventArgs : EventArgs
    {
        private int _tile;
        public int Tile => _tile;

        public TileChangeEventArgs(int tile)
        {
            _tile = tile;
        }
    }
    
    public class UserInterfaceTarget
    {

        private GraphicsDevice _graphicsDevice;
        
        public UserInterfaceTarget(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
        }

    }
}