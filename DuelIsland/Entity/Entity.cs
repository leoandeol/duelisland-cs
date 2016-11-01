using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DuelIsland.Entity
{
    public abstract class Entity
    {
        protected Rectangle hitbox;
        protected float life;

        public Rectangle Hitbox
        {
            get { return hitbox; }
        }
        public float Life
        {
            get { return life; }
            set { life = value; }
        }

        public abstract void Explode();
        public abstract void Draw(SpriteBatch spritebatch);
    }
}
