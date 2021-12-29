using System;
using System.Windows.Forms;
using h073_pushy.Items;
using HxInput;
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
        private TextureCollection col;

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

            var door = new Door(4, 0);
            var @switch = new Switch(4, 5, door);
            
            _stage.AddStageObject(door);
            _stage.AddStageObject(@switch);

            var key = new InventoryObject(3, 3, new Key());

            _stage.AddInventoryObject(key);

            var table = new Table(1, 1);
            
            _stage.AddStageObject(table);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            TextureContentLoader.Instance.LoadContent(Content);
            SoundEffectContentLoader.Instance.LoadContent(Content);
            EffectContentLoader.Instance.LoadContent(Content);
            
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


            EffectContentLoader.Instance.Find("gba").Parameters["ElapsedSeconds"]?.SetValue((float)gameTime.ElapsedGameTime.TotalSeconds);
            EffectContentLoader.Instance.Find("gba").Parameters["TotalSeconds"]?.SetValue((float)gameTime.TotalGameTime.TotalSeconds);
            EffectContentLoader.Instance.Find("gba").Parameters["TotalMilliseconds"]?.SetValue((float)gameTime.TotalGameTime.TotalMilliseconds);

            base.Update(gameTime);
        }
        
        
        protected override void Draw(GameTime gameTime)
        {
            
            if (!IsActive)
            {
                return;
            }

            GraphicsDevice.Clear(new Color(32, 70, 49));

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, transformMatrix: Matrix.CreateScale(_camera.Scale) * Matrix.CreateTranslation(-_camera.Position), effect: EffectContentLoader.Instance.Find("default"));
            _stage.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();
            
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            var scale = 4;
            for (var i = 0; i < _stage.Pushy.Inventory.MaxSize; i++)
            {
                _spriteBatch.Draw(TextureContentLoader.Instance.Find("inventory_slot"),
                    new Rectangle(new Point(16 * scale * i + 8, 8), new Point(16 * scale, 16 * scale)), Color.White);
                if (_stage.Pushy.Inventory.Content[i] != null)
                {
                    _spriteBatch.Draw(TextureContentLoader.Instance.Find(_stage.Pushy.Inventory.Content[i].StageTexture),
                        new Rectangle(new Point(16 * scale * i + 8, 8), new Point(16 * scale, 16 * scale)),
                        Color.White);
                }
            }

            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}