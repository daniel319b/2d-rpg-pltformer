using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine
{
    public class Session
    {
        public Map CurrentMap { get; set; }

        public Player Player { get; set; }

        public static Session Singleton { get; private set; }

        public Session(Map map)
        {
            CurrentMap = map;
            Player = new Player(TextureManager.SetTexture("player"), new Vector2(100), map);
            CurrentMap.Player = Player;
            Singleton = this;
            CurrentMap.Enemies.Add(new Enemy() { Texture = TextureManager.SetTexture("square"), Position = new Vector2(862, 595) });
        }

        public void Update(GameTime gameTime)
        {
            CurrentMap.Update(gameTime);
            Input.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentMap.Draw(spriteBatch);
            Player.Draw(spriteBatch);
        }
    }
}
