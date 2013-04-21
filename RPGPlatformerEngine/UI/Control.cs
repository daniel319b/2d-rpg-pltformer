using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RPGPlatformerEngine.UI
{
    /// <summary>
    /// A base class for UI controls.
    /// </summary>
    public class Control
    {
        /// <summary>
        /// The position of the control on the screen.
        /// </summary>
        public Vector2 Position { get; set; }
        
        /// <summary>
        /// The color of the control.
        /// </summary>
        public Color Color { get; set; }
       
        /// <summary>
        /// The texture of the control.
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// The Bounding Rectagle of the control.
        /// </summary>
        public virtual Rectangle BoundBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }

        /// <summary>
        /// Creates a new Control.
        /// </summary>
        public Control()
        {
            Color = Color.White;
        }

        /// <summary>
        /// Creates a new Control with a given position.
        /// </summary>
        /// <param name="position">The position of the control.</param>
        public Control(Vector2 position)
        {
            Position = position;
            Color = Color.White;
        }

        /// <summary>
        /// Creates a new control with a given position and texture.
        /// </summary>
        /// <param name="position">The position of the control.</param>
        /// <param name="texture">The texture of the control.</param>
        public Control(Vector2 position, Texture2D texture)
        {
            Position = position;
            Texture = texture;
            Color = Color.White;
        }
       
        public virtual void Update() { }

        public virtual void Draw(SpriteBatch sb)
        {
            if (Texture != null)
                sb.Draw(Texture, Position, Color); 
        }

        
    }
}
