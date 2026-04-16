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
    public class Brick
    {
        private Rectangle _rectangle;
        private Texture2D _texture;
        private Color _color;
        private float _opacity;

        public Brick(Texture2D texture, Rectangle rectangle, Color color)
        {
            _texture = texture;
            _rectangle = rectangle;
            _color = color;
            _opacity = 1;
        }

        public Rectangle Rect
        {
            get { return _rectangle; }
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectangle, _color * _opacity);
        }

    }
}
