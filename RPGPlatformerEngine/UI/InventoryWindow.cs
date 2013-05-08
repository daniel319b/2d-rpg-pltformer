using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGPlatformerEngine.UI
{
    public class InventoryWindow : Window
    {
        Player player;
        Inventory inventory;

        public InventoryWindow() : base(new Vector2(10), TextureManager.SetTexture("form"))
        {
            player = Session.Singleton.Player;
            inventory = player.Inventory;
        }

        public override void Update()
        {
            base.Update();
            inventory.Update();
            inventory.Position = Position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            inventory.Draw(spriteBatch);
        }
    }
}
