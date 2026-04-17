using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    public class Ball
    {
        private Texture2D _texture;
        private Rectangle _rectangle;
        private Color _color;
        private Vector2 _speed;

        public Ball(Texture2D texture, Rectangle rectangle, Vector2 speed, Color color)
        {
            _texture = texture;
            _rectangle = rectangle;
            _speed = speed;
            _color = color;
        }

        public Rectangle Rect
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }
        
        // add List<Brick> bricks
        public void Update(Rectangle window, Paddle paddle, List<Brick> bricks)
        {
            //_rectangle.Offset(_speed);

            //_speed.X += (int)_speed.Y;
            //_speed.Y += (int)_speed.X;
            // window
            _rectangle.X += (int)_speed.X;
            if (_rectangle.Right > window.Width || _rectangle.Left < 0)
            {
                _rectangle.X -= (int)_speed.X;
                _speed.X *= -1;
            }
            _rectangle.Y += (int)_speed.Y;
            if (_rectangle.Bottom > paddle.Rect.Top || _rectangle.Top < 0)
            {
                _rectangle.Y -= (int)_speed.Y;
                _speed.Y *= -1;
            }
                

            for (int i = 0; i < bricks.Count; i++)
            {
                if (_rectangle.Intersects(bricks[i].Rect))
                {
                    if (_rectangle.Bottom > bricks[i].Rect.Top || _rectangle.Top <  bricks[i].Rect.Bottom)
                        _speed.Y *= -1;
                    if (_rectangle.Left > bricks[i].Rect.Right || _rectangle.Right < bricks[i].Rect.Left)
                        _speed.X *= -1;
                    bricks.RemoveAt(i);
                }
            }
            

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectangle, Color.White);
        }

    }
}
