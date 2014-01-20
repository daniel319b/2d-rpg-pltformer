using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine
{
    /// <summary>
    /// Holds global variables and methods for the current game session.
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

        public GameTime GameTime { get; set; }

        public Session(Map map)
        {
            Singleton = this;
            CurrentMap = map;
            Player = new Player(TextureManager.SetTexture("Player/player"), new Vector2(100), map);
            CurrentMap.Player = Player;
            UI.WindowManager.InitializeWindows();

            CurrentMap.Enemies.Add(new RedSnail() { Position = new Vector2(1062, 595)});
            CurrentMap.Enemies.Add(new Shroom() { Position = new Vector2(362, 595)});
            CurrentMap.Enemies.Add(new Shroom() { Position = new Vector2(862, 595) });
        }

        /// <summary>
        /// Update all the game components
        /// </summary>
        /// <param name="gameTime">A GameTime Object</param>
        public void Update(GameTime gameTime)
        {
            this.GameTime = gameTime;
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
            HUD.Draw(spriteBatch);

        }

        /// <summary>
        /// The things to do when an enemy was killed, in UI side, like display a message.
        /// </summary>
        /// <param name="enemy"></param>
        public void OnEnemyKill(Enemy enemy)
        {
            
        }

        
    }
}
