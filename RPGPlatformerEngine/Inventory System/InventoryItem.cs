﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine
{
    /// <summary>
    /// This class represents an item icon in the inventory.
    /// </summary>
    public class InventoryItem : GameObject
    {  
        int quantity = 1;
        string name = "";

        /// <summary>
        /// The name of the item
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// The quantity of the item.
        /// </summary>
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
    
        /// <summary>
        /// Create a new item object.
        /// </summary>
        /// <param name="itemName">The name of the item and the name of the texture file.</param>
        public InventoryItem(string itemName)
        {
            Name = itemName;
            texture = TextureManager.SetTexture("Items/" + itemName);
        }

        /// <summary>
        /// Create a new item object.
        /// </summary>
        /// <param name="itemName">The name of the item/</param>
        /// <param name="itemTexture">The texture of the item</param>
        public InventoryItem(string itemName,Texture2D itemTexture)
        {       
            name = itemName;
            texture = itemTexture;
        }

        /// <summary>
        /// A Copy constructor.
        /// </summary>
        /// <param name="other">The other Item object to copy from.</param>
        public InventoryItem(InventoryItem other)
        {
            name = other.Name;
            texture = other.Texture;             
        }

        /// <summary>
        /// Updates the inventory item.
        /// </summary>
        public override void Update()
        {
            base.Update();   
           
            if (Hover())
                OnHover();
            else
                Normal();    
        }
        

        /// <summary>
        /// Checks if two items are the same
        /// </summary>
        /// <param name="other">The other item to check with.</param>
        /// <returns>True if this item equals to the other item.</returns>
        public bool Equals(InventoryItem other)
        {
            return name == other.name && texture == other.texture;
        }

        private void Normal()
        {
            scale = 1;
        }

        private void OnHover()
        {
            scale += 0.02f;
            if (scale >= 1.1f)
                scale = 1.1f;
        }

        private bool Hover()
        {
            return Input.MouseRectangle.Intersects(BoundBox);
        }
        
    }
}
