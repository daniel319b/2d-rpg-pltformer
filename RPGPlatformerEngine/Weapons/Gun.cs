using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGPlatformerEngine.Weapons
{
    public abstract class RangedWeapon : Weapon
    {
        /// <summary>
        /// The list of the bullets.
        /// </summary>
        private List<WeaponProjectile> Bullets;
        /// <summary>
        /// The fire rate of the weapon(seconds).
        /// </summary>
        protected float fireRate;
        /// <summary>
        /// The number of seconds passed from the last shot.
        /// </summary>
        private float fireTime;

        Timer timer;

        protected abstract Texture2D BulletTexture { get; }

        protected RangedWeapon()
        {
            Player = Session.Singleton.Player;
            Bullets = new List<WeaponProjectile>();
            //fireRate = 0.1f;
            timer = new Timer(fireRate, CreateBullet);
        }

        public override void Update(GameTime gameTime)
        {
            //update the bullets.
            UpdateBullets();
            HandleInput(gameTime);
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
            if (Input.KeyPressed(GameSetting.ShootKey) && true)
                Shoot(gameTime);
        }

        protected abstract bool CanFire();
        
        private void Shoot(GameTime gameTime)
        {
            timer.Update(gameTime);
            //fireTime += (float)gameTime.ElapsedGameTime.TotalSeconds;//count the time.

            //if (fireTime >= fireRate)
            //{
            //    CreateBullet();//create a bullet and add it to the world.

            //    fireTime = 0;//reset the timer.
            //}
            
        }

        protected virtual void CreateBullet()
        {
            WeaponProjectile b = new WeaponProjectile();//creates a bullet.  
            b.Speed = Session.Singleton.Player.HorizontalDirection == HorizontalDirection.Right ? 9 : -9;
            b.Texture = BulletTexture;
            b.Scale = 0.3f;
            b.Fire(Player);//Fire the bullet         
            Bullets.Add(b);//add the bullet to the bullet list.
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (WeaponProjectile b in Bullets)
                b.Draw(spriteBatch);
        }

        
    }

    public class Gun : RangedWeapon
    {
        /// <summary>
        /// The number of bullets left in the current weapon's magazine.
        /// </summary>
        public int BulletsLeftInMag { get; private set; }
        /// <summary>
        /// The current number of magazines in the weapon.
        /// </summary>
        public int Magazines { get; private set; }

        /// <summary>
        /// The number of bullets per magazine in this weapon.
        /// </summary>
        public int BulletsPerMag {get;private set;}

        protected override Texture2D BulletTexture
        {
            get { return TextureManager.SetTexture("square"); }
        }

        protected override void CreateBullet()
        {
            base.CreateBullet();
            BulletsLeftInMag--;
        }

        protected override bool CanFire()
        {
            return BulletsLeftInMag > 0;
        }

        
    }
}
