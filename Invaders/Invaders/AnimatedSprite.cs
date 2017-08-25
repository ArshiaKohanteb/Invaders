using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Invaders
{
    class AnimatedSprite : Sprite
    {

        protected List<Frame> frames;
        int currentFrame;
        TimeSpan frameRate;
        TimeSpan frameTimer;

        public AnimatedSprite(Texture2D Image, Vector2 Position, Color Color)
            : base(Image, Position, Color)
        {
            frames = new List<Frame>();
            currentFrame = 0;
            frameRate = TimeSpan.Zero;
            frameTimer = TimeSpan.Zero;

        }

        public AnimatedSprite(Texture2D Image, Vector2 Position, Color Color, List<Frame> frames, TimeSpan frameRate)
            : base(Image, Position, Color)
        {
            this.frames = frames;
            currentFrame = 0;

            this.frameRate = frameRate;
            frameTimer = TimeSpan.Zero;
        }


        //update (gametime): loop through the animation infinetly (currentFrame++ but if currentFrame > # of frames, set currentFrame = 0, and use a timespan to control the frameRate)
        public virtual void Update(GameTime gameTime)
        {

            frameTimer += gameTime.ElapsedGameTime;
            if (frameTimer >= frameRate)
            {
                frameTimer = TimeSpan.Zero;
                currentFrame++;
                if (currentFrame >= frames.Count)
                {
                    currentFrame = 0;
                }

                sourceRectangle = frames[currentFrame].Bounds;
            }



        }

    }
}
