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

        public Map()
        {
            Enemies = new List<Enemy>();
        }

        public void Update(GameTime gameTime)
        {
            
            Player.Update(gameTime);
            foreach (Enemy e in Enemies)
            {
                e.Update(gameTime,Player);
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            sb.Begin();
            foreach (Enemy e in Enemies)
                e.Draw(sb);
            sb.End();
        }
    }
}
