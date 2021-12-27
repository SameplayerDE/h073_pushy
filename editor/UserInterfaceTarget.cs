using System;
using editor.Texture;
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
        public event EventHandler<TileChangeEventArgs> OnTileChange;
        
        public UserInterfaceTarget(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //render TopMenu
            spriteBatch.Draw("p_w", new Rectangle(0, 0, 32, 32), CColor.DarkC);
            spriteBatch.Draw("icons/place", new Rectangle(0, 0, 32, 32), CColor.Dark);
            
            spriteBatch.Draw("p_w", new Rectangle(32, 0, 32, 32), CColor.DarkC);
            spriteBatch.Draw("icons/remove", new Rectangle(32, 0, 32, 32),  CColor.Dark);
            
            spriteBatch.Draw("p_w", new Rectangle(64, 0, 32, 32), CColor.DarkC);
            spriteBatch.Draw("icons/edit", new Rectangle(64, 0, 32, 32),  CColor.Dark);
            
            spriteBatch.Draw("p_w", new Rectangle(96, 0, 32, 32), CColor.DarkC);
            spriteBatch.Draw("icons/save", new Rectangle(96, 0, 32, 32),  CColor.Dark);
        }

    }
}