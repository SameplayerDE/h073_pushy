using System;
using HxGraphics;
using HxInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace editor
{
    public class Game1 : Game
    {

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private EditorTarget _editorTarget;
        private UserInterfaceTarget _userInterfaceTarget;
        private Camera _camera;
        private const int TargetFps = 60;

        public Game1()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            
            _graphics.SynchronizeWithVerticalRetrace = false; //Vsync
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(1000.0f / TargetFps);
        }

        protected override void Initialize()
        {
            _camera = new Camera(_graphics);
            Graphics.Instance.SetGraphicsDeviceManager(_graphics);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _editorTarget = new EditorTarget(GraphicsDevice, _camera, 10, 10);
            _userInterfaceTarget = new UserInterfaceTarget(GraphicsDevice);

            TextureContentLoader.Instance.LoadContent(Content);
            EffectContentLoader.Instance.LoadContent(Content);
            
            base.LoadContent();
        }
        
        private void OnTileSelectionChange(object? sender, TileChangeEventArgs args)
        {
            _editorTarget.TileSelection = args.Tile;
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (!IsActive)
            {
                return;
            }
            
            Input.Instance.Update(gameTime);

            var direction = Vector2.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                direction.X += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                direction.Y -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                direction.X -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                direction.Y += 1;
            }

            if (direction.Length() != 0)
            {
                direction.Normalize();
                direction *= 100f;
                direction *= (float)gameTime.ElapsedGameTime.TotalSeconds;

                Window.Title = "" + direction;
                _camera.Move(direction.X, direction.Y);
            }
            
            _editorTarget.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            if (!IsActive)
            {
                return;
            }

            GraphicsDevice.Clear(Color.CornflowerBlue);

            //scene
            _editorTarget.Draw(_spriteBatch, gameTime);
            

            //ui
            _spriteBatch.Begin();
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}