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
    class Sprite
    {
        protected Texture2D image;
        protected Vector2 position;
        protected Rectangle sourceRectangle;
        protected Color color;
        protected Vector2 origin;
        protected float scale;
        protected SpriteEffects effect;

        public Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);
            }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float X
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public float Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }
        public Sprite(Texture2D image, Vector2 position, Color color)
        {
            this.image = image;
            this.position = position;
            this.color = color;
            sourceRectangle = new Rectangle(0, 0, image.Width, image.Height);
            origin = Vector2.Zero;
            scale = 1f;
            effect = SpriteEffects.None;
        }

        public Sprite(Texture2D image, Vector2 position, Color color, Rectangle sourceRectangle)
        {
            this.image = image;
            this.position = position;
            this.color = color;
            this.sourceRectangle = sourceRectangle;
            origin = Vector2.Zero;
            scale = 1f;
            effect = SpriteEffects.None;
        }


        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(image, position, sourceRectangle, color, 0, origin, scale, effect, 0f);
        }
    }
}
