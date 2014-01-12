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
            Stats.ExpPointsBonus = 5;
            Stats.Health = 50;
        }

        public void DropItem()
        {
            Session.Singleton.CurrentMap.AddPickable(new HealthPotion() { Position = new Vector2 (BoundBox.X, BoundBox.Y)});
        }
    }

    public class Shroom : Enemy, IItemDroppingEnemy
    {
        public Shroom()
        {
            animations["walking"] = new Animation(TextureManager.SetTexture("shroom_animation"), 1, 7, 10);
            CurrentAnimation = animations["walking"];
            Velocity = new Vector2(-1.5f, 0);
            Stats.ExpPointsBonus = 2;
            Stats.Health = 20;
        }

        public void DropItem()
        {
           
        }
    }

    public interface IItemDroppingEnemy
    {
        void DropItem();
    }
}
