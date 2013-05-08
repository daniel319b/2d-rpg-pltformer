using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGPlatformerEngine
{
    public class InfoBox 
    {
        /// <summary>
        /// The position of the box.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The bounding rectangle of the info box.
        /// </summary>
        public Rectangle Bounds { get; set; }

        /// <summary>
        /// Returns the text of the info box.
        /// </summary>
        public string Text
        {
            get { return textObject.Text; }
        }
         GameObject textObject;
     
        /// <summary>
        /// Creates an new info box in a specific position and with a specific text in it.
        /// </summary>
        /// <param name="pos">The position of the info box on the screen.</param>
        /// <param name="text">The text that will be in the info box.</param>
         public InfoBox(Vector2 pos, string text)
         {
             Position = pos;
             textObject = new GameObject();
             textObject.Text = text;
             Bounds = new Rectangle((int)Position.X, (int)Position.Y, (int)Font.Regular.MeasureString(text).X+10, (int)Font.Regular.MeasureString(text).Y+5);
         }

         public void Update()
         {      
             textObject.Position = new Vector2(Bounds.X+5, Bounds.Y) ;
             textObject.Update();
         }


         public void Draw(SpriteBatch sb)
         {
             sb.Begin();
             sb.Draw(Texture, Bounds,Color.White);
             sb.End();
             textObject.Draw(sb);
         }

        /// <summary>
        /// The background texture for the info box.
        /// </summary>
        public static Texture2D Texture { get; set; }
    }
}
