using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine.UI
{
    /// <summary>
    /// A label control, displays a text on a window. 
    /// </summary>
    public class Label : Control
    {
        public string Text { get; set; }

        public override Rectangle BoundBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Font.Regular.MeasureString(Text).X, (int)Font.Regular.MeasureString(Text).Y);
            }
        }

        public Label()
        {
            Color = Color.Black;
            Text = "";
        }
        public Label(Vector2 position, string text)
            : base(position)
        {
            Text = text;
            Color = Color.Black;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            sb.DrawString(Font.Regular, Text, Position, Color);
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
