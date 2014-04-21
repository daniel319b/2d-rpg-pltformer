using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGPlatformer
{
    public class ScreenManager
    {
        public GraphicsDevice GraphicsDevice { get; private set; }

        public List<Screen> Screens { get; private set; }

        public ScreenManager(Game game)
        {
            GraphicsDevice = game.GraphicsDevice;
            MenuEntry.SelectedColor = Color.Gold;
        }

        public void LoadContent()
        {
            foreach (Screen s in Screens) s.LoadContent();
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void AddScreen(Screen screen)
        {
            screen.LoadContent();
            Screens.Add(screen);
        }
    }
}
