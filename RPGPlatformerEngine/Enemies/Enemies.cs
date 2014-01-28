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
            animations["walking"] = new Animation(TextureManager.SetTexture("Monsters/red_snail"), 4, 1, 10);
            //animations["dying"] = new Animation(TextureManager.SetTexture("red_snail_dying"), 3, 1, 10);
            animations["hit"] = new Animation(TextureManager.SetTexture("Monsters/red_snail_hit"), 1, 1, 1);
            CurrentAnimation = animations["walking"];
            Speed = 0.8f;
            Velocity = new Vector2(-Speed, 0);
            Stats.ExpPointsBonus = 5;
            Stats.MaxHealth = Stats.Health = 50;
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
            animations["walking"] = new Animation(TextureManager.SetTexture("Monsters/shroom_walking"), 4, 1, 9);
            animations["dying"] = new Animation(TextureManager.SetTexture("Monsters/shroom_death"), 4, 1, 6);
            animations["standing"] = new Animation(TextureManager.SetTexture("Monsters/shroom_standing"), 3, 1, 9);
            animations["hit"] = new Animation(TextureManager.SetTexture("Monsters/shroom_hit"), 1, 1, 1);

            CurrentAnimation = animations["walking"];
            Speed = 1.5f;
            Velocity = new Vector2(-Speed, 0);
            Stats.ExpPointsBonus = 2;
            Stats.MaxHealth = Stats.Health = 20;
            
        }

        public void DropItem()
        {
            Session.Singleton.CurrentMap.AddPickable(new BronzeCoin() { Position = Position });
        }
    }

    public interface IItemDroppingEnemy
    {
        /// <summary>
        /// This enemy will drop it's item.
        /// </summary>
        void DropItem();
    }
}
