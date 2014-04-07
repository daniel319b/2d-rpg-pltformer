using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RPGPlatformerEngine
{
    public class Map : TileMap
    {
        [ContentSerializerIgnore]
        public Player Player { get; set; }

        public List<Enemy> Enemies
        {
            get;
            private set;
        }

        List<PickableItem> pickeableItems;

        public Map()
        {
            Enemies = new List<Enemy>();
            pickeableItems = new List<PickableItem>();
            
            pickeableItems.Add(new HealthPotion() { Position = new Vector2(250,350) });
            pickeableItems.Add(new HealthPotion() { Position = new Vector2(300, 350) });
            pickeableItems.Add(new HealthPotion() { Position = new Vector2(350, 350) });

            pickeableItems.Add(new PoisonPotion() { Position = new Vector2(280, 580) });
            pickeableItems.Add(new PoisonPotion() { Position = new Vector2(330, 580) });
            pickeableItems.Add(new PoisonPotion() { Position = new Vector2(380, 580) });
        }

        public void Update(GameTime gameTime)
        {
            
            Player.Update(gameTime);

            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Update(gameTime);
                if (Enemies[i].Alive == false)
                {
                    Enemies.RemoveAt(i);
                    i--;
                }
            }
            
                
            
            foreach (PickableItem item in pickeableItems)
            {
                if (item.Alive && Player.BoundBox.Intersects(item.BoundBox))
                    item.Pick();
                item.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            sb.Begin();
            foreach (Enemy e in Enemies)
                e.Draw(sb);
            foreach (PickableItem item in pickeableItems)
                item.Draw(sb);
            
            sb.End();
        }

        public void AddPickable(PickableItem item)
        {
            pickeableItems.Add(item);
        }
    }
}
