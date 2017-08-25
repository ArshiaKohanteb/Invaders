using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Invaders
{
    class Base : AnimatedSprite
    {
        //health or damage
        //public bool DrawTrue = true;
        public int damage = 0;
        public Base(Texture2D image, Vector2 position, List<Frame> frames)
            : base(image, position, Color.White)
        {
            this.image = image;
            this.position = position;
            this.frames = frames;

            sourceRectangle = frames[0].Bounds;
        }

        public override void Update(GameTime gameTime)
        {
            //change the image depending on health
            //SourceRectangle = frames[damage]; // < check this doesn't go out of bounds

            //take damage elsewhere

            sourceRectangle = frames[damage / 2].Bounds;
        }

        public bool BaseHit(Rectangle enemyBulletHitbox)
        {
            return Hitbox.Intersects(enemyBulletHitbox);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, sourceRectangle, Color.White, 0f, frames[damage / 2].Origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
