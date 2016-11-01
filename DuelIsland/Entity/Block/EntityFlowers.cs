using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using DuelIsland.Items;
using DuelIsland.Manager;
using DuelIsland.Entity.World;


namespace DuelIsland.Entity.Block
{
    public class EntityFlowers : EntityBlock
    {
        static SoundEffect collect = ResourceManager.GetSound(@"Sounds\collect");

        public EntityFlowers(Vector2 pos)
        {
            hitbox = new Rectangle((int)pos.X, (int)pos.Y, 8, 8);
            sourceRectangle = new Rectangle(16, 8, 8, 8);
            life = 1;
            testColisions = false;
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, hitbox, sourceRectangle, Color.White);
        }

        public override float Activate(short damages)
        {
            life = -1;
            Explode();
            return 0;
        }

        public override void Explode()
        {
            collect.Play();
            for (int i = 0; i < 2; i++)
            {
                World.World.Items.Add(new ItemFlower(new Vector2(hitbox.X + (8 * i), hitbox.Y)));
            }
        }
    }
}
