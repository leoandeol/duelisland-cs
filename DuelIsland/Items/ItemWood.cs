using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DuelIsland.Items
{
    public class ItemWood : Item
    {
        public ItemWood(Vector2 position)
        {
            hitbox = new Rectangle((int)position.X+4, (int)position.Y+4, 8, 8);
            sourceRectangle = new Rectangle(8, 0, 8, 8);
            type = ItemType.Wood;
        }
    }
}
