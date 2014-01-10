using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine
{
    public class Animation
    {
        Rectangle currentFrameRect;
        /// <summary>
        /// A rectangle which holds the current frame.
        /// </summary>
        public Rectangle CurrentFrameRect
        {
            get { return currentFrameRect; }
            set { currentFrameRect = value; }
        }

        /// <summary>
        /// The index of the current frame.
        /// </summary>
        public int currentFrame;

        /// <summary>
        /// A timer which indicates when to change frame.
        /// </summary>
        float timer;

        /// <summary>
        /// Total number of frames in the texture.
        /// </summary>
        public int numOfFrames;

        /// <summary>
        /// The time which each frame will be displayed.
        /// </summary>
        float frameTime;

        /// <summary>
        /// The size of the frame.
        /// </summary>
        Vector2 frameSize = Vector2.Zero;

        /// <summary>
        /// The number of columns in the texture.
        /// </summary>
        int columns;

        Texture2D texture;

        /// <summary>
        /// The texture that holds the animation.
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        private bool repeating;

        /// <summary>
        /// Determines if the animation is repeating or not.
        /// </summary>
        public bool Repeating
        {
            get { return repeating; }
            set { repeating = value; }
        }

        private Vector2 position;

        /// <summary>
        /// The position of the animation in the world.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        /// <summary>
        /// Constructs a new Animation.
        /// </summary>
        /// <param name="texture">The texture that holds the animation. </param>
        /// <param name="totalNumOfFrames">the total number of frames in the animation texture. </param>
        /// <param name="columns"> The number of columns in the animation texture.</param>
        /// <param name="rows">The number of rows in the animation texture. </param>
        /// <param name="framesPerSecond">The speed of the animation. </param>
        public Animation(Texture2D texture, int columns, int rows, float framesPerSecond)
        {
            this.texture = texture;
            //calculate the frame size.
            frameSize.X = texture.Width / columns;
            frameSize.Y = texture.Height / rows;
            this.columns = columns;// for later calculations.
            numOfFrames = rows * columns;
            //calculate the frame time.
            frameTime = 1.0f / framesPerSecond;
            Reset();//reset all the update components.
            repeating = true;
        }

        /// <summary>
        /// Resets the animation.
        /// </summary>
        public void Reset()
        {
            //zero everything..... 
            currentFrame = 0;
            timer = 0;
            currentFrameRect = Rectangle.Empty;
        }

        /// <summary>
        /// Updates the animation
        /// </summary>
        /// <param name="gameTime">GameTime Object</param>
        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer += elapsed; //add the total seconds passed from the last update to the timer.

            if (timer >= frameTime)// if it is time to change frame...
            {
                timer -= frameTime;//reset the timer.
                if (currentFrame != (numOfFrames - 1))
                    currentFrame++;//advance by 1 frame.
                else if (repeating)
                    currentFrame = 0;
            }

            int x = currentFrame % columns; //the frame number in the row.
            int y = currentFrame / columns; //the  frame number in the column.       
            //we know that the frameSize.X is the width and the Y is the height.
            //To get the position(in pixels) of the rectangle:
            //we have the frame number in the column and the row, so if we multiply it by the width or height of  frame - we get the the position.           
            //so, build the frame drawing rectangle from this information.
            currentFrameRect = new Rectangle((int)(x * frameSize.X), (int)(y * frameSize.Y), (int)frameSize.X, (int)frameSize.Y);
        }

        /// <summary>
        /// Draws the animation.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch for drawing.</param>
        /// <param name="position">The position for the drawing.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin();
            spriteBatch.Draw(texture, position, currentFrameRect, Color.White , 0, new Vector2(frameSize.X  / 2, frameSize.Y / 2), 1,SpriteEffects.None, 0);
           // spriteBatch.End();

        }

        public void Draw(SpriteBatch spriteBatch, SpriteEffects effects)
        {
            spriteBatch.Draw(texture, position, currentFrameRect, Color.White, 0, new Vector2(frameSize.X / 2, frameSize.Y / 2), 1, effects, 0);
        }
        /// <summary>
        /// Draws the animation.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch for drawing.</param>
        /// <param name="position">The position for the drawing.</param>
        /// <param name="rotation">The rotation value of the animation.</param>
        /// <param name="scale">A Scale Factor.</param>
        public void Draw(SpriteBatch spriteBatch, float rotation, float scale, Vector2 origin)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, currentFrameRect, Color.White, rotation, origin, scale, SpriteEffects.FlipHorizontally, 0.0f);
            spriteBatch.End();
        }

        public void Draw(SpriteBatch spriteBatch, float rotation, float scale, Vector2 origin, Matrix transformation)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, transformation);
            spriteBatch.Draw(texture, position, currentFrameRect, Color.White, rotation, origin, scale, SpriteEffects.FlipHorizontally, 0.0f);
            spriteBatch.End();
        }
    }
}
