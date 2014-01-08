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
        /// <summary>
        /// The text of the label
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The Bounding Rectangle of the label.
        /// </summary>
        public override Rectangle BoundBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Font.Regular.MeasureString(Text).X, (int)Font.Regular.MeasureString(Text).Y);
            }
        }


        /// <summary>
        /// Creates a new Label object
        /// </summary>
        public Label() : this(Vector2.Zero, "") { }

        /// <summary>
        /// Creates a new Label object with a given position and a name.
        /// </summary>
        /// <param name="position">The position in the screen of the label.</param>
        /// <param name="text">The text of the label.</param>
        public Label(Vector2 position, string text)
            : base(position)
        {
            Text = text;
            Color = DefautLabelColor;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            sb.DrawString(Font.Regular, Text, Position, Color);
        }

        public override string ToString()
        {
            return Text;
        }


        public static Color DefautLabelColor { get { return Color.Black; } }
    }
}
