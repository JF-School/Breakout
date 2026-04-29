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
        private int ballResets, ballLives, score;
        private bool didBallFall, hitBrick;


        public Ball(Texture2D texture, Rectangle rectangle, Vector2 speed, Color color)
        {
            _texture = texture;
            _rectangle = rectangle;
            _speed = speed;
            _color = color;
            ballLives = 3;
            didBallFall = true;
            hitBrick = false;
        }

        public Rectangle Rect
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }

        public Vector2 Speed
        {
            get { return _speed; }
        }

        public bool BallBool
        {
            get { return didBallFall; }
            set { didBallFall = value; }
        }
        
        public int Lives
        {
            get { return ballLives; }
            set { ballLives = value;}
        }

        public int Score
        {
            get { return score; }
        }

        public void BallState(Rectangle window, Paddle paddle, List<Brick> bricks, KeyboardState keyboardState)
        {
            //do
            //{
            //    BallLaunch(keyboardState, paddle);
            //} while (didBallFall == true);
            //do
            //{
            //    BallMovement(window, paddle, bricks);
            //} while (didBallFall == false);

            if (didBallFall)
                BallLaunch(keyboardState, paddle);
            else
                BallMovement(window, paddle, bricks);
        }

        public void ResetLocation(Paddle paddle)
        {
            _rectangle.X = paddle.Rect.Center.X - 12;
            _rectangle.Y = paddle.Rect.Y - 50;
        }

        public void BallLaunch(KeyboardState keyboardState, Paddle paddle)
        {
            if (didBallFall)
            {
                ResetLocation(paddle);
                _speed.X *= 0;
                _speed.Y *= 0;
                if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
                {
                    _rectangle.X -= (int)_speed.X;
                    _rectangle.Y -= (int)_speed.Y;
                    //_speed.X = -3;
                    _speed.Y = -5;
                    didBallFall = false;
                }
            }
        }

        // add List<Brick> bricks
        public void BallMovement(Rectangle window, Paddle paddle, List<Brick> bricks)
        {

            //_rectangle.Offset(_speed);
            // Horizontal Movement
            _rectangle.X += (int)_speed.X;

            // Keep in window
            if (_rectangle.Right > window.Width || _rectangle.Left < 0)
            {
                _rectangle.X -= (int)_speed.X;
                _speed.X *= -1;
            }
            if (_rectangle.Right > window.Width + 1 || _rectangle.Left < -1)
            {
                ResetLocation(paddle);
            }

            ////Hits Side of Paddle
            //if (_rectangle.Intersects(paddle.Rect))
            //{
            //    _speed.X *= -1;
            //    _rectangle.X -= (int)_speed.X;

            //}

            // Hitting a brick(s)
            hitBrick = false;
            for (int i = 0; i < bricks.Count; i++)
            {
                if (_rectangle.Intersects(bricks[i].Rect))
                {
                    hitBrick = true;
                    bricks.RemoveAt(i);
                    score += 50;
                    i--;
                }
            }
            if (hitBrick)
            {
                _speed.X *= -1;
            }



            // Vertical Movement
            // ball Vector2(3, 3)
            _rectangle.Y += (int)_speed.Y;

            // Keep on screen
            if (_rectangle.Top < 0)
            {
                _rectangle.Y -= (int)_speed.Y;
                _speed.Y *= -1;
            }
            // falls out of window
            if (_rectangle.Bottom > window.Height && ballLives != 0)
            {
                didBallFall = true;
                ballLives--;
            }

            // more bricks
            hitBrick = false;
            for (int i = 0; i < bricks.Count; i++)
            {
                if (_rectangle.Intersects(bricks[i].Rect))
                {
                    hitBrick = true;
                    score += 50;
                    bricks.RemoveAt(i);
                    i--;
                }
                
            }
            if (hitBrick)
            {
                _speed.Y *= -1;
            }

            // Resolve Paddle Collision

            if (_rectangle.Intersects(paddle.Rect))
            {
                int paddleLeft = (paddle.Rect.Center.X) / 2;
                int paddleRight = (paddle.Rect.Center.X + paddle.Rect.Right) / 2;
                if (_rectangle.Center.X < paddleLeft)
                {
                    _rectangle.X -= (int)_speed.X;
                    _rectangle.Y -= (int)_speed.Y;
                    _speed.X = -3;
                    _speed.Y = -4;
                }
                if (_rectangle.Center.X > paddleLeft || _rectangle.Center.X < paddleRight)
                {
                    _rectangle.X -= (int)_speed.X;
                    _rectangle.Y -= (int)_speed.Y;
                    _speed.X = -3;
                    _speed.Y = -3;
                }
                if (_rectangle.Center.X > paddleRight)
                {
                    _rectangle.X -= (int)_speed.X;
                    _rectangle.Y -= (int)_speed.Y;
                    _speed.X = 4;
                    _speed.Y = -3;
                }
                
                //_rectangle.Y -= 1;
                //_rectangle.Y -= (int)_speed.Y;
                //_speed.Y *= -1;

            }
            
            // Hit a brick on the sides
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectangle, Color.White);
        }

    }
}
