using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine
{
    public class InventoryBlock : GameObject
    {
        /// <summary>
        /// Determines if the inventory block has an item in it.
        /// </summary>
        public bool HasItem
        {
            get { return item != null; }
        }

        InventoryItem item;

        /// <summary>
        /// The item in this block.
        /// </summary>
        public InventoryItem Item
        {
            get { return item; }
            set { item = value; }
        }

        /// <summary>
        /// The info box which pops when the user hovers over the block.
        /// Displays the name of the item.
        /// </summary>
        public  InfoBox itemInfoBox;

        /// <summary>
        /// Constructs a new Inventory block.
        /// </summary>
        /// <param name="position">The position of the block.</param>
        public InventoryBlock(Vector2 position):base(position)
        {
            texture = Inventory.Block;      
        }

        public override void Update()
        {
            base.Update();
            //to avoid bugs, we make the boundBox a little smaller, for better Intersction.
            boundBox = new Rectangle((int)(position.X - origin.X), (int)(position.Y - origin.Y), texture.Width-10, texture.Height-10);
            if (HasItem)
            {          
                item.Position = new Vector2(boundBox.Center.X, boundBox.Center.Y);              
                item.Update();
            }
            if (Hovering())
            {
                color = new Color(color.R, color.G, color.B, 230);//brighten up a little the block.
                if (HasItem)
                {
                    itemInfoBox = new InfoBox(new Vector2 (boundBox.Right,boundBox.Top), item.Name);
                    itemInfoBox.Update();
                }
               
            }
            else
            {
                color = Color.White;
                itemInfoBox = null;
            }
        }

        public override void Draw(SpriteBatch sb)
        {          
            base.Draw(sb);
            if (item != null)
            {
                item.Draw(sb);
                Vector2 numberSize = Font.Regular.MeasureString(item.Quantity.ToString());
                GameObject number = new GameObject(new Vector2(boundBox.Right- numberSize.X+6, boundBox.Bottom- numberSize.Y+6));
                number.Text = item.Quantity.ToString();
                if (item.Quantity >= 2)
                    number.Draw(sb);
            }
           
        }

        /// <summary>
        /// Checks if the mouse is Hovering on the block.
        /// </summary>
        /// <returns>True if the mouse intersects with the block.</returns>
        public bool Hovering()
        {
            return Input.MouseRectangle.Intersects(boundBox);        
        }

        /// <summary>
        /// Checks if the user has clicked the block with the Left Mouse Button.
        /// </summary>
        /// <returns>True if the block was clicked with the left mouse button..</returns>
        public bool IsLeftClicked()
        {
            return Hovering() && Input.LeftButtonPressed();
        }

        /// <summary>
        /// Checks if the user has clicked the block with the Right Mouse Button.
        /// </summary>
        /// <returns>True if the block was clicked with the Right mouse button..</returns>
        public bool IsRightClicked()
        {
            return Hovering() && Input.RightButtonPressed();
        }

        
    }
}
