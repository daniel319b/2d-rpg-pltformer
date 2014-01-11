using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPlatformerEngine
{
    public class RedSnail : Enemy
    {
        public RedSnail()
        {
            //  animations["walking"] = new Animation(TextureManager.SetTexture(""), 1, 7, 10);
            //animations["dying"] = new Animation();

        }
    }


    public class AnimatedObject : GameObject
    {
        Dictionary<string, Animation> animations;
        Animation CurrentAnimation;

        new public Rectangle BoundBox { get; private set; }

        public AnimatedObject()
        {
            animations = new Dictionary<string, Animation>();
            animations["walking"] = new Animation(TextureManager.SetTexture(""), 1, 7, 10);

        }

        public void Update(GameTime gameTime)
        {
            base.Update();
            CurrentAnimation.Update(gameTime);

        }
    }
}
