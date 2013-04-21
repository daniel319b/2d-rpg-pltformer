using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine
{
    public class Enemy : GameObject
    {
        /// <summary>
        /// The health of the enemy.
        /// </summary>
        public int Health { get; private set; }

        /// <summary>
        /// The max health that the enemy can have.
        /// </summary>
        public int MaxHealth { get; private set; }

        /// <summary>
        /// The attack rate of the enemy in seconds.
        /// </summary>
        public float AttackRate { get; private set; }

        
        float attackTime;
        private bool changedDirection;

        public Enemy()
        {
            velocity = new Vector2(-3, 0);
        }

        public void Update(GameTime gameTime,Player player)
        {
            base.Update();
            position += velocity;
            UpdateMovement();
            PlayerCollision(gameTime,player);
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
              //  if (changedDirection == true) return;
                velocity *= -1;// Vector2.Zero;
                //changedDirection = true;
            }
        }

        /// <summary>
        /// Check for player collision with the enemy
        /// </summary>
        /// <param name="gameTime">A GameTime object.</param>
        /// <param name="player">The player of the game.</param>
        private void PlayerCollision(GameTime gameTime, Player player)
        {
            if (BoundBox.Intersects(player.BoundBox))
                AttackPlayer(gameTime, player);
        }

        /// <summary>
        /// Attack the player!
        /// </summary>
        /// <param name="gameTime">A GameTime object</param>
        /// <param name="player">The player of the game.</param>
        private void AttackPlayer(GameTime gameTime, Player player)
        {
            attackTime += (float)gameTime.ElapsedGameTime.TotalSeconds;//count the attack interval.
            if(attackTime > AttackRate)//if its time to attack, attack!
            {
                player.Hit(this);//attack the player.
                attackTime = 0;//reset the time.
            }
        }

        /// <summary>
        /// Handles what will happen when the enemy gets hit by the player.
        /// </summary>
        /// <param name="Owner">The owner of the bullet, the player.</param>
        public void Hit(int hitAmount, Player player)
        {
            Health -= hitAmount;
            if (Health <= 0)
            {
                alive = false;

            }
        }
    }
}
