using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGPlatformerEngine
{
    public abstract class Weapon
    {
        public Player Player { get; set; }

        public int Damage { get; set; }

        public abstract void Update(GameTime gameTime);

        protected abstract void HandleInput(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

    }
}
