using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Microsoft.Xna.Framework.Content;

namespace RPGPlatformerEngine.UI
{
    public class Button : Control
    {
        /// <summary>
        /// This event fires when the user has clicked the button. 
        /// </summary>
        public event EventHandler Clicked;

        Color textColor = Color.Black;

        public Color TextColor
        {
            get { return textColor; }
            set { textColor = value; }
        }

        public string Text { get; set; }

        public override Rectangle BoundBox
        {
            get
            {
                if (Text != null && !Text.Equals(""))
                    return new Rectangle((int)Position.X, (int)Position.Y, (int)Font.Regular.MeasureString(Text).X + 20, (int)Font.Regular.MeasureString(Text).Y + 10);
                else
                    return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }

        EventArgs eventArgs = EventArgs.Empty;

        public EventArgs EventArgs
        {
            get { return eventArgs; }
            set { eventArgs = value; }
        }

        public Button()
        { }

        public Button(Vector2 position, Texture2D texture) : base(position, texture)
        {
            Text = "";
        }
           
        public Button(Vector2 position, string text): base(position)
        {
            this.Text = text;
        }

        public Button(Vector2 position, Texture2D texture, string text): base(position, texture)
        {
            this.Text = text;
        }


        public override void Update()
        {
            if (Input.MouseRectangle.Intersects(BoundBox) && Input.LeftButtonPressed())
                onLeftClick();
        }

        public void onLeftClick()
        {
            if (Clicked != null)
                Clicked(this, eventArgs);
        }

        public override void Draw(SpriteBatch sb)
        {
            if (Text != null)
            {
                if (Texture != null)
                {
                    sb.Draw(Texture, BoundBox, Color.White);
                    sb.DrawString(Font.Regular, Text, Position + new Vector2(10, 5), TextColor);
                }
                else//if only text.
                    sb.DrawString(Font.Regular, Text, Position, TextColor);
            }
            else//if there is no text draw the regular texture.
                base.Draw(sb);
        }   
    }
}
