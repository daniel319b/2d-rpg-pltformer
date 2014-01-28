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
        static FilledBar healthBar = new FilledBar(new Vector2(20, 10), Color.Gray, Color.Red) { Texture = TextureManager.SetTexture("Player/HealthBar") };
        static FilledBar expBar = new FilledBar(new Vector2(20, 80), Color.Gray, Color.Gold) { Texture = TextureManager.SetTexture("Player/HealthBar") };

        static Vector2 BottomPanelPosition = new Vector2(0, 600);

        public static void Draw(SpriteBatch sb)
        {
            sb.Begin();
            DrawBars(sb);
            sb.End();
        }

        private static void DrawBars(SpriteBatch sb)
        {
            var background = TextureManager.SetTexture("UI/form");
           // sb.Draw(background, new Rectangle((int)BottomPanelPosition.X,(int)BottomPanelPosition.Y, 500, 100), Color.White);
            var stats = Session.Singleton.Player.CurrentStatistics;
            healthBar.Draw(sb, stats.Health, stats.MaxHealth);
            expBar.Draw(sb, stats.ExperiencePoints, stats.ExpPointsToNextLevel);
        }

    }

    /// <summary>
    /// A class that represents a 'Bar' UI object like Health/Exp/Armor bar. 
    /// </summary>
    public class FilledBar
    {
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }

        public Color BackColor { get; set; }
        public Color BarColor { get; set; }

        public Vector2 Size { get; set; }

        public FilledBar(Vector2 position, Color backColor, Color barColor)
        {
            Position = position;
            BackColor = backColor;
            BarColor = barColor;
            Size = Vector2.Zero;
        }

        public void Draw(SpriteBatch sb, int value, int maxValue)
        {
            if(Texture != null)
            {
                Size = Size == Vector2.Zero ? new Vector2(Texture.Width, Texture.Height) : Size;
                int healthWidth = (int)(Size.X * ((double)value / maxValue));

                sb.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), BackColor);
                sb.Draw(Texture, new Rectangle((int)Position.X,(int)Position.Y, healthWidth, (int)Size.Y), BarColor);
            }
        }
    }
}
