using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class Session
    {
        /// <summary>
        /// The current map
        /// </summary>
        public Map CurrentMap { get; set; }

        /// <summary>
        /// The current game player.
        /// </summary>
        public Player Player { get; set; }

        public static Session Singleton { get; private set; }

        public Session(Map map)
        {
            CurrentMap = map;
            Player = new Player(TextureManager.SetTexture("player"), new Vector2(100), map);
            CurrentMap.Player = Player;
            Singleton = this;
            CurrentMap.Enemies.Add(new Enemy() { Texture = TextureManager.SetTexture("square"), Position = new Vector2(862, 595) });
            UI.WindowManager.InitializeWindows();
        }

        /// <summary>
        /// Update all the game components
        /// </summary>
        /// <param name="gameTime">A GameTime Object</param>
        public void Update(GameTime gameTime)
        {
            Input.Update();
            CurrentMap.Update(gameTime);
            UI.WindowManager.Update();
        }

        /// <summary>
        /// Draw all the game components
        /// </summary>
        /// <param name="spriteBatch">A SpriteBatch object</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentMap.Draw(spriteBatch);
            Player.Draw(spriteBatch);
            UI.WindowManager.Draw(spriteBatch);
        }
    }
}
