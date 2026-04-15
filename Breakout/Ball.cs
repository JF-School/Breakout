using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    public class Ball
    {
        private Texture2D _texture;
        private Rectangle _location;
        private Rectangle _size;
        private Rectangle _collision;
        private Color _color;
        private Vector2 _speed;

        public Ball(Texture2D texture, Rectangle location, Vector2 speed)
        {
            _texture = texture;
            _location = location;
            _speed = speed;
        }

    }
}
