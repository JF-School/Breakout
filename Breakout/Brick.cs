using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    public class Brick
    {
        private Rectangle _location;
        private Rectangle _size;
        private Rectangle _collision;
        private Texture2D _texture;
        private Color _color;

        public Brick(Texture2D texture, Rectangle location)
        {
            _texture = texture;
            _location = location;
        }
    }
}
