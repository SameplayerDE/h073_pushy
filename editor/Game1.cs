using HxInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace editor
{
    public class Game1 : Game
    {

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (!IsActive)
            {
                return;
            }
            
            Input.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            if (!IsActive)
            {
                return;
            }

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}