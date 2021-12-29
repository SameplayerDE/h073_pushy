using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace h073_pushy
{
    public class InventoryObject
    {
        protected string _textureKey = string.Empty;
        protected Point _position;
        protected Color _color;
        protected Direction _direction;
        protected Stage _stage = null;
        protected InventoryItem _item;

        public InventoryObject(int x, int y, InventoryItem item)
        {
            _item = item ?? throw new NullReferenceException();
            _position.X = x;
            _position.Y = y;
            _color = Color.White;
            _direction = Direction.Up;
        }

        public void SetStage(Stage stage)
        {
            _stage = stage;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(TextureContentLoader.Instance.Find("missing"), _position.ToVector2() * 32f, null, _color, _direction.ToRotation(), new Vector2(16, 16), 1f, SpriteEffects.None, 0f);
        }
    }
}