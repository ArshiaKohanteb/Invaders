using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Invaders
{
    class Player : Sprite
    {
        //List<Bullet>

        public Player(Texture2D image, Vector2 position, Color color, Rectangle sourceRectangle)
            : base(image, position, color, sourceRectangle)
        {
        }

        //update which moves the character


    }
}
