using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine
{
    public class RedSnail : Enemy, IItemDroppingEnemy
    {
        public RedSnail()
        {
            animations["walking"] = new Animation(TextureManager.SetTexture("red_snail"), 4, 1, 10);
            //animations["dying"] = new Animation(TextureManager.SetTexture("red_snail_dying"), 3, 1, 10);
            CurrentAnimation = animations["walking"];
            Velocity = new Vector2(-0.8f, 0);
        }

        public IPickable DropItem()
        {
            return new HealthPotion();
        }
    }

    public class Shroom : Enemy, IItemDroppingEnemy
    {
        public Shroom()
        {
            animations["walking"] = new Animation(TextureManager.SetTexture("shroom_animation"), 1, 7, 10);
            CurrentAnimation = animations["walking"];
            Velocity = new Vector2(-1.5f, 0);
        }

        public IPickable DropItem()
        {
            return new PoisonPotion();
        }
    }

    public interface IItemDroppingEnemy
    {
        IPickable DropItem();
    }
}
