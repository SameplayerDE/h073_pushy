using System;
using HxInput;
using HxInput.Events;
using HxMath;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace editor
{
    public class EditorTarget
    {

        private Camera _camera;
        private GraphicsDevice _graphicsDevice;
        
        private int _width;
        private int _height;
        private int[,] _tiles;
        
        private int _tileWidth = 32;
        private int _tileHeight = 32;
        
        private int _editorX;
        private int _editorY;
        private int _editorW;
        private int _editorH;

        private Point _tranformedMouse;
        private Effect _effect;

        private bool _drawMouse = false;
        
        
        public int Width => _width;
        public int Height => _height;
        public int[,] Tiles => _tiles;

        public int TileSelection = -1;
        

        public EditorTarget(GraphicsDevice graphicsDevice, Camera camera, int width = 64, int height = 64)
        {
            _graphicsDevice = graphicsDevice;
            _camera = camera;
            _width = width;
            _height = height;
            
            _editorX = 128;
            _editorY = 0;
            _editorW = _graphicsDevice.Viewport.Width;
            _editorH = _graphicsDevice.Viewport.Height;
            
            _tiles = new int[_width, _height];
            var centerPosition = new Vector2(_width, height);
            centerPosition /= 2;
            centerPosition *= 32;
            _camera.Teleport(centerPosition.X, centerPosition.Y);
            Input.Instance.OnMouseScrollWheelValueChange += Zoom;
            Input.Instance.OnMouseMove += Move;
        }

        public void Update(GameTime gameTime)
        {
            _effect ??= EffectContentLoader.Instance.Find("texture");
            
            _effect.Parameters["Texture"].SetValue(TextureContentLoader.Instance.Find("grid"));
            _effect.Parameters["Width"]?.SetValue(_width);
            _effect.Parameters["Height"]?.SetValue(_height);
            _effect.Parameters["MatrixTransform"]?.SetValue(Matrix.CreateScale(_camera.Scale) * Matrix.CreateTranslation(-_camera.Position));
            _effect.Parameters["ElapsedSeconds"]?.SetValue((float)gameTime.ElapsedGameTime.TotalSeconds);
            _effect.Parameters["TotalSeconds"]?.SetValue((float)gameTime.TotalGameTime.TotalSeconds);
            _effect.Parameters["TotalMilliseconds"]?.SetValue((float)gameTime.TotalGameTime.TotalMilliseconds);
            
            _tranformedMouse = Math2D.InverseTransform(Input.Instance.LatestMousePosition, Matrix.CreateScale(_camera.Scale) * Matrix.CreateTranslation(-_camera.Position)).ToPoint();
            var area = new Rectangle(new Point(0, 0), new Point(_tileWidth * _width, _tileHeight * _height));
            
            _drawMouse = area.Contains(_tranformedMouse);

            float tilesetW = _width * 32 * _camera.Scale;
            float tilesetH = _height * 32 * _camera.Scale;
            
            Matrix m = Matrix.CreateScale(_camera.Scale) * Matrix.CreateTranslation(-_camera.Position);
            float areaX = Math2D.Transform(new Vector2(0, 0), m).X, areaY = Math2D.Transform(new Vector2(0, 0), m).Y;
            float camX = Math2D.Transform(new Vector2(_camera.RawPosition.X, _camera.RawPosition.Y), m).X, camY = Math2D.Transform(new Vector2(_camera.RawPosition.X, _camera.RawPosition.Y), m).Y;

            if (_effect.Parameters["OffX"] != null)
            {
                _effect.Parameters["OffX"].SetValue(areaX);
            }
            
            if (_effect.Parameters["OffY"] != null)
            {
                _effect.Parameters["OffY"].SetValue(areaY);
            }
            
            if (tilesetH > (_editorH / 2)) //area is bigger
            {
                if (areaY + tilesetH < _editorH / 2)
                {
                    _camera.Move(0, areaY + tilesetH - _editorH / 2);
                }
                else if (areaY > (_editorH / 2))
                {
                    _camera.Move(0, areaY - _editorH / 2);
                }
            }
            else
            {
                if (areaY < _editorY)
                {
                    _camera.Move(0, areaY + _editorY);
                }
                if (areaY + tilesetH > _editorH)
                {
                    _camera.Move(0, areaY + tilesetH - _editorH);
                }
                /*if (areaX < 0 || areaY < 0)
                {
                    _camera.Move(areaX < 0 ? areaX : 0, areaY < 0 ? areaY : 0);
                }
                if (areaX + tilesetW > _graphicsDevice.Viewport.Width || areaY + tilesetH > _graphicsDevice.Viewport.Height)
                {
                    _camera.Move(areaX + tilesetW > _graphicsDevice.Viewport.Width ? (areaX + tilesetW - _graphicsDevice.Viewport.Width) : 0, areaY + tilesetH > _graphicsDevice.Viewport.Height ? areaY + tilesetH - _graphicsDevice.Viewport.Height : 0);
                }*/
            }


            if (tilesetW > _graphicsDevice.Viewport.Width / 2)
            {
                if (areaX + tilesetW < _graphicsDevice.Viewport.Width / 2)
                {
                    _camera.Move(areaX + tilesetW - _graphicsDevice.Viewport.Width / 2, 0);
                }
                else if (areaX > (_graphicsDevice.Viewport.Width / 2))
                {
                    _camera.Move(areaX - _graphicsDevice.Viewport.Width / 2, 0);
                }
            }
            else
            {
                if (areaX < _editorX)
                {
                    _camera.Move(areaX - _editorX, 0);
                }
                if (areaX + tilesetW > _graphicsDevice.Viewport.Width)
                {
                    _camera.Move(areaX + tilesetW - _graphicsDevice.Viewport.Width, 0);
                }
            }
            
            //var cell = 1;
            //_camera.Teleport(Math2D.Snap(new Vector2(_camera.RawPosition.X, _camera.RawPosition.Y), cell).X, Math2D.Snap(new Vector2(_camera.RawPosition.X, _camera.RawPosition.Y), cell).Y);
        }
        
        private void Zoom(object sender, MouseScrollWheelValueChangeEventArgs e)
        {
            if (Input.Instance.IsMouseKeyDown(MouseButton.Middle))
            {
                return;
            }
            float oldWidth = _width * _camera.Scale;
            float oldHeight = _height * _camera.Scale;
            switch (e.Change)
            {
                case 120:
                    //UP
                    _camera.ChangeScaleBy(_camera.Scale * 0.25f);
                    //_camera.ChangeScaleBy(0.1f);
                    /*if (_camera.Scale < 1) {
                        _camera.SetScale(_camera.Scale + 0.1f * 1);
                    }
                    else {
                        _camera.SetScale(_camera.Scale + 0.1f * (int)_camera.Scale);
                    }*/
                    break;
                case -120:
                    //Down
                    //_camera.ChangeScaleBy(-0.1f);
                    /*if (_camera.Scale < 1) {
                        _camera.SetScale(_camera.Scale - 0.1f * 1);
                    }
                    else {
                        _camera.SetScale(_camera.Scale - 0.1f * (int)_camera.Scale);
                    }*/
                    _camera.ChangeScaleBy(-_camera.Scale * 0.25f);
                    break;
            }
            /*float newWidth = _width * _camera.Scale;
            float newHeight = _height * _camera.Scale;

            float diffWidth = (oldWidth - newWidth);
            float diffHeight = (oldHeight - newHeight);
            _camera.Move(diffWidth / 2, diffHeight / 2);*/
             _camera.SetScale((float) Math.Clamp(_camera.Scale, 0.01f, 10));
             Console.WriteLine(_camera.Scale);
            //TODO change offset with zoom
            //_offset -= (_offset.ToVector2() * _zoom).ToPoint();
        }
        
        private void Move(object sender, MouseMoveEventArgs e)
        {
            if (Input.Instance.IsMouseKeyDown(MouseButton.Middle))
            {
                _camera.Move(-e.DeltaX, -e.DeltaY);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            spriteBatch.Begin(transformMatrix: Matrix.CreateScale(_camera.Scale) * Matrix.CreateTranslation(-_camera.Position), effect: _effect);
            //spriteBatch.Begin(effect: _effect);

            spriteBatch.Draw(
                TextureContentLoader.Instance.Find("grid"),
                new Rectangle(new Point(0, 0), new Point(_tileWidth * _width, _tileHeight * _height)),
                null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            
            spriteBatch.End();
            
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateScale(_camera.Scale) * Matrix.CreateTranslation(-_camera.Position)); 
            spriteBatch.Draw(
            TextureContentLoader.Instance.Find("missing"),
            new Rectangle(new Point((int)(_camera.RawPosition.X / _camera.Scale), (int)(_camera.RawPosition.Y / _camera.Scale)), new Point((int)(_tileWidth / _camera.Scale), (int)(_tileHeight / _camera.Scale))),
            null, Color.White, 0f, TextureContentLoader.Instance.Find("missing").Bounds.Center.ToVector2(), SpriteEffects.None, 0f);
            
            //Mouse
            if (_drawMouse)
            {
                spriteBatch.Draw(
                    TextureContentLoader.Instance.Find("p_w"),
                    new Rectangle(Math2D.Snap(_tranformedMouse, _tileHeight).ToPoint(),
                        new Point(_tileWidth, _tileHeight)),
                    null, Color.White * 0.25f, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            }

            spriteBatch.End();
        }

        public void SetEditorPosition(int? x, int? y)
        {
            x ??= _editorX;
            _editorX = x.Value;
            y ??= _editorY;
            _editorY = y.Value;
        }
        
        public void SetEditorSize(int? x, int? y)
        {
            x ??= _editorW;
            _editorW = x.Value;
            y ??= _editorH;
            _editorH = y.Value;
        }
        
        public void SetEditorSize(Point size)
        {
            
        }
    }
}