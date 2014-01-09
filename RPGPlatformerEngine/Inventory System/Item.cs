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

    public interface IPickable
    {
        void Pick();

        Rectangle BoundBox { get; }


    }

    #region Potions Interfaces / Classes

    public enum PotionType
    {
        Health,
        Armor,
        Magic,
        Poison
    }

    public interface IPotion : IPickable
    {
        PotionType PotionType { get; }

        void Drink();
    }

    //public class HealthPotion : Item, IPotion
    //{
    //    public PotionType PotionType { get { return PotionType.Health; } }

    //    public static int HealthBonus = 10;

    //    public void Drink()
    //    {
    //        Session.Singleton.Player.CurrentStatistics.Health += HealthBonus;
    //    }

    //    public void Pick()
    //    {
    //        Session.Singleton.Player.Inventory.AddItem(new InventoryItem("Health Potion", TextureManager.SetTexture("Items/Potion")));
    //    }

    //    public Rectangle BoundBox
    //    {
    //        get { return new Rectangle(); }
    //    }
    //}

    //public class PoisonPotion : Item, IPotion
    //{
    //    public PotionType PotionType { get { return PotionType.Poison; } }
        
    //    public void Pick()
    //    {
    //        Session.Singleton.Player.Inventory.AddItem(new InventoryItem("Poison Potion", TextureManager.SetTexture("Items/poison_potion")));
    //    }

    //    public void Drink()
    //    {
    //        Session.Singleton.Player.CurrentStatistics.Health += PoisonHealthHit;
    //    }

    //    public static int PoisonHealthHit = 10;


    //    public Rectangle BoundBox
    //    {
    //        get { return new Rectangle(); }
    //    }
    //}

#endregion

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


    public abstract class PickableItem : GameObject, IPickable
    {
        //public new abstract Texture2D Texture { get; }

        public abstract string Name { get; }

        public Rectangle BoundBox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        public PickableItem()
        {
            Scale = 0.5f;
        }

        public void Pick()
        {
            Session.Singleton.Player.Inventory.AddItem(new InventoryItem(Name, Texture));
            Alive = false;
        }
    }

    public class HealthPotion : PickableItem, IPotion
    {
        public static int HealthBonus = 10;

        public override Texture2D Texture
        {
            get { return TextureManager.SetTexture("Items/Potion"); }
        }

        public override string Name
        {
            get { return "Health Potion"; }
        }

        public PotionType PotionType
        {
            get { return PotionType.Health; }
        }

        public void Drink()
        {
            Session.Singleton.Player.CurrentStatistics.Health += HealthBonus;
        }
    }


    public class PoisonPotion : PickableItem, IPotion
    {
        public static int PoisonHealthHit = 10;

        public override Texture2D Texture
        {
            get { return TextureManager.SetTexture("Items/poison_potion"); }
        }

        public override string Name
        {
            get { return "Poison Potion"; }
        }

        public PotionType PotionType
        {
            get { return PotionType.Poison; }
        }

        public void Drink()
        {
            Session.Singleton.Player.CurrentStatistics.Health += PoisonHealthHit;
        }
    }
}

