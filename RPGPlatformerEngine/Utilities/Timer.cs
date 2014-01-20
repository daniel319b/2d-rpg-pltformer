using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine
{
    /// <summary>
    /// A class that gives the ability to execute a method after a period of time.
    /// </summary>
    public class Timer
    {
        float interval, elapsed;
        Action action;

        /// <summary>
        /// Makes a new Timer object.
        /// </summary>
        /// <param name="interval">The time to pass to execute the method</param>
        /// <param name="a">The method to execute</param>
        public Timer(float interval, Action a)
        {
            this.interval = interval;
            action = a;
        }

        /// <summary>
        /// Updates the Timer
        /// </summary>
        /// <param name="gameTime">A GameTime object</param>
        public void Update(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsed >= interval)
            {
                action();
                elapsed = 0;
            }
        }
    }
}
