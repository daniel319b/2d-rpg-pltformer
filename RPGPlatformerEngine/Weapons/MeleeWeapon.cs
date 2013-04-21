using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine.Weapons
{
    public class MeleeWeapon : Weapon
    {
        protected float hitRate;
        private float hitTime;

        protected float hitRange = 20;

        public MeleeWeapon(Player player)
        {
            Player = player;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        protected override void HandleInput(GameTime gameTime)
        {
            if (Input.KeyDown(GameSetting.ShootKey))
                Attack(gameTime);
        }

        private void Attack(GameTime gameTime)
        {
            hitTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (hitTime >= hitRate)
            {
                hitTime = 0;
                foreach (Enemy e in Player.Map.Enemies)
                    if(Vector2.Distance(e.Position,Player.Position) <= hitRange)
                    {
                        int hitAmount = 0;//Do Calculations here.
                        e.Hit(hitAmount, Player);
                    }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
