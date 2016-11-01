using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuelIsland.Entity;
using DuelIsland.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace DuelIsland.Entity.Block
{
    public class EntityCactus : EntityBlock
    {
        static SoundEffect collect = ResourceManager.GetSound(@"Sounds\collect");
        static SoundEffect explode = ResourceManager.GetSound(@"Sounds\explode");

        public EntityCactus(Vector2 pos)
        {
            hitbox = new Rectangle((int)pos.X, (int)pos.Y, 8, 8);
            sourceRectangle = new Rectangle(24, 8, 8, 8);
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
                collect.Play();
            }
            else if (Life <= 0)
            {
                Explode();
            }
            return 0.1f;
        }

        public override void Explode()
        {
            explode.Play();
            /*for (int i = 0; i < 2; i++)
            {
                explode.Play();
                World.Items.Add(new ItemWood(new Vector2(hitbox.X + (8 * i), hitbox.Y)));
            }*/
        }
    }
}
