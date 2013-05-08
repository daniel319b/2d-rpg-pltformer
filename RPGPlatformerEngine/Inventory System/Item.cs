using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPlatformerEngine
{
    public interface Item
    {
        /// <summary>
        /// The name of the item.
        /// </summary>
        public string Name { get; set; }

        public int BuyingPrice { get; set; }

        public int SellingPrice { get; set; }
    }
}
