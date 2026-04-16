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
            _rectangle.Offset(_speed);
            if (_rectangle.Right > window.Width || _rectangle.Left < 0)
                _speed.X *= -1;
            if (_rectangle.Intersects(paddle.Rect) || _rectangle.Top < 0)
                _speed.Y *= -1;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectangle, Color.White);
        }

    }
}
