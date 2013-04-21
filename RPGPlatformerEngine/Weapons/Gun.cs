using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGPlatformerEngine.Weapons
{
    public abstract class Gun : Weapon
    {
        /// <summary>
        /// The list of the bullets.
        /// </summary>
        private List<Bullet> Bullets;
        /// <summary>
        /// The fire rate of the weapon(seconds).
        /// </summary>
        protected float fireRate;
        /// <summary>
        /// The number of seconds passed from the last shot.
        /// </summary>
        private float fireTime; 

        /// <summary>
        /// The number of bullets left in the current weapon's magazine.
        /// </summary>
        public int BulletsLeftInMag
        {
            get;
            private set;
        }

        /// <summary>
        /// The current number of magazines in the weapon.
        /// </summary>
        public int Magazines
        {
            get;
            private set;
        }

        /// <summary>
        /// The number of bullets per magazine in this weapon.
        /// </summary>
        public int BulletsPerMag
        {
            get;
            private set;
        }

        protected Gun()
        {

        }
        protected Gun(Player player)
        {
            Player = player;
            Bullets = new List<Bullet>();
        }

        public override void Update(GameTime gameTime)
        {
            //update the bullets.
            UpdateBullets();
        }

        private void UpdateBullets()
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                Bullets[i].Update();
                if (Bullets[i].Alive == false)
                {
                    Bullets.RemoveAt(i);
                    i--;
                }
            }
        }

       
        protected override void HandleInput(GameTime gameTime)
        {
            if (Input.KeyPressed(GameSetting.ShootKey) && BulletsLeftInMag > 0)
                Shoot(gameTime);

        }

        private void Shoot(GameTime gameTime)
        {
            fireTime += (float)gameTime.ElapsedGameTime.TotalSeconds;//count the time.

            if (fireTime >= fireRate)
            {
                CreateBullet();//create a bullet and add it to the world.
                BulletsLeftInMag--;
                fireTime = 0;//reset the timer.
            }
        }

        private void CreateBullet()
        {
            Bullet b = new Bullet();//creates a bullet.  
            b.Speed = 8;
            b.Fire(Player);//Fire the bullet         
            Bullets.Add(b);//add the bullet to the bullet list.
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet b in Bullets)
                b.Draw(spriteBatch);
        }

        
    }
}
