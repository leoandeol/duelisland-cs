using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DuelIsland.Entity.Player
{
    public class Bullet
    {
        Texture2D texture;
        Rectangle hitbox, sourceRectangle;
        Vector2 movement;
        const float speed = 0.05f;
        int totalTime;
        const int maxTime = 5000;
        public Rectangle Hitbox
        {
            get { return hitbox; }
        }

        public Bullet(Texture2D texture, Rectangle sourceRectangle, Vector2 pos, Vector2 movement)
        {
            this.texture = texture;
            this.sourceRectangle = sourceRectangle;
            this.movement = movement;
            this.hitbox = new Rectangle((int)pos.X, (int)pos.Y, 8, 8);
            this.totalTime = 0;
        }

        public bool Update(GameTime gameTime)
        {
            totalTime += gameTime.ElapsedGameTime.Milliseconds;
            if(hitbox.X < 0 || hitbox.Y < 0 || (hitbox.X > 600 * 8) || (hitbox.Y > 320 * 8))
                return true;
            else if (totalTime >= maxTime)
                return true;
            hitbox.X += (int)Math.Round(movement.X * speed * gameTime.ElapsedGameTime.Milliseconds);
            hitbox.Y += (int)Math.Round(movement.Y * speed * gameTime.ElapsedGameTime.Milliseconds);
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitbox, sourceRectangle, Color.White);
        }
    }
}
