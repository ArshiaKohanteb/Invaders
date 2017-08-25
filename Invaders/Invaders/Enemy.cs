using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Invaders
{
    class Enemy:Sprite
    {
        public Enemy(Texture2D image, Vector2 position, Color color, Rectangle sourceRectangle)
            : base(image, position, color, sourceRectangle)
        {
        }
    }
}
