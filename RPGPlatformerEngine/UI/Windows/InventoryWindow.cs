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

        InventoryManager inventoryManager;

        public InventoryWindow() : base (new Vector2(10), TextureManager.SetTexture("UI/form"))
        {
            player = Session.Singleton.Player;
            inventory = player.Inventory;
            inventoryManager = new InventoryManager();
            inventoryManager.AddInventory(inventory);
        }

        public override void Update()
        {
            base.Update();
            inventoryManager.Update();
            inventory.Position = Position + new Vector2(5, 35) ;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //base.Draw(spriteBatch);
            //Change that to Vector Size 
            spriteBatch.Draw(Texture, new Rectangle(BoundBox.X, BoundBox.Y, BoundBox.Width - 355, BoundBox.Height - 7), Color.White);
            inventoryManager.Draw(spriteBatch);
        }
    }
}
