using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DuelIsland.Manager;

namespace DuelIsland.Entity
{
    public abstract class EntityBlock : Entity
    {
        protected static Texture2D texture = ResourceManager.GetTexture(@"Textures\Tilesets\terrain");
        protected Rectangle sourceRectangle;
        protected bool testColisions = true;

        public bool TestColisions
        {
            get { return testColisions; }
        }
        /// <summary>
        /// Bla
        /// </summary>
        /// <returns>The amount of life gained (+) or lost (-)</returns>
        public virtual float Activate(short damages)
        {
            life -= damages;
            return 0;
        }
        public abstract void Update(GameTime gameTime);
    }
}
