using System;
using System.Windows.Forms;
using h073_pushy.Items;
using HxInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace h073_pushy
{
    public class Pushy : IInventoryHolder
    {
        private InventoryItem _mainHand;
        private int _mainHandSlot = -1;
        private string _textureKey;
        private Texture2D _texture = null;
        private Vector2 _position;
        private Vector2 _destination;

        private Inventory _inventory;
        private Direction _direction = Direction.Up;
        private Stage _stage = null;

        public bool FixRotation = true;
        private bool _smooth = false;

        public int X => (int)_position.X;
        public int Y => (int)_position.Y;
        
        public float Xf => _position.X;
        public float Yf => _position.Y;

        public Inventory Inventory => _inventory;
        public InventoryItem MainHand => _mainHand;
        public int MainHandSlot => _mainHandSlot;
        
        public Pushy(Stage stage)
        {
            _stage = stage;
            _inventory = new Inventory(this);
            _textureKey = "pushy";
            _mainHand = null;
        }
        
        public void Teleport(Point amount)
        {
            Teleport(amount.X, amount.Y);
        }
        
        public void Teleport(int x, int y)
        {
            _position.X = x;
            _position.Y = y;
            _destination.X = x;
            _destination.Y = y;
        }
        
        public void Move(Point amount)
        {
            Move(amount.X, amount.Y);
        }
        
        public void Move(int x, int y)
        {

            if (_mainHandSlot != -1)
            {
                //UseItem
                if (_stage.IsStageObject(_position.X + x, _position.Y + y))
                {
                    if (_stage.GetStageObject(_position.X + x, _position.Y + y) is Door { IsOpen: false } door)
                    {
                        //IsDoor
                        door.IsOpen = true;
                        Inventory.Remove(_mainHandSlot);
                    }
                }
                _mainHandSlot = -1;
                return;
            }
            
            if (_smooth)
            {
                if (_stage.IsBlocked(_destination.X + x, _destination.Y + y)) return;
                if (!_stage.IsMovable(_destination.X + x, _destination.Y + y))
                {
                    if (_stage.IsInventoryObject(_destination.X + x, _destination.Y + y))
                    {
                        var item = _stage.GetInventoryObject(_destination.X + x, _destination.Y + y);
                        if (item != null)
                        {
                            Inventory.Add(item.InventoryItem);
                            _stage.RemoveInventoryObject(item);
                            SoundEffectContentLoader.Instance.Find("pickup").Play();
                        }
                    }
                    _destination.X += x;
                    _destination.Y += y;
                    //_position.X += x;
                    //_position.Y += y;
                }
                else
                {
                    //Collides With Movable
                    var movable = _stage.GetMovable(_destination.X + x, _destination.Y + y);
                    if (!movable.Move(x, y)) return;
                    _destination.X += x;
                    _destination.Y += y;
                    //_position.X += x;
                    //_position.Y += y;
                }
            }
            else
            {
                if (_stage.IsBlocked(_position.X + x, _position.Y + y)) return;
                if (!_stage.IsMovable(_position.X + x, _position.Y + y))
                {
                    if (_stage.IsInventoryObject(_position.X + x, _position.Y + y))
                    {
                        var item = _stage.GetInventoryObject(_position.X + x, _position.Y + y);
                        if (item != null)
                        {
                            Inventory.Add(item.InventoryItem);
                            _stage.RemoveInventoryObject(item);
                            SoundEffectContentLoader.Instance.Find("pickup").Play();
                        }
                    }
                    _position.X += x;
                    _position.Y += y;
                }
                else
                {
                    //Collides With Movable
                    var movable = _stage.GetMovable(_position.X + x, _position.Y + y);
                    if (!movable.Move(x, y)) return;
                    _position.X += x;
                    _position.Y += y;
                }
            }
            
        }

        public void Update(GameTime gameTime)
        {
            _texture ??= TextureContentLoader.Instance.Find(_textureKey);

            if (Input.Instance.IsKeyboardKeyDownOnce(Keys.D1))
            {
                _mainHand = Inventory.Content[0];
                if (_mainHand != null)
                {
                    _mainHandSlot = 0;
                }
            }
            if (Input.Instance.IsKeyboardKeyDownOnce(Keys.D2))
            {
                _mainHand = Inventory.Content[1];
                if (_mainHand != null)
                {
                    _mainHandSlot = 1;
                }
            }
            if (Input.Instance.IsKeyboardKeyDownOnce(Keys.D3))
            {
                _mainHand = Inventory.Content[2];
                if (_mainHand != null)
                {
                    _mainHandSlot = 2;
                }
            }
            
            if (Input.Instance.IsKeyboardKeyDownOnce(Keys.Left))
            {
                Move(-1, 0);
                _direction = Direction.Left;
            }
            else if (Input.Instance.IsKeyboardKeyDownOnce(Keys.Down))
            {
                Move(0, 1);
                _direction = Direction.Down;
            }
            else if (Input.Instance.IsKeyboardKeyDownOnce(Keys.Right))
            {
                Move(1, 0);
                _direction = Direction.Right;
            }
            else if (Input.Instance.IsKeyboardKeyDownOnce(Keys.Up))
            {
                Move(0, -1);
                _direction = Direction.Up;
            }

            if (_smooth)
            {
                _position.X = MathHelper.Lerp(_position.X, _destination.X, 0.5f);
                _position.Y = MathHelper.Lerp(_position.Y, _destination.Y, 0.5f);
            }
        }
        
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(_texture, _destination * 32f, null, Color.Black, FixRotation ? 0f : _direction.ToRotation(), new Vector2(16, 16), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(_texture, _position * 32f, null, Color.White, FixRotation ? 0f : _direction.ToRotation(), new Vector2(16, 16), 1f, SpriteEffects.None, 0f);
            if (_mainHandSlot != -1)
                spriteBatch.Draw(TextureContentLoader.Instance.Find(_mainHand.StageTexture), _position * 32f + new Vector2(0, -16f), null, Color.White, MathHelper.ToRadians(10f * (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 10)), new Vector2(16, 16), 1f, SpriteEffects.None, 0f);
        }
    }
}