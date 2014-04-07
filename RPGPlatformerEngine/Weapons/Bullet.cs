using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine
{
    public class WeaponProjectile : GameObject
    {
        public int Damage { get; set; }

        public Player Owner { get; set; }

        public float Speed { get; set; }

        public override void Update()
        {
            base.Update();
            
            Owner = Session.Singleton.Player;
            position += velocity;
            foreach (Enemy e in Owner.Map.Enemies)
            {
                if (BoundBox.Intersects(e.BoundBox))
                {
                    Hit(e);
                    Alive = false;
                    return;
                }
            }
        }

        public void Hit(Enemy e)
        {
            // Calculate damage here
            e.Hit(2, Owner);//hit the enemy.
            
        }

        /// <summary>
        /// Fires the bullet from the player.
        /// </summary>
        /// <param name="p"></param>
        public void Fire(Player p)
        {
            //Owner = p;
            p = Session.Singleton.Player;
            Position = new Vector2(p.Position.X, p.Position.Y);//sets the position to the player's position         
            Velocity = new Vector2((float)Math.Cos(p.Rotation), (float)Math.Sin(p.Rotation)) * Speed;//set the velocity according to the rotation. 
        }

        
    }
}
