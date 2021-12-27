using System;
using editor.UI;
using HxGraphics;
using HxInput;
using HxSystem;
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

        private Spritesheet _sheet;
        private HScrollBar _scroll;

        public Game1()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnWindowSizeChange;

            _graphics.SynchronizeWithVerticalRetrace = false; //Vsync
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(1000.0f / TargetFps);
        }

        private void OnWindowSizeChange(object? sender, EventArgs e)
        {
            _editorTarget.SetEditorSize(Window.ClientBounds.Width - 32, Window.ClientBounds.Height);
        }

        protected override void Initialize()
        {
            Hx.Instance.Init(this, _graphics, Framework.DesktopGL);
            
            _camera = new Camera(_graphics);
            Graphics.Instance.SetGraphicsDeviceManager(_graphics);
            _scroll = new HScrollBar(new Point(0, 0), new Point(16, Hx.Instance.ViewportHeight));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _editorTarget = new EditorTarget(GraphicsDevice, _camera, 64, 64);
            _userInterfaceTarget = new UserInterfaceTarget(GraphicsDevice);
            // ReSharper disable once HeapView.DelegateAllocation
            _userInterfaceTarget.OnTileChange += OnTileSelectionChange;
            _userInterfaceTarget.OnSelectionChangeSize += OnSelectionSizeChange;

            TextureContentLoader.Instance.LoadContent(Content);
            NineTileResourceLoader.Instance.LoadResources();
            EffectContentLoader.Instance.LoadContent(Content);
            SpriteFontContentLoader.Instance.LoadContent(Content);

            _sheet = new Spritesheet(GraphicsDevice, TextureContentLoader.Instance.Find("atlas"), 16, 16);
                
            base.LoadContent();
        }
        
        private void OnTileSelectionChange(object sender, TileChangeEventArgs args)
        {
            _editorTarget.TileSelection = args.Tile;
        }
        
        private void OnSelectionSizeChange(object sender, SelectionSizeChangeEventArgs args)
        {
            _editorTarget.SetEditorPosition(args.Size, null);
        }

        protected override void Update(GameTime gameTime)
        {
            if (!IsActive)
            {
                return;
            }
            
            Input.Instance.Update(gameTime);
            _scroll.Update(gameTime);

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
            _userInterfaceTarget.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            if (!IsActive)
            {
                return;
            }

            GraphicsDevice.Clear(CColor.Dark);

            //scene
            _editorTarget.Draw(_spriteBatch, gameTime);
            
            //ui
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _userInterfaceTarget.Draw(_spriteBatch, gameTime);
            _scroll.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}