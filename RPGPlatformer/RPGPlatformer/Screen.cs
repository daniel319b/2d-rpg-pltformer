using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGPlatformer
{
    public abstract class Screen
    {
        public abstract void Update(GameTime gameTime);

        public abstract void LoadContent();

        public abstract void Draw(SpriteBatch spriteBatch);

        
    }
}
