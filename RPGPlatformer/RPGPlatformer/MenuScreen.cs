using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RPGPlatformer
{
    class MenuScreen : Screen
    {
        public virtual string Title { get; set; }

        public List<MenuEntry> MenuEntries { get; set; }

     
        public override void Update(GameTime gameTime)
        {
           
        }

        public override void LoadContent()
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }


    class MainMenu : MenuScreen
    {
        public override string Title
        {
            get
            {
                return "Main Menu";
            }
          
        }
    }
}
