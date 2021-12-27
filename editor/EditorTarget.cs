using System;
using HxInput;
using HxInput.Events;
using HxMath;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        private Point _tranformedMouse;
        private Effect _effect; 
        
        
        public int Width => _width;
        public int Height => _height;
        public int[,] Tiles => _tiles;
        

        public EditorTarget(GraphicsDevice graphicsDevice, Camera camera, int width = 64, int height = 64)
        {
            _graphicsDevice = graphicsDevice;
            _camera = camera;
            _width = width;
            _height = height;
            _tiles = new int[_width, _height];
            Input.Instance.OnMouseScrollWheelValueChange += Zoom;
            Input.Instance.OnMouseMove += Move;
        }

        public void Update(GameTime gameTime)
        {
            _effect ??= EffectContentLoader.Instance.Find("texture");
            
            //_effect.Parameters["Overlay"].SetValue(TextureContentLoader.Instance.Find("grid"));
            _effect.Parameters["Width"].SetValue(_width);
            _effect.Parameters["Height"].SetValue(_height);
            
            if (_effect.Parameters["ElapsedSeconds"] != null)
            {
                _effect.Parameters["ElapsedSeconds"].SetValue((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
           
            if (_effect.Parameters["TotalSeconds"] != null)
            {
                _effect.Parameters["TotalSeconds"].SetValue((float)gameTime.TotalGameTime.TotalSeconds);
            }
            
            if (_effect.Parameters["TotalMilliseconds"] != null)
            {
                _effect.Parameters["TotalMilliseconds"].SetValue((float)gameTime.TotalGameTime.TotalMilliseconds);
            }
 
            _tranformedMouse = Math2D.InverseTransform(Input.Instance.LatestMousePosition, Matrix.CreateScale(_camera.Scale) * Matrix.CreateTranslation(-_camera.Position)).ToPoint();
        }
        
        private void Zoom(object sender, MouseScrollWheelValueChangeEventArgs e)
        {
            if (Input.Instance.IsMouseKeyDown(MouseButton.Middle))
            {
                return;
            }
            switch (e.Change)
            {
                case 120:
                    //UP
                    _camera.ChangeScaleBy(1);
                    break;
                case -120:
                    //Down
                    _camera.ChangeScaleBy(-1);
                    break;
            }
            _camera.SetScale(Math.Clamp(_camera.Scale, 1, 10));;
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
            //_effect.Parameters["Texture"].SetValue(TextureContentLoader.Instance.Find("grid"));
            
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, transformMatrix: Matrix.CreateScale(_camera.Scale) * Matrix.CreateTranslation(-_camera.Position), effect: _effect);

            spriteBatch.Draw(
                TextureContentLoader.Instance.Find("grid"),
                new Rectangle(new Point(0, 0), new Point(_tileWidth * _width, _tileHeight * _height)),
                null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            
            spriteBatch.End();
            
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateScale(_camera.Scale) * Matrix.CreateTranslation(-_camera.Position));
            //spriteBatch.Draw(
             //   TextureContentLoader.Instance.Find("missing"),
              //  new Rectangle(new Point(0, 0), new Point(_tileWidth, _tileHeight)),
               // null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            
            //Mouse
            spriteBatch.Draw(
                TextureContentLoader.Instance.Find("p_w"),
                new Rectangle(Math2D.Snap(_tranformedMouse, _tileHeight).ToPoint(), new Point(_tileWidth, _tileHeight)),
                null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}