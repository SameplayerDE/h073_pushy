using Microsoft.Xna.Framework;

namespace h073_pushy
{
    public abstract class StageObject
    {
        private string _textureKey;
        private Point _position;
        private Color _color;
        private Direction _direction;
        private Stage _stage = null;
    }
}