using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using DuelIsland.Manager;

namespace DuelIsland.Items
{
    public enum ItemType
    {
        Rock,
        Wood,
        Flower, 
        XP
    }

    public abstract class Item
    {
        #region Fields
        // Static
        protected static Texture2D texture = ResourceManager.GetTexture(@"Textures\Tilesets\items");
        protected static SoundEffect contact = ResourceManager.GetSound(@"Sounds\pickup");
        // Protected
        protected Rectangle hitbox;
        protected Rectangle sourceRectangle;
        protected float scale = 1;
        protected float scale_speed = 0.01f;
        protected ItemType type;
        #endregion

        #region Properties
        public Rectangle Hitbox
        {
            get { return hitbox; }
        }
        public ItemType Type
        {
            get { return type; }
        }
        public static Texture2D Texture
        {
            get { return texture; }
        }
        public static SoundEffect Contact
        {
            get { return contact; }
        }
        #endregion

        public virtual void Update()
        {
            scale += scale_speed;
            if (scale < 0.6f || scale > 1.4f)
                scale_speed = -scale_speed;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(hitbox.X, hitbox.Y), sourceRectangle, Color.White, 0f, new Vector2(4, 4), scale, SpriteEffects.None, 0f);
        }
    }
}
