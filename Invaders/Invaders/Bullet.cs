using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Invaders
{
    class Bullet : Sprite
    {
        float speed;

        public Bullet(Texture2D image, Vector2 position, Color color, Rectangle sourceRectangle, float speed)
            : base(image, position, color, sourceRectangle)
        {
            this.speed = speed;
        }

        public void Update(GameTime gameTime)
        {
            position.Y -= speed;
        }
    }
}
