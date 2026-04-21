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
    public class Paddle
    {
        private Texture2D _texture;
        private Rectangle _rectangle;
        private Rectangle _window;
        private Vector2 _speed;

        public Paddle(Texture2D texture, Rectangle rectangle, Rectangle window)
        {
            _texture = texture;
            _rectangle = rectangle;
            _window = window;
            _speed = Vector2.Zero;
        }

        public Rectangle Rect
        {
            get { return _rectangle; }
        }

        public Vector2 Speed
        {
            get { return _speed; }
        }

        public void Update(KeyboardState keyboardState, Rectangle window)
        {
            keyboardState = Keyboard.GetState();
            _speed = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
                _speed.X -= 5;
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                _speed.X += 5;


            if (_rectangle.Right > window.Width)
                _rectangle.X = (window.Width - _rectangle.Width);
            if (_rectangle.Left < 0)
                _rectangle.X = 0;
            _rectangle.Offset(_speed);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectangle, Color.White);
        }
        
    }
}
