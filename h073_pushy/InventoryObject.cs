using System;
using Microsoft.Xna.Framework;

namespace h073_pushy
{
    public abstract class InventoryObject
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
    }
}