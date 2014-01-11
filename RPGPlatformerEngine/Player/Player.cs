using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGPlatformerEngine
{
    public enum HorizontalDirection
    {
        Left,
        Right,
    }

    public class Player : GameObject
    {
        public Vector2 Acceleration { get; private set; }
      
        public Map Map { get; set; }

        public Weapon CurrentWeapon { get; private set; }

        public PlayerStatistics CurrentStatistics { get; private set; }

        public Inventory Inventory { get; private set; }

        public HorizontalDirection HorizontalDirection { get; set; }

        bool isOnGround;

        RPGPlatformerEngine.Weapons.MeleeWeapon knife;
        public Player(Texture2D tex, Vector2 pos,Map map):base(pos,tex)
        {
            Map = map;
            CurrentStatistics = new PlayerStatistics();
            Inventory = new Inventory(new Vector2(10), 7, 7);
            Inventory.AddItem(new InventoryItem("Sword"));
            Inventory.AddItem(new InventoryItem("Sword"));
            Inventory.AddItem(new InventoryItem("Sword"));
            knife = new RPGPlatformerEngine.Weapons.MeleeWeapon(this);
        }

        public void Update(GameTime gameTime)
        {
            base.Update();
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (!isOnGround)
                Acceleration = new Vector2(0, 1400);
           
            HandleInput();
            ApplyPhysics(time);
            HandleCollisions();
            knife.Update(gameTime);
        }

        private void HandleInput()
        {
            if (Input.KeyDown(Keys.D))
            {
                position.X += 2;
                HorizontalDirection = RPGPlatformerEngine.HorizontalDirection.Right;
            }
            if (Input.KeyDown(Keys.A))
            {
                position.X -= 2;
                HorizontalDirection = RPGPlatformerEngine.HorizontalDirection.Left;
            }
            if (Input.KeyPressed(Keys.W) && isOnGround)
            {
                isOnGround = false;
                velocity.Y = -455f;
            }

        }

        private void HandleCollisions()
        {
            int leftTile = (int)Math.Floor((float)BoundBox.Left / Tile.Width);
            int rightTile = (int)Math.Ceiling(((float)BoundBox.Right / Tile.Width)) - 1;
            int topTile = (int)Math.Floor((float)BoundBox.Top / Tile.Height);
            int bottomTile = (int)Math.Ceiling(((float)BoundBox.Bottom / Tile.Height)) - 1;

            isOnGround = false;

            for (int y = topTile; y <= bottomTile; y++)
            {
                for (int x = leftTile; x <= rightTile; x++)
                {
                    TileCollision collisionType = Map.GetTileCollision(x, y);//get the collision type of the current tile.
                    Rectangle tileBounds = Map.GetTileBounds(x, y);//get the tile bounding rectangle.
                    Rectangle intersection = Rectangle.Intersect(tileBounds, BoundBox);//get the intersection rectangle of this tile and the player.

                    if (collisionType != TileCollision.Passable && intersection != Rectangle.Empty)
                    {
                        
                        int YDepth = intersection.Height;
                        int XDepth = intersection.Width;

                        if (collisionType == TileCollision.Impassable && YDepth > XDepth)//X Axis (-Dont check for platforms).
                        {
                            //right direction
                            if (position.X <= tileBounds.X)
                                position.X -= XDepth;
                            //left direction
                            else
                                position.X += XDepth;
                          
                        }
                        else if (YDepth < XDepth || collisionType == TileCollision.Impassable)//Y Axis
                        {
                            //we are standing on the tile.(above the tile)
                            if (BoundBox.Bottom +20 < tileBounds.Bottom && velocity.Y >= 0)
                            {
                                position.Y -= YDepth -1f;
                                isOnGround = true;
                                Acceleration = Vector2.Zero;
                                velocity.Y = 0;
                            }
                            //our head touches the bottom of the tile.(below the tile)
                            else if (position.Y > tileBounds.Y && collisionType == TileCollision.Impassable && velocity.Y < 0)
                            {
                                position.Y += YDepth;
                                velocity.Y = 0;
                            }
                        }                       
                    }
                }//for x
            }//for y
        }//HandleCollisions

        
        private void ApplyPhysics(float time)
        {
            velocity += Acceleration * time;
            position += velocity * time;
        }

        
        public override void Draw(SpriteBatch sb)
        {
          //  sb.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Camera.Transform);
            sb.Begin();
            sb.Draw(texture, position, Color.White);
            sb.End();
            knife.Draw(sb);
        }

        /// <summary>
        /// An enemy has hit the player!
        /// </summary>
        /// <param name="enemy">The enemy that hit the player.</param>
        public void OnHit(Enemy enemy)
        {
            //CurrentStatistics.Health -= 5;
            //velocity = new Vector2(50 * enemy.Velocity.X, 300);
        }

        /// <summary>
        /// The player has killed an enemy!
        /// </summary>
        /// <param name="enemy">The enemy that was slain</param>
        public void OnEnemyKill(Enemy enemy)
        {
            // updates stats, display a message..
        }
    }
}
