using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGPlatformerEngine.Inventory_System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGPlatformerEngine
{
    public enum ItemType
    {
        Weapon,
        Potion,
        Armor,
        Misc
    }

    public class ItemStats
    {
        public int BuyingPrice { get; set; }
        public int SellingPrice { get; set; }
        public string Name { get; set; }
    }


    public class Item
    {
        /// <summary>
        /// The name of the item.
        /// </summary>
        public string Name { get; set; }

        public int BuyingPrice { get; set; }

        public int SellingPrice { get; set; }
    }

    public interface IWeapon : IPickable
    {
        int Damage { get; }


    }

    public interface FireArm : IWeapon
    {
        int FireRate { get; }
        int ReloadTime { get; }
        int ClipSize { get; }
        int BulletsPerClip { get; }

        void Fire();
        void Reload();

    }
 
}

