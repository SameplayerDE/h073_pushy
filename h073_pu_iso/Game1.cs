using System;
using System.Windows.Forms;
using HxInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace h073_pu_iso
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
            _camera = new Camera(_graphics);
            _stage = StageLoader.LoadFromFile("stagexxx.stage");
            _stage.Camera = _camera;
            _stage.AddStageObject(new Door(4, 0));
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            TextureContentLoader.Instance.LoadContent(Content);

            SpritesheetResourceLoader.Instance.Add("default",
                new Spritesheet(GraphicsDevice, TextureContentLoader.Instance.Find("atlas"), 64, 32), true);
            
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (!IsActive)
            {
                return;
            }
            Input.Instance.Update(gameTime);

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

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, transformMatrix: Matrix.CreateScale(_camera.Scale) * Matrix.CreateTranslation(-_camera.Position));
            _stage.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}