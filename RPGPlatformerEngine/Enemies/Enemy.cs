﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGPlatformerEngine
{

    public class Enemy : AnimatedObject
    {
        public EnemyStats Stats { get; protected set; }

        public HorizontalDirection Direction { get; set; }

        float attackTime, attackRate;

        public Enemy()
        {
            Stats = new EnemyStats() { MaxHealth = 100, Health = 100, Level = 1, AttackRate = 1 };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            position += velocity;
            if (velocity.X < 0) Direction = HorizontalDirection.Left;
            else if (velocity.X > 0) Direction = HorizontalDirection.Right;

            UpdateMovement();
            PlayerCollision(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            var effect = Direction == HorizontalDirection.Left ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            
            base.Draw(sb, effect);
            DrawHealthBar(sb);
            //sb.Draw(TextureManager.SetTexture("square_border"), BoundBox, Color.Black);
        }

        private void DrawHealthBar(SpriteBatch sb)
        {
            int width = 80;
            int height = 7;
            Rectangle destRect = new Rectangle(BoundBox.X, BoundBox.Y - 20, (int)(width * ((float)Stats.Health /  Stats.MaxHealth)), height);
            sb.Draw(TextureManager.SetTexture("square"), destRect, Color.Red);
        }
        /// <summary>
        /// Update the movement of the enemy
        /// </summary>
        private void UpdateMovement()
        {
            Map map = Session.Singleton.CurrentMap;
            int leftTile = (int)Math.Floor((float)BoundBox.Left / Tile.Width);
            int rightTile = (int)Math.Ceiling(((float)BoundBox.Right / Tile.Width)) - 1;
            if (map.GetTileCollision(leftTile, (int)Position.Y / Tile.Height) == TileCollision.Impassable ||
                map.GetTileCollision(rightTile+1, (int)Position.Y / Tile.Height) == TileCollision.Impassable)
            {
                velocity *= -1;      
            }
        }

        /// <summary>
        /// Check for player collision with the enemy
        /// </summary>
        /// <param name="gameTime">A GameTime object.</param>
        /// <param name="player">The player of the game.</param>
        private void PlayerCollision(GameTime gameTime)
        {
            if (BoundBox.Intersects(Session.Singleton.Player.BoundBox))
                AttackPlayer(gameTime, Session.Singleton.Player);
        }

        /// <summary>
        /// Attack the player!
        /// </summary>
        /// <param name="gameTime">A GameTime object</param>
        /// <param name="player">The player of the game.</param>
        private void AttackPlayer(GameTime gameTime, Player player)
        {
            attackTime += (float)gameTime.ElapsedGameTime.TotalSeconds;//count the attack interval.
            if(attackTime > attackRate)//if its time to attack, attack!
            {
                player.OnHit(this);//attack the player.
                attackTime = 0;//reset the time.
                attackRate = Stats.AttackRate;
            }
        }

        /// <summary>
        /// Handles what will happen when the enemy gets hit by the player.
        /// </summary>
        /// <param name="Owner">The owner of the bullet, the player.</param>
        public void Hit(int hitAmount, Player player)
        {
            Stats.Health -= hitAmount;
            OnHit();
            if (Stats.Health <= 0)
            {
                Alive = false;
                OnDeath();
                player.OnEnemyKill(this);
                Session.Singleton.OnEnemyKill(this);
            }
        }

        /// <summary>
        /// What will happen when this enemy dies.
        /// </summary>
        protected virtual void OnDeath() 
        {
            var a = this as IItemDroppingEnemy;
            if (a != null) a.DropItem();
        }

        /// <summary>
        /// What happens when this enemy gets hit - override in derived classes.
        /// </summary>
        protected virtual void OnHit() { }
    }
}
