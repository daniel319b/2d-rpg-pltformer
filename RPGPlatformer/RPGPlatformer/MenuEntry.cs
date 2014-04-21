using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGPlatformer
{
    class MenuEntry
    {
        public string Text { get; set; }

        public Vector2 Position { get; set; }

        public Color Color { get; set; }

        public float Opacity { get; set; }

        public Rectangle BoundBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y,
                    (int)RPGPlatformerEngine.Font.MenuEntry.MeasureString(Text).X,
                    (int)RPGPlatformerEngine.Font.MenuEntry.MeasureString(Text).Y);
            }
        }

        public bool IsSelected { get; set; }

        /// <summary>
        /// Draws the text on the screen, must call begin/end  before.
        /// </summary>
        /// <param name="sb"></param>
        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(RPGPlatformerEngine.Font.MenuEntry, Text, Position, Color * Opacity);
        }


        public static Color SelectedColor { get; set; }
        
    }
}
