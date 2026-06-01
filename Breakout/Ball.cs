using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private int _ballLives, _bricksScore, _score;
        private bool _didBallFall, _hitBrick;


        public Ball(Texture2D texture, Rectangle rectangle, Vector2 speed, Color color)
        {
            _texture = texture;
            _rectangle = rectangle;
            _speed = speed;
            _color = color;
            _ballLives = 3;
            _didBallFall = true;
            _hitBrick = false;
        }

        public Rectangle Rect
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }

        public bool BallBool
        {
            get { return _didBallFall; }
            set { _didBallFall = value; }
        }
        
        public int Lives
        {
            get { return _ballLives; }
            set { _ballLives = value;}
        }

        public int Score
        {
            get { return _score; }
        }

        public void BallState(Rectangle window, Paddle paddle, SoundEffectInstance bounceInstance, List<Brick> bricks, KeyboardState keyboardState)
        {
            if (_didBallFall)
                BallLaunch(keyboardState, paddle);
            else
                BallMovement(window, paddle, bounceInstance, bricks);
        }

        public void ResetLocation(Paddle paddle)
        {
            _rectangle.X = paddle.Rect.Center.X - 12;
            _rectangle.Y = paddle.Rect.Y - 50;
        }

        public void BallLaunch(KeyboardState keyboardState, Paddle paddle)
        {
            if (_didBallFall)
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
                    _didBallFall = false;
                }
            }
        }

        // add List<Brick> bricks
        public void BallMovement(Rectangle window, Paddle paddle, SoundEffectInstance bounceInstance, List<Brick> bricks)
        {

            //_rectangle.Offset(_speed);
            // Horizontal Movement
            _rectangle.X += (int)_speed.X;

            // Keep in window
            if (_rectangle.Right > window.Width || _rectangle.Left < 0)
            {
                bounceInstance.Play();
                _rectangle.X -= (int)_speed.X;
                _speed.X *= -1;
            }
            if (_rectangle.Right > window.Width + 1 || _rectangle.Left < -1)
            {
                bounceInstance.Play();
                ResetLocation(paddle);
            }

            ////Hits Side of Paddle
            //if (_rectangle.Intersects(paddle.Rect))
            //{
            //    _speed.X *= -1;
            //    _rectangle.X -= (int)_speed.X;

            //}

            // Hitting a brick(s)
            _hitBrick = false;
            for (int i = 0; i < bricks.Count; i++)
            {
                if (_rectangle.Intersects(bricks[i].Rect))
                {
                    bounceInstance.Play();
                    _hitBrick = true;
                    bricks.RemoveAt(i);
                    _score += 50;
                    _bricksScore++;
                    i--;
                }
            }
            if (_hitBrick)
            {
                _speed.X *= -1;
            }



            // Vertical Movement
            // ball Vector2(3, 3)
            _rectangle.Y += (int)_speed.Y;

            // Keep on screen
            if (_rectangle.Top < 0)
            {
                bounceInstance.Play();
                _rectangle.Y -= (int)_speed.Y;
                _speed.Y *= -1;
            }
            // falls out of window
            if (_rectangle.Bottom > window.Height && _ballLives != 0)
            {
                _didBallFall = true;
                _ballLives--;
            }

            // more bricks
            _hitBrick = false;
            for (int i = 0; i < bricks.Count; i++)
            {
                if (_rectangle.Intersects(bricks[i].Rect))
                {
                    bounceInstance.Play();
                    _hitBrick = true;
                    _score += 50;
                    bricks.RemoveAt(i);
                    i--;
                }
                
            }
            if (_hitBrick)
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
                    bounceInstance.Play();
                    _rectangle.X -= (int)_speed.X;
                    _rectangle.Y -= (int)_speed.Y;
                    _speed.X = -3;
                    _speed.Y = -4;
                }
                if (_rectangle.Center.X > paddleLeft || _rectangle.Center.X < paddleRight)
                {
                    bounceInstance.Play();
                    _rectangle.X -= (int)_speed.X;
                    _rectangle.Y -= (int)_speed.Y;
                    _speed.X = -3;
                    _speed.Y = -3;
                }
                if (_rectangle.Center.X > paddleRight)
                {
                    bounceInstance.Play();
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

        public int Size
        {
            get { return _rectangle.Width; }
            set 
            { 
                _rectangle.Width = value;
                _rectangle.Height = value;
            }
        }

    }
}
