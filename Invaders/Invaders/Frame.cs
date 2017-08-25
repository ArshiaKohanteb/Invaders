using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invaders
{
    class Frame
    {

        public Rectangle Bounds;
        public Vector2 Origin;

        public Frame(Rectangle bounds, Vector2 origin)
        {
            this.Bounds = bounds;
            this.Origin = origin;
        }

    }
}
