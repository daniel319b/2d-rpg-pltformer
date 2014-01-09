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
            var potion = new HealthPotion();
            potion.Position = new Vector2(300);
            pickeableItems.Add(potion);
        }

        public void Update(GameTime gameTime)
        {
            
            Player.Update(gameTime);
            foreach (Enemy e in Enemies)
            {
                e.Update(gameTime,Player);
            }

            foreach (PickableItem item in pickeableItems)
            {
                if (item.Alive && Player.BoundBox.Intersects(item.BoundBox))
                    item.Pick();
                    
            }

            if (Input.LeftButtonPressed())
            {
                pickeableItems.Add(new PoisonPotion() { Position = Input.MousePosition});
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            sb.Begin();
            foreach (Enemy e in Enemies)
                e.Draw(sb);
            foreach (PickableItem item in pickeableItems)
            {
                item.Draw(sb);
            }
            sb.End();
        }
    }
}
