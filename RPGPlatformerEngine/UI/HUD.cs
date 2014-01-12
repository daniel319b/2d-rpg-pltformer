using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine
{
    /// <summary>
    /// A class that handles the HUD drawing.
    /// </summary>
    public class HUD
    {
        public static void Draw(SpriteBatch sb)
        {
            sb.Begin();
            DrawHealthBar(sb);
            sb.End();
        }

        private static void DrawHealthBar(SpriteBatch sb)
        {
            Player player = Session.Singleton.Player;
            int healthWidth =  (int)(HealthBarTexture.Width * ((double)player.CurrentStatistics.Health / player.CurrentStatistics.MaxHealth));
            sb.Draw(HealthBarTexture, new Rectangle(20, 10, HealthBarTexture.Width, 44), new Rectangle(0, 0, HealthBarTexture.Width, 44), Color.Gray);
            sb.Draw(HealthBarTexture, new Rectangle(20, 10, healthWidth, 44), new Rectangle(0,0, HealthBarTexture.Width, 44), Color.Red);
        }

        public static Texture2D HealthBarTexture { get { return TextureManager.SetTexture("Player/HealthBar"); } }
    }
}
