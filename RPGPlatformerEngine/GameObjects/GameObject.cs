﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace RPGPlatformerEngine
{
    /// <summary>
    /// This is the base class for all the objects in the game.
    /// It handles basic graphics and physics things.
    /// </summary>
    public class GameObject
    {
        #region Fields and Properties

        protected Vector2 position;
        protected Vector2 origin, velocity;
        protected Texture2D texture;
        protected Rectangle boundBox;
        protected float rotation = 0.0f, scale = 1.0f, opacity = 1.0f;

        protected bool alive = true;
        protected string text;
        protected Color color = Color.White;
      
        /// <summary>
        /// The position of the object in the world.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Origin property used for the center of the game object
        /// </summary>
        public Vector2 Origin
        {
            get { return origin;}        
        }

        /// <summary>
        /// Object's velocity to update the position.
        /// </summary>
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        /// <summary>
        /// The current texture of the object.
        /// </summary>
        [ContentSerializerIgnore]
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; TextureName = texture.Name; }
        }

        /// <summary>
        /// The assest name of the texture.
        /// </summary>
        public string TextureName
        { get; set; }

        /// <summary>
        /// Used for drawing a text instead of a texture.
        /// </summary>
        [ContentSerializerIgnore]
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        /// <summary>
        /// The color of the object.
        /// it uses the opacity to make the object transparent.
        /// </summary>
        [ContentSerializerIgnore]
        public Color Color
        {
            get
            {
                return color * opacity;
            }
            set { color = value; }
        }

        /// <summary>
        /// The opacity of the texture. Ranging from 0-1 : 1 is fully visible, 0 is fully transparent.
        /// </summary>
        public float Opacity
        {
            get { return opacity; }
            set { opacity = value; }
        }
        /// <summary>
        /// The scale of the GameObject.
        /// Bigger values to make the object bigger.
        /// Smaller values to make the object smaller.
        /// 1 for the default.
        /// </summary>
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>
        /// A rectangle representing the bounds of the object - for collision.
        /// </summary>
        [ContentSerializerIgnore]
        public Rectangle BoundBox
        {
            // get { return boundBox; }
            // protected set { boundBox = value; }
            protected set { boundBox = value; }
            get { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); }
        }

        /// <summary>
        /// The center of the texture of the game object.
        /// </summary>
        [ContentSerializerIgnore]
        public Vector2 Center
        {
            get { return new Vector2(boundBox.Center.X, boundBox.Center.Y); }
        }

        /// <summary>
        /// The rotation of the object - where the object is looking.
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        /// <summary>
        /// Returns if the object is alive or not.
        /// </summary>
        [ContentSerializerIgnore]
        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        /// <summary>
        /// Transform matrix for correct rectangle collision.
        /// </summary>
        [ContentSerializerIgnore] 
        public Matrix Transform
        {
            get;
            private set;
        }
        
        #endregion


        #region Constructors
        /// <summary>
        /// An empty Constructor for a Game Object.
        /// </summary>
        public GameObject() { }
        /// <summary>
        /// Constructs a GameObject with a specific position.
        /// </summary>
        /// <param name="pos">The position of the object.</param>
        public GameObject(Vector2 pos) { Position = pos; }
        /// <summary>
        /// Constructs a GameObject with specific position and texture.
        /// </summary>
        /// <param name="pos">The position of the object.</param>
        /// <param name="text">The texture of the object.</param>
        public GameObject(Vector2 pos, Texture2D text) { Position = pos; Texture = text; }

        #endregion


        #region Update and Draw Methods
        /// <summary>
        /// Updates the object's components and position.
        /// </summary>
        public virtual void Update()
        {
            if(texture != null)
                origin = new Vector2(texture.Width / 2.0f, texture.Height / 2.0f);
            CalculateMatrix();
            CalculateBoundingRectangle();
            
        }

        /// <summary>
        /// Calculates the transform matrix of the object with the origin,
        /// rotation, scale, and position.  This will need to be done every
        /// game loop because chances are the position changed.
        /// </summary>
        private void CalculateMatrix()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Origin, 0)) *
                Matrix.CreateRotationZ(rotation) *
                Matrix.CreateScale(1.0f) *
                Matrix.CreateTranslation(new Vector3(position, 0));
        }

        /// <summary>
        /// Calculates the bounding rectangle of the object using the object's transform
        /// matrix to make a correct rectangle.
        /// </summary>
        private void CalculateBoundingRectangle()
        {
            if (texture != null)
            {
                boundBox = new Rectangle(0, 0, texture.Width, texture.Height);
            }
            else
                boundBox = new Rectangle(0, 0, (int)Font.Regular.MeasureString(text).X, (int)Font.Regular.MeasureString(text).Y);

            Vector2 leftTop = Vector2.Transform(new Vector2(boundBox.Left, boundBox.Top), Transform);
            Vector2 rightTop = Vector2.Transform(new Vector2(boundBox.Right, boundBox.Top), Transform);
            Vector2 leftBottom = Vector2.Transform(new Vector2(boundBox.Left, boundBox.Bottom), Transform);
            Vector2 rightBottom = Vector2.Transform(new Vector2(boundBox.Right, boundBox.Bottom), Transform);

            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop),
                              Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop),
                                      Vector2.Max(leftBottom, rightBottom));

            boundBox = new Rectangle((int)min.X, (int)min.Y,
                (int)(max.X - min.X), (int)(max.Y - min.Y));
            boundBox.X -= (int)origin.X;
            boundBox.Y -= (int)origin.Y;

        }

        /// <summary>
        /// Draws the object only if he's alive.
        /// </summary>
        /// <param name="sb">The spritebatch for drawing.</param>
        public virtual void Draw(SpriteBatch sb)
        {
            if (Alive)
            {               
             //   sb.Begin();
                if (texture != null)
                    sb.Draw(texture, position, null, Color, rotation, origin, scale, SpriteEffects.None, 0.0f);
               // else
               //     sb.DrawString(Font.Regular, text, position, Color);
               //// sb.End();
            }
        }

        /// <summary>
        /// Draws the object only if he's alive.
        /// </summary>
        /// <param name="sb">The spritebatch for drawing.</param>
        /// <param name="transform">The transformation matrix which will apply.</param>
        public virtual void Draw(SpriteBatch sb,Matrix transform)
        {
            if (Alive)
            {
                sb.Begin(SpriteSortMode.Deferred, null, null, null, null, null, transform);
                if (texture != null)
                    sb.Draw(texture, position, null, Color, rotation, origin, scale, SpriteEffects.None, 0.0f);
                else
                    sb.DrawString(Font.Regular, text, position, Color);
                sb.End();
            }
        }
        #endregion

        #region Content Reader

        public class GameObjectReader : ContentTypeReader<GameObject>
        {
            protected override GameObject Read(ContentReader input, GameObject existingInstance)
            {
                GameObject result = existingInstance;
                if (result == null) result = new GameObject();

                result.Position = input.ReadVector2();
                result.Velocity = input.ReadVector2();
                result.TextureName = input.ReadString();
                result.Texture = input.ContentManager.Load<Texture2D>(result.TextureName);
                result.Opacity = input.ReadSingle();
                result.Scale = input.ReadSingle();
                result.Rotation = input.ReadSingle();

                return result;
            }
        }

        #endregion
    }

    #region Content Writer
    [ContentTypeWriter]
    public class GameObjectWriter : ContentTypeWriter<GameObject>
    {
        protected override void Write(ContentWriter output, GameObject value)
        {
            output.Write(value.Position);
            output.Write(value.Velocity);
            output.Write(value.TextureName);
            output.Write(value.Opacity);
            output.Write(value.Scale);
            output.Write(value.Rotation);
        }

        public override string GetRuntimeReader(Microsoft.Xna.Framework.Content.Pipeline.TargetPlatform targetPlatform)
        {
            return typeof(GameObject.GameObjectReader).AssemblyQualifiedName;
        }
    } 
    #endregion
}
