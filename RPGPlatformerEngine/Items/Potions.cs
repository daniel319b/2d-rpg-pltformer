using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace RPGPlatformerEngine
{
    public enum PotionType
    {
        Health,
        Armor,
        Magic,
        Poison
    }

    /// <summary>
    /// An interface the represents a Potion, with a method Drink and a PotionType.
    /// </summary>
    public interface IPotion : IPickable
    {
        PotionType PotionType { get; }

        /// <summary>
        /// The player drinks this potion.
        /// </summary>
        void Drink();
    }

    /// <summary>
    /// A class that represents a health potion item in the game.
    /// </summary>
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

    /// <summary>
    /// A class that represents a poison potion.
    /// </summary>
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
