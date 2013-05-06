using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RPGPlatformerEngine
{
    /// <summary>
    /// The inventory manager is a component which manages one or more Inventory instances.
    /// Calls their Update and Draw methods, and manages their relationship.
    /// It manages the items that are in the inventories,how to move them,stack them, etc..
    /// </summary>
    class InventoryManager
    {
        List<Inventory> inventories;

        /// <summary>
        /// The list of the current inventories on the screen.
        /// </summary>
        public List<Inventory> Inventories
        {
            get { return inventories; }
            set { inventories = value; }
        }

        /// <summary>
        /// List of all the inventory blocks in the world.
        /// </summary>
        List<InventoryBlock> WorldBlocks;
        Item MouseItem;
        ConfirmBox throwConfirm;
        /// <summary>
        /// Initializes a new Inventory manager.
        /// </summary>
        public InventoryManager()
        {
            inventories = new List<Inventory>();
            WorldBlocks = new List<InventoryBlock>();
        }

        /// <summary>
        /// Adds a new Inventory to the list of inventories.
        /// </summary>
        /// <param name="inventory"></param>
        public void AddInventory(Inventory inventory)
        {
            inventories.Add(inventory);
            foreach (InventoryBlock b in inventory.Blocks)
                WorldBlocks.Add(b);//Adds the inventory blocks to the blocks list.
        }

        public void Update()
        {
            foreach (Inventory i in inventories)         
                i.Update();


            if (throwConfirm == null)//if you are not throwing anything right now.
            {
                ClickedOutsideOfInventory();
                foreach (InventoryBlock b in WorldBlocks)
                    HandleBlockClick(b);
            }
            else
                throwConfirm.Update();

            if (MouseItem != null)
            {
                MouseItem.Position = new Vector2(Input.MousePosition.X, Input.MousePosition.Y);
                MouseItem.Update();
                MouseItem.Scale = 0.9f;
            }       
        }

        /// <summary>
        /// Checks if the user clicked the mouse button outside of an Inventory window.
        /// </summary>
        private void ClickedOutsideOfInventory()
        {
            bool[] intersecting = new bool[inventories.Count];
            if (MouseItem != null && Input.LeftButtonPressed())
            {
                for (int i = 0; i < intersecting.Length; i++)
                    intersecting[i] = inventories[i].Bounds.Intersects(Input.MouseRectangle);

                bool flag = false;
                foreach (bool t in intersecting.Where(t => t))
                    flag = true;
                
                if (flag == false)
                {
                     throwConfirm = new ConfirmBox(new Vector2(Input.MousePosition.X - 100, Input.MousePosition.Y-100), "Drop Item?");
                     throwConfirm.No.Clicked += new EventHandler(No_Clicked);
                     throwConfirm.Yes.Clicked += new EventHandler(Yes_Clicked);
                }
            }
        }

        void Yes_Clicked(object sender, EventArgs e)
        {
            if (MouseItem != null)
                MouseItem = null;
          
            throwConfirm = null;//delete the message.
           
        }

        void No_Clicked(object sender, EventArgs e)
        {
            throwConfirm = null;//delete the message.
        }


        /// <summary>
        /// Handles what will happen when a block is clicked by the user.
        /// </summary>
        /// <param name="b">The block that was clicked.</param>
        private void HandleBlockClick(InventoryBlock b)
        {
            if (b.IsLeftClicked())
            {
                #region Left Clicked
                if (b.HasItem)//if the block is not empty
                {
                    if (MouseItem == null && !Input.KeyDown(Keys.LeftShift))
                    {
                        MouseItem = new Item(b.Item);
                        MouseItem.Quantity = b.Item.Quantity;
                        b.Item = null;
                    }  
                    else if(MouseItem != null && !Input.KeyDown (Keys.LeftShift ))          
                    {
                        if (MouseItem.Equals(b.Item))
                        {
                            b.Item.Quantity += MouseItem.Quantity;
                            MouseItem = null;
                        }
                        else//the items are not the same, so swap them.
                        {
                            Item temp = new Item(MouseItem); temp.Quantity = MouseItem.Quantity;
                            MouseItem = new Item(b.Item); MouseItem.Quantity = b.Item.Quantity;
                            temp.Position = b.Center;//avoiding bugs....
                            b.Item = temp;
                        }
                    }
                }
                else//if the cell is empty
                {
                    if (MouseItem != null)
                    {
                        b.Item = MouseItem;
                        MouseItem = null;
                    }
                }

                #endregion
            }
            if (b.IsRightClicked())
            {
                #region Right Clicked

                if (b.HasItem)
                {
                    if (MouseItem == null)//if we are not holding anything
                    {
                        MouseItem = new Item(b.Item);
                        MouseItem.Quantity = b.Item.Quantity / 2;
                        if (MouseItem.Quantity < 1)
                            MouseItem.Quantity = 1;
                        b.Item.Quantity -= MouseItem.Quantity;
                    }
                    else if (MouseItem.Equals(b.Item))//if we are holding something and it's the same as the block's.
                    {
                        b.Item.Quantity++;//add one item to the current block
                        MouseItem.Quantity--;//take one item out of the copyItem.            
                    }
                }
                else if (!b.HasItem && MouseItem != null)
                {
                    b.Item = new Item(MouseItem);
                    b.Item.Position = b.Center;
                    MouseItem.Quantity--;
                }
                if (b.HasItem || MouseItem != null)
                {
                    b.Item = b.Item.Quantity >= 1 ? b.Item : null;//check if we took all the items.
                    MouseItem = MouseItem.Quantity >= 1 ? MouseItem : null;//check if we took all the items.
                }
                #endregion
            }

        }

       
        public void Draw(SpriteBatch sb)
        {
            foreach (Inventory i in inventories)
                i.Draw(sb);
            if (MouseItem != null)
            {
                MouseItem.Draw(sb);
                if (MouseItem.Quantity >= 2)
                {
                    sb.Begin();
                    sb.DrawString(Font.Regular, MouseItem.Quantity + "", new Vector2(MouseItem.BoundBox.Right, MouseItem.BoundBox.Top), Color.White);
                    sb.End();
                }
            }
            if (throwConfirm != null)
                throwConfirm.Draw(sb);
        }
    }
}
