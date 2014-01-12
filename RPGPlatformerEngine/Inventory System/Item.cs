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


    public abstract class CoinItem : PickableItem
    {
        abstract protected int moneyValue { get; }

        public override string Name
        {
            get { return "Money"; }
        }

        abstract protected Animation animation { get; }

        public override void Pick()
        {
            // here we need to add to the money of the player since we are not treating 'money' as inventory item.
            Session.Singleton.Player.CurrentStatistics.Money += moneyValue;
        }

        public override void Update()
        {
            base.Update();
            animation.Update(Session.Singleton.GameTime);
        }    }

    public class BronzeCoin : CoinItem
    {
        protected override int moneyValue { get { return 10; } }

        public override Texture2D Texture { get { return TextureManager.SetTexture("coin_bronze"); } }

        protected override Animation animation { get { return new Animation(TextureManager.SetTexture("coin_bronze_animation"), 3, 1, 10); } }
    }

    public class GoldCoin : CoinItem
    {
        protected override int moneyValue { get { return 50; } }

        public override Texture2D Texture { get { return TextureManager.SetTexture("coin_gold"); } }

        protected override Animation animation { get { return new Animation(TextureManager.SetTexture("coin_gold_animation"), 3, 1, 10); } }

    }
}

