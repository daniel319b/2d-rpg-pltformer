using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine.UI
{
    public class InventoryWindow : Window
    {

        public InventoryWindow() : base(new Vector2(10), TextureManager.SetTexture("form")) { }
    }
}
