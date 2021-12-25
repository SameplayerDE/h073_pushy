using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace h073_pushy
{
    public class Stage
    {

        private Pushy _pushy;
        private List<Ball> _balls;
        private readonly int _width;
        private readonly int _height;

        private bool[,] _walls;

        public Stage(int w, int h, int startX = 0, int startY = 0)
        {
            _pushy = new Pushy(this);
            _balls = new List<Ball>();

           
            
            _pushy.Teleport(startX, startY);
            _walls = new bool[w, h];
            _width = w;
            _height = h;
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (x == 0 || x == w - 1 || y == 0 || y == h - 1)
                    {
                        _walls[x, y] = true;
                    }
                }
            }
        }

        public bool AddMovable(int x, int y)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height) return false;
            if (IsBlocked(x, y)) return false;
            
            var ball0 = new Ball(this);
            ball0.Teleport(x, y);
            _balls.Add(ball0);
            
            return true;
        }
        
        public bool SetStart(int x, int y)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height) return false;
            if (IsBlocked(x, y)) return false;
            _pushy.Teleport(x, y);
            return true;
        }
        
        public bool SetBlocked(int x, int y, bool value)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height) return false;
            _walls[x, y] = value;
            return true;
        }
        
        public bool IsBlocked(int x, int y)
        {
            return _walls[x, y];
        }
        
        public bool IsMovable(int x, int y)
        {
            for (var i = _balls.Count - 1; i >= 0; i--)
            {
                if (_balls[i].Position.X == x && _balls[i].Position.Y == y)
                {
                    return true;
                }
            }

            return false;
        }
        
        public Ball GetMovable(int x, int y)
        {
            for (var i = _balls.Count - 1; i >= 0; i--)
            {
                if (_balls[i].Position.X == x && _balls[i].Position.Y == y)
                {
                    return _balls[i];
                }
            }
            return null;
        }
        
        public void Update(GameTime gameTime)
        {
            _pushy.Update(gameTime);
            for (var i = _balls.Count - 1; i >= 0; i--)
            {
                _balls[i].Update(gameTime);
            }
        }
        
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    spriteBatch.Draw(TextureContentLoader.Instance.Find(IsBlocked(x, y) ? "wall" : "floor"), new Vector2(x, y) * 32, null, Color.White, 0f, new Vector2(16, 16), 1f, SpriteEffects.None, 0f);
                }
            }
            
            _pushy.Draw(spriteBatch, gameTime);
            for (var i = _balls.Count - 1; i >= 0; i--)
            {
                _balls[i].Draw(spriteBatch, gameTime);
            }
            
        }
        
    }
}