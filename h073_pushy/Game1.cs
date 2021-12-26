using System;
using System.Windows.Forms;
using HxInput;
using HxSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace h073_pushy
{
    public class Game1 : Game
    {

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Camera _camera;

        private Stage _stage;

        public Game1()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            
            Hx.Instance.Init(this, _graphics, Framework.DesktopGL);
      
            _camera = new Camera(_graphics);
            _stage = StageLoader.LoadFromFile("stagexxx.stage");
            _stage.Camera = _camera;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            TextureContentLoader.Instance.LoadContent(Content);
            
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (!IsActive)
            {
                return;
            }
            Hx.Instance.Update(gameTime);

            _stage.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            if (!IsActive)
            {
                return;
            }

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: Matrix.CreateScale(_camera.Scale) * Matrix.CreateTranslation(-_camera.Position));
            _stage.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}