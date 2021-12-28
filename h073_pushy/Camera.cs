using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace h073_pushy
{
    public class Camera
    {
        private Vector3 _position = Vector3.Zero;
        private float _scale = 2.5f;
        private GraphicsDevice _graphicsDevice;

        public Camera(GraphicsDeviceManager graphics)
        {
            _graphicsDevice = graphics.GraphicsDevice;
        }

        public Vector3 Position => _position - new Vector3(_graphicsDevice.Viewport.Bounds.Center.ToVector2(), 0);
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
        
        public void Teleport(float x, float y, float z = 0f)
        {
            _position.X = x;
            _position.Y = y;
            _position.Z = z;
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