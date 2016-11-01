using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using DuelIsland.Manager;
using DuelIsland.Items;

namespace DuelIsland.Entity.Block
{
    public class EntityTree : EntityBlock
    {
        static SoundEffect chop = ResourceManager.GetSound(@"Sounds\mine");
        static SoundEffect explode = ResourceManager.GetSound(@"Sounds\explode");

        public EntityTree(Vector2 pos)
        {
            hitbox = new Rectangle((int)pos.X, (int)pos.Y, 8, 8);
            sourceRectangle = new Rectangle(8, 8, 8, 8);
            life = 5;
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
            life -= damages;
            if (Life > 0)
            {
                chop.Play();
            }
            else if (Life <= 0)
            {
                Explode();
            }
            return 0;
        }

        public override void Explode()
        {
            explode.Play();
            for (int i = 0; i < 2; i++)
            {
                World.World.Items.Add(new ItemWood(new Vector2(hitbox.X + (8 * i), hitbox.Y)));
            }
        }
    }
}
