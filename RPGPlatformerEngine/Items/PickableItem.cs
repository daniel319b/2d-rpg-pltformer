using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine
{
    /// <summary>
    /// An interface for a pickable object
    /// </summary>
    public interface IPickable
    {
        void Pick();
    }

    /// <summary>
    /// An abstract class that represents pickable items on the map.
    /// </summary>
    public abstract class PickableItem : GameObject, IPickable
    {
        /// <summary>
        /// The name of this item.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The Bounding Rectangle of this item.
        /// </summary>
        new public Rectangle BoundBox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, (int)(Texture.Width * Scale), (int)(Texture.Height * Scale)); }
        }

        public PickableItem()
        {
            Scale = 0.5f;
        }

        /// <summary>
        /// Picks ups this item - adds it to the players inventory.
        /// </summary>
        public virtual void Pick()
        {
            Session.Singleton.Player.Inventory.AddItem(new InventoryItem(Name, Texture));
            Alive = false;
        }
    }

}
