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
        protected float hitRange = 60;

        Rectangle rangeRectangle;
        Timer timer;
        public MeleeWeapon(Player player)
        {
            Player = player;
            hitRate = 0.7f;
            timer = new Timer(hitRate, CheckHit);
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
        }

        
        protected override void HandleInput(GameTime gameTime)
        {
            int pos = Player.HorizontalDirection == HorizontalDirection.Left ? -2 * Player.Texture.Width : Player.Texture.Width; 
            rangeRectangle = new Rectangle((int)Player.Position.X + pos,(int)Player.Position.Y, (int)hitRange, Player.Texture.Height);
            if (Input.KeyDown(GameSetting.ShootKey))
                Attack(gameTime);
        }

        private void CheckHit()
        {
            foreach (Enemy e in Player.Map.Enemies)
                if (rangeRectangle.Intersects(e.BoundBox))
                {
                    int hitAmount = Player.CurrentStatistics.BaseDamage + this.Damage;//Do Calculations here.
                    e.Hit(hitAmount, Player);
                    return;
                }
        }

        private void Attack(GameTime gameTime)
        {
            timer.Update(gameTime);
            //hitTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //if (hitTime >= hitRate)
            //{
            //    hitTime = 0;
            //    foreach (Enemy e in Player.Map.Enemies)
            //        if (rangeRectangle.Intersects(e.BoundBox))
            //        {
            //            int hitAmount = Player.CurrentStatistics.BaseDamage + this.Damage;//Do Calculations here.
            //            e.Hit(hitAmount, Player);
            //            return;
            //        }
            //}
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(TextureManager.SetTexture("square"), new Rectangle(rangeRectangle.X, rangeRectangle.Y, 10, 10), Color.Black);
            spriteBatch.End();
        }
    }
}
