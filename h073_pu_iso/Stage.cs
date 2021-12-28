using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace h073_pu_iso
{
    public class Stage
    {

        private Pushy _pushy;
        private List<Ball> _balls;
        private List<Parser> _parsers;
        private List<StageObject> _stageObjects;
        private readonly int _width;
        private readonly int _height;
        private Point _end;

        private bool[,] _walls;

        public Pushy Pushy => _pushy;
        public int  Width => _width;
        public int  Height => _height;
        public Camera Camera;

        public Stage(int w, int h, int startX = 0, int startY = 0)
        {
            _pushy = new Pushy(this);
            _stageObjects = new List<StageObject>();
            _balls = new List<Ball>();
            _parsers = new List<Parser>();
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

        public void AddStageObject(StageObject stageObject)
        {
            stageObject.SetStage(this);
            _stageObjects.Add(stageObject);
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
        
        public bool AddParser(int x, int y, Color color)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height) return false;
            if (IsBlocked(x, y)) return false;
            
            var parser = new Parser(this);
            parser.SetColor(color);
            parser.Teleport(x, y);
            _parsers.Add(parser);
            
            return true;
        }
        
        public bool SetStart(int x, int y)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height) return false;
            if (IsBlocked(x, y)) return false;
            _pushy.Teleport(x, y);
            return true;
        }
        
        public bool SetEnd(int x, int y)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height) return false;
            if (IsBlocked(x, y)) return false;
            _end = new Point(x, y);
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
            var result = false;
            for (var i = _stageObjects.Count - 1; i >= 0; i--)
            {
                if (_stageObjects[i].Position == new Point(x, y))
                {
                    if (_stageObjects[i].IsBlocking)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return IsBlockedByWall(x, y) || result;
        }
        
        private bool IsBlockedByWall(int x, int y)
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
        
        public bool IsParser(int x, int y)
        {
            for (var i = _parsers.Count - 1; i >= 0; i--)
            {
                if (_parsers[i].Position.X == x && _parsers[i].Position.Y == y)
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
            for (var i = _parsers.Count - 1; i >= 0; i--)
            {
                _parsers[i].Update(gameTime);
            }
            for (var i = _balls.Count - 1; i >= 0; i--)
            {
                _balls[i].Update(gameTime);
            }
            for (var i = _stageObjects.Count - 1; i >= 0; i--)
            {
                _stageObjects[i].Update(gameTime);
            }
            Camera.Teleport(Pushy.X * 32, Pushy.Y * 32);
        }
        
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    spriteBatch.Draw(SpritesheetResourceLoader.Instance.Find("iso").Sprites[IsBlockedByWall(x, y) ? 0 : 1], /*new Vector2(x, y) * 32*/Utils.ToIsometric(x, y).ToVector2(), null, Color.White, 0f, new Vector2(16, 16), 1f, SpriteEffects.None, 0f);
                }
            }
            
            spriteBatch.Draw(TextureContentLoader.Instance.Request("house").Result, _end.ToVector2() * 32, null, Color.White, 0f, new Vector2(16, 16), 1f, SpriteEffects.None, 0f);
            for (var i = _parsers.Count - 1; i >= 0; i--)
            {
                _parsers[i].Draw(spriteBatch, gameTime);
            }
            for (var i = _balls.Count - 1; i >= 0; i--)
            {
                _balls[i].Draw(spriteBatch, gameTime);
            }
            for (var i = _stageObjects.Count - 1; i >= 0; i--)
            {
                _stageObjects[i].Draw(spriteBatch, gameTime);
            }
            _pushy.Draw(spriteBatch, gameTime);
        }
        
    }
}