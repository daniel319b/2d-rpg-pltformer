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
    /// This class implements a basic Inventory window.
    /// </summary>
    class Inventory
    {         
        /// <summary>
        /// The number of rows in the inventory.
        /// </summary>
        int rows;
        /// <summary>
        /// The number of columns in the inventory.
        /// </summary>
        int columns;

        Vector2 position;
        InventoryBlock[,] blocks;

        /// <summary>
        /// The array of the Inventory Blocks which build the window.
        /// </summary>
        public InventoryBlock[,] Blocks
        {
            get { return blocks; }
            set { blocks = value; }
        }

        /// <summary>
        /// The position of the inventory window.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// The bounding rectangle of the inventory window.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                Vector2 RectanglePosition = new Vector2(position.X - blocks[0, 0].Origin.X, position.Y - blocks[0, 0].Origin.Y);
                return new Rectangle((int)RectanglePosition.X, (int)RectanglePosition.Y, blocks[rows-1, columns-1].BoundBox.Right - (int)RectanglePosition.X + 20, blocks[rows-1, columns-1].BoundBox.Bottom - (int)RectanglePosition.Y + 20);
            }
        }

        public bool ShowWindow
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new Inventory window with a given position.
        /// </summary>
        /// <param name="position">The position of the window.</param>
        /// <param name="rows">The number of columns in the inventory window.</param>
        /// <param name="columns">The number of columns in the inventory window.</param>
        public Inventory(Vector2 position,int rows,int columns)
        {
            this.position = position;
            this.rows = rows;
            this.columns = columns;           
            blocks = new InventoryBlock[rows, columns];
            for (int i = 0; i < rows; i++)           
                for (int j = 0; j < columns; j++)
                    blocks[i, j] = new InventoryBlock(new Vector2(position.X + Block.Width * j, position.Y + Block.Height * i));
            ShowWindow = true;
        }

        /// <summary>
        /// Adds a new Item in a given position of the block.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <param name="position">The block position to add the item.</param>
        public void AddItem(Item item,int position)
        {
            if (position >= blocks.Length || position < 0)//check for errors in the input.
                return;
         
            int posY = position / rows;
            int posX = position % rows;
            //To avoid bugs, like the item is flickering..
            item.Position = blocks[posX, posY].Center;
            blocks[posX, posY].Item = item;
        }

        public void Update()
        {
            if (ShowWindow)
            {
                foreach (InventoryBlock b in blocks)
                {
                    b.Update();
                    if (b.IsLeftClicked() && b.HasItem)                        
                        if (Input.KeyDown(Keys.LeftShift))                       
                           GatherSimilarItems(b);
                }         
            }
        }

        /// <summary>
        /// Gathers all the items which are the same as in the given block, and placed in that block.
        /// </summary>
        /// <param name="b">The block that the items of the same kind will be gathered to.</param>
        private void GatherSimilarItems(InventoryBlock b)
        {
            foreach (InventoryBlock block in blocks)
            {
                if (block.HasItem && b.HasItem && block != b &&  block.Item.Equals(b.Item))
                {
                    b.Item.Quantity += block.Item.Quantity;//add the items to the clicked block.
                    block.Item = null;//delete the item in the current Block.
                }
            }
        }

       
        /// <summary>
        /// Draws the inventory window.
        /// </summary>
        /// <param name="sb">A spritebatch for drawing.</param>
        public void Draw(SpriteBatch sb)
        {
            if (ShowWindow)
            {
                foreach (InventoryBlock block in blocks)
                    block.Draw(sb);

                foreach (InventoryBlock block in blocks)
                    if (block.itemInfoBox != null)
                        block.itemInfoBox.Draw(sb);
            }
        }
     
        /// <summary>
        /// Gets the first free block.
        /// </summary>
        /// <returns>The position of the first free block</returns>
        public int getFirstFreeBlock()
        {    
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (blocks[i,j].HasItem == false)
                        return i + j*rows;
                }
            }    
            return -1;
        }

       
        /// <summary>
        /// The block texture.
        /// </summary>
        public static Texture2D Block { get {return TextureManager.SetTexture("Block"); }  }
    }
}
