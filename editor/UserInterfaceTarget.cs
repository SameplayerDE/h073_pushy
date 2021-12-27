using System;
using editor.Font;
using editor.Texture;
using HxInput;
using HxMath;
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
    
    public class SelectionSizeChangeEventArgs : EventArgs
    {
        private int _size;
        public int Size => _size;

        public SelectionSizeChangeEventArgs(int size)
        {
            _size = size;
        }
    }
    
    public class UserInterfaceTarget
    {

        private GraphicsDevice _graphicsDevice;
        private int _vWidth => _graphicsDevice.Viewport.Width;
        private int _vHeight => _graphicsDevice.Viewport.Height;
        private int _selectionWidth = 128;
        private bool _selectionSizeChange = false;
        private Rectangle _selectionRectangle;
        public event EventHandler<TileChangeEventArgs> OnTileChange;
        public event EventHandler<SelectionSizeChangeEventArgs> OnSelectionChangeSize;
        
        public UserInterfaceTarget(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _selectionRectangle = new Rectangle(0, 0, _selectionWidth, _vHeight);
        }

        public void Update(GameTime gameTime)
        {
            var mPos = Input.Instance.LatestMousePosition;
            if (new Rectangle(_selectionWidth - 5, 0, _selectionWidth + 5, _vHeight).Contains(mPos) && !_selectionSizeChange)
            {
                if (Input.Instance.IsMouseKeyDown(MouseButton.Left))
                {
                    _selectionSizeChange = true;
                }
            }

            if (_selectionSizeChange)
            {
                if (!Input.Instance.IsMouseKeyDown(MouseButton.Left))
                {
                    _selectionSizeChange = false;
                    return;
                }
                _selectionWidth = Input.Instance.LatestMousePosition.X;
                _selectionWidth = Math.Clamp(_selectionWidth, 128, 500);
                
                OnSelectionChangeSize?.Invoke(this, new SelectionSizeChangeEventArgs(_selectionWidth));
            }
            _selectionRectangle.Width = _selectionWidth;
            _selectionRectangle.Height = _vHeight;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var sideSize = 32;
            //Render Tool Side
            spriteBatch.Draw("p_w", new Rectangle(_vWidth - sideSize, 0, sideSize, sideSize), CColor.DarkC);
            spriteBatch.Draw("icons/select", new Rectangle(_vWidth - sideSize, 0, sideSize, sideSize), CColor.Dark);
            
            spriteBatch.Draw("p_w", new Rectangle(_vWidth- sideSize, sideSize, sideSize, sideSize), CColor.DarkC);
            spriteBatch.Draw("icons/paint", new Rectangle(_vWidth- sideSize, sideSize, sideSize, sideSize),  CColor.Dark);
            
            //Render Selection
            spriteBatch.Draw("p_w", _selectionRectangle, CColor.DarkC);
            for (int y = 0; y < 100;)
            {
                for (int i = 0; i < 100; i++)
                {
                    var rect = new Rectangle(8 + i * 32 + 16 * i, 8 + 32 * y, 32, 32);
                    if (_selectionRectangle.Contains(rect))
                    {
                        spriteBatch.Draw("p_w", rect, CColor.DarkC);
                        spriteBatch.Draw("icons/save", rect, CColor.Dark);
                        spriteBatch.DrawString($"{i}:{y}", "default", rect.Location.ToVector2(), 0.5f, CColor.BrightC);
                    }
                    else
                    {
                        y++;
                        break;
                    }
                }
            }
        }

    }
}