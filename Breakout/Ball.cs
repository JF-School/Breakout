using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        private int ballFalls;
        private bool didBallFall;

        public Ball(Texture2D texture, Rectangle rectangle, Vector2 speed, Color color)
        {
            _texture = texture;
            _rectangle = rectangle;
            _speed = speed;
            _color = color;
            ballFalls = 0;
            didBallFall = false;
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
            // window
            _rectangle.X += (int)_speed.X;
            if (_rectangle.Right > window.Width || _rectangle.Left < 0)
            {
                _rectangle.X -= (int)_speed.X;
                _speed.X *= -1;
            }
            if (_rectangle.Bottom > window.Height && ballFalls < 4)
            {
                didBallFall = true;
                ballFalls += 1;
            }


            // ball Vector2(3, 3)
            _rectangle.X += (int)_speed.X;
            _rectangle.Y += (int)_speed.Y;
            if (_rectangle.Intersects(paddle.Rect))
            {
                int paddleLeft = (paddle.Rect.Center.X) / 2;
                int paddleRight = (paddle.Rect.Center.X + paddle.Rect.Right) / 2;
                if (_rectangle.Center.X < paddleLeft)
                {
                    _rectangle.X -= (int)_speed.X;
                    _rectangle.Y -= (int)_speed.Y;
                    _speed.X = -3;
                    _speed.Y = -2;
                }
                if (_rectangle.Center.X > paddleLeft || _rectangle.Center.X < paddleRight)
                {
                    _rectangle.X -= (int)_speed.X;
                    _rectangle.Y -= (int)_speed.Y;
                    _speed.X = -2;
                    _speed.Y = -2;
                }
                if (_rectangle.Center.X > paddleRight)
                {
                    _rectangle.X -= (int)_speed.X;
                    _rectangle.Y -= (int)_speed.Y;
                    _speed.X = 2;
                    _speed.Y = -3;
                }
                
                //_rectangle.Y -= 1;
                //_rectangle.Y -= (int)_speed.Y;
                //_speed.Y *= -1;

            }
            
            if (_rectangle.Top < 0)
            {
                _rectangle.Y -= (int)_speed.Y;
                _speed.Y *= -1;
            }

            for (int i = 0; i < bricks.Count; i++)
            {
                if (_rectangle.Intersects(bricks[i].Rect))
                {
                    if (_rectangle.Bottom > bricks[i].Rect.Top || _rectangle.Top <  bricks[i].Rect.Bottom)
                    {
                        _speed.Y *= -1;
                    }   
                    if (_rectangle.Left > bricks[i].Rect.Right || _rectangle.Right < bricks[i].Rect.Left)
                    {
                        _speed.X *= -1;
                    }
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
