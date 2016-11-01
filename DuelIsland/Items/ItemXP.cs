using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DuelIsland.Items
{
    public class ItemXP : Item
    {
        int value;

        public ItemXP(Vector2 position, int v)
        {
            hitbox = new Rectangle((int)position.X+4, (int)position.Y+4, 8, 8);
            sourceRectangle = new Rectangle(24, 0, 8, 8);
            type = ItemType.XP;
            value = v;
        }
    }
}
