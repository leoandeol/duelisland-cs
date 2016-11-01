using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuelIsland.Entity.Player;
using DuelIsland.Items;
using DuelIsland.Manager;
using DuelIsland.Tool;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DuelIsland.Entity
{
    public abstract class EntityPlayer : Entity
    {
        protected string name;
        protected byte maxLife = 10, maxStamina = 10;
        protected byte level;
        protected short damages;
        protected Animation currentAnimation;
        protected List<Animation> animations;
        protected Vector2 movement;
        protected float speed;
        protected float stamina;
        protected short xp;
        protected const short maxXp = 100;
        protected List<Item> inventory;
        protected byte id;
        protected Keys[][] keys;

        #region Properties
        public short Damages
        {
            get { return damages; }
        }
        public float Stamina
        {
            get { return stamina; }
        }
        public short XP
        {
            get { return xp; }
        }
        public byte MaxLife
        {
            get { return maxLife; }
        }
        public byte MaxStamina
        {
            get { return maxStamina; }
        }
        public short MaxXP
        {
            get { return maxXp; }
        }
        public string Name
        {
            get { return name; }
        }
        public byte Level
        {
            get { return level; }
        }
        public List<Item> Inventory
        {
            get { return inventory; }
        }
        public byte ID
        {
            get { return id; }
        }
        #endregion

        public override void Draw(SpriteBatch spritebatch)
        {
            currentAnimation.Draw(spritebatch, hitbox);
            //spritebatch.DrawString(font, name + " lvl " + level.ToString(), new Vector2(hitbox.X, hitbox.Y - 15), Color.Black);
        }

        public virtual void Update(GameTime gameTime, KeyboardState keyboardState, KeyboardState oldKeyboardState)
        {

        }
    }
}
