using Microsoft.Xna.Framework;

namespace editor
{
    public class Camera
    {
        private Vector3 _position = Vector3.Zero;
        private float _scale = 1f;
        public Vector3 Position => _position;
        public float Scale => _scale;

        public void Move(int x, int y, int z = 0)
        {
            _position.X += x;
            _position.Y += y;
            _position.Z += z;
        }

        public void Move(float x, float y, float z = 0f)
        {
            _position.X += x;
            _position.Y += y;
            _position.Z += z;
        }
        
        public void ChangeScaleBy(float amount, bool rel = false)
        {
            if (rel)
            {
                _scale += amount * _scale;
                return;
            }
            _scale += amount;
        }
        
        public void SetScale(float scale)
        {
            _scale = scale;
        }
        
    }
}