using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGPlatformerEngine
{
    public class AnimatedObject : GameObject
    {
        protected Dictionary<string, Animation> animations;

        public Animation CurrentAnimation { get; protected set; }

        new public Rectangle BoundBox
        {
            get
            {
                return new Rectangle((int)Position.X - (int)CurrentAnimation.FrameSize.X / 2, (int)Position.Y - (int)CurrentAnimation.FrameSize.Y / 2, (int)CurrentAnimation.FrameSize.X, (int)CurrentAnimation.FrameSize.Y);
            }
        }

        public AnimatedObject()
        {
            animations = new Dictionary<string, Animation>();   
        }

        public virtual void Update(GameTime gameTime)
        {
            base.Update();
            CurrentAnimation.Position = Position;
            CurrentAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            CurrentAnimation.Draw(sb);
        }

        public virtual void Draw(SpriteBatch sb, SpriteEffects effect)
        {
            CurrentAnimation.Draw(sb, effect);
        }

        protected void StopAndResetAnimation()
        {
            CurrentAnimation.Reset();
            CurrentAnimation.Active = false;
        }
    }
}

