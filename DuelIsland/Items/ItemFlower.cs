using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DuelIsland.Items
{
    public class ItemFlower : Item
    {
        public ItemFlower(Vector2 position)
        {
            hitbox = new Rectangle((int)position.X+4, (int)position.Y+4, 8, 8);
            sourceRectangle = new Rectangle(16, 0, 8, 8);
            type = ItemType.Flower;
        }
    }
}
