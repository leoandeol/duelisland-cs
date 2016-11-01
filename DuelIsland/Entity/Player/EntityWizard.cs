using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuelIsland.Entity;
using DuelIsland.Items;
using DuelIsland.Manager;
using DuelIsland.Screens;
using DuelIsland.Tool;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DuelIsland.Entity.Player
{
    public class EntityWizard : EntityPlayer
    {
        #region Fields
        private List<Bullet> bullets;
        #endregion

        public EntityWizard(Vector2 pos, string name, string textureAssetName, byte id_player)
        {
            this.animations = new List<Animation>();
            for(int i = 0; i < 8; i++)
            {
                this.animations.Add(new Animation(i * 8, 8, 8, 2, 100, textureAssetName));
            }
            this.currentAnimation = animations[0];
            this.movement = Vector2.Zero;
            this.speed = 0.032f;
            this.life = maxLife;
            this.stamina = maxStamina;
            this.xp = 0;
            this.hitbox = new Rectangle((int)pos.X, (int)pos.Y, 8, 8);
            this.name = name;
            this.level = 1;
            this.damages = 5;
            this.inventory = new List<Item>();
            this.id = id_player;
            this.keys = new Keys[2][];
            this.bullets = new List<Bullet>();
            for (int i = 0; i < 2; i++)
            {
                this.keys[i] = new Keys[5];
                switch (i)
                {
                    case 0:
                        this.keys[i][0] = Keys.Z;
                        this.keys[i][1] = Keys.S;
                        this.keys[i][2] = Keys.Q;
                        this.keys[i][3] = Keys.D;
                        this.keys[i][4] = Keys.Space;
                        break;
                    case 1:
                        this.keys[i][0] = Keys.Up;
                        this.keys[i][1] = Keys.Down;
                        this.keys[i][2] = Keys.Left;
                        this.keys[i][3] = Keys.Right;
                        this.keys[i][4] = Keys.Enter;
                        break;
                }
            }
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, KeyboardState oldKeyboardState)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].Update(gameTime) || TestBulletColisions(new Vector2(bullets[i].Hitbox.X, bullets[i].Hitbox.Y)))
                    bullets.Remove(bullets[i]);
            }
            #region Animation & move
            if (keyboardState.IsKeyDown(this.keys[(int)(this.id - 1)][0]))
			{
				this.movement = new Vector2(0f, -1f);
                if (World.World.GetClosestTile(new Vector2((float)this.hitbox.X, (float)this.hitbox.Y)).Type == World.TileType.Water)
				{
					this.currentAnimation = this.animations[5];
				}
				else
				{
					this.currentAnimation = this.animations[1];
				}
			}
			else
			{
				if (keyboardState.IsKeyDown(this.keys[(int)(this.id - 1)][1]))
				{
					this.movement = new Vector2(0f, 1f);
                    if (World.World.GetClosestTile(new Vector2((float)this.hitbox.X, (float)this.hitbox.Y)).Type == World.TileType.Water)
					{
						this.currentAnimation = this.animations[4];
					}
					else
					{
						this.currentAnimation = this.animations[0];
					}
				}
				else
				{
					if (keyboardState.IsKeyDown(this.keys[(int)(this.id - 1)][2]))
					{
						this.movement = new Vector2(-1f, 0f);
                        if (World.World.GetClosestTile(new Vector2((float)this.hitbox.X, (float)this.hitbox.Y)).Type == World.TileType.Water)
						{
							this.currentAnimation = this.animations[6];
						}
						else
						{
							this.currentAnimation = this.animations[2];
						}
					}
					else
					{
						if (keyboardState.IsKeyDown(this.keys[(int)(this.id - 1)][3]))
						{
							this.movement = new Vector2(1f, 0f);
                            if (World.World.GetClosestTile(new Vector2((float)this.hitbox.X, (float)this.hitbox.Y)).Type == World.TileType.Water)
							{
								this.currentAnimation = this.animations[7];
							}
							else
							{
								this.currentAnimation = this.animations[3];
							}
						}
						else
						{
							this.movement = new Vector2(0f, 0f);
						}
					}
				}
			}
            if (movement != Vector2.Zero && !TestColisions(movement * speed * gameTime.ElapsedGameTime.Milliseconds))
            {
                currentAnimation.Update(gameTime);
                hitbox.X += (int)Math.Round(movement.X * speed * gameTime.ElapsedGameTime.Milliseconds);
                hitbox.Y += (int)Math.Round(movement.Y * speed * gameTime.ElapsedGameTime.Milliseconds);
            }
            TestItemsColisions();
            #endregion
            #region Other actions
            if (keyboardState.IsKeyDown(this.keys[(int)(this.id - 1)][4]) && stamina >= 1)
            {
                int animation_id = 0;
                for (animation_id = 0; animation_id < animations.Count; animation_id++)
                {
                    if (animations[animation_id] == currentAnimation)
                        break;
                }
                this.stamina--;
                this.stamina = (int)this.stamina;
                Vector2 bulletPos = new Vector2(hitbox.X, hitbox.Y);
                switch (animation_id)
                {
                    case 0:
                        bulletPos.Y += 8;
                        break;
                    case 1:
                        bulletPos.Y -= 8;
                        break;
                    case 2:
                        bulletPos.X -= 8;
                        break;
                    case 3:
                        bulletPos.X += 8;
                        break;
                }
                #region Calcul du mouvement de la boule
                Vector2 mov = Vector2.Zero;
                switch (animation_id)
                {
                    case 0:
                    case 4:
                        mov = new Vector2(0, 1);
                        break;
                    case 1:
                    case 5:
                        mov = new Vector2(0, -1);
                        break;
                    case 2:
                    case 6:
                        mov = new Vector2(-1, 0);
                        break;
                    case 3:
                    case 7:
                        mov = new Vector2(1, 0);
                        break;
                }
                #endregion
                bullets.Add(new Bullet(ResourceManager.GetTexture(@"Textures\Spritesheets\char_wizard_male"), new Rectangle(0, 72, 8, 8), bulletPos, mov));
                //DestructBlock();
            }
            #endregion
            #region Tests & arrangement of player values
            if (stamina < maxStamina)
            {
                stamina += 0.002f * gameTime.ElapsedGameTime.Milliseconds;
            }
            else if (stamina > maxStamina)
            {
                stamina = maxStamina;
            }
            if (World.World.GetClosestTile(new Vector2(hitbox.X, hitbox.Y)).Type == World.TileType.Water)
            {
                stamina -= (float)(0.004 * gameTime.ElapsedGameTime.Milliseconds);
            }
            if (stamina < 0)
                stamina = 0;
            if (stamina < 1 && World.World.GetClosestTile(new Vector2(hitbox.X, hitbox.Y)).Type == World.TileType.Water)
            {
                life -= (float)(0.004 * gameTime.ElapsedGameTime.Milliseconds);
            }
            if (xp >= maxXp)
            {
                xp = (short)(xp % maxXp);
                this.maxLife++;
                this.maxStamina++;
                this.life = this.maxLife;
                this.stamina = this.maxStamina;
                level++;
            }
            #endregion

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }
            currentAnimation.Draw(spriteBatch, hitbox);
        }

        private void DestructBlock()
        {
            Vector2 blockPos;
            blockPos.X = World.World.GetClosestTile(new Vector2(hitbox.X, hitbox.Y) + movement).Hitbox.X;
            blockPos.Y = World.World.GetClosestTile(new Vector2(hitbox.X, hitbox.Y) + movement).Hitbox.Y;
            int animation_id = 0;
            for (animation_id = 0; animation_id < animations.Count; animation_id++)
            {
                if (animations[animation_id] == currentAnimation)
                    break;
            }
            switch (animation_id)
            {
                case 0:
                    blockPos.Y += 8;
                    break;
                case 1:
                    blockPos.Y -= 8;
                    break;
                case 2:
                    blockPos.X -= 8;
                    break;
                case 3:
                    blockPos.X += 8;
                    break;
            }
            for (int x = 0; x < 120; x++)
            {
                for (int y = 0; y < 120; y++)
                {
                    if (World.World.BlockEntities[x][y] != null && World.World.BlockEntities[x][y].Hitbox.Intersects(new Rectangle((int)blockPos.X, (int)blockPos.Y, 8, 8)))
                    {
                        World.World.BlockEntities[x][y].Activate(this.damages);
                    }
                }
            }
        }

        private bool TestColisions(Vector2 pos)
        {
            Camera2D camera;
            if (id == 1)
                camera = GameplayScreen.Camera1;
            else
                camera = GameplayScreen.Camera2;
            for (int x = (int)((camera.Pos.X / 8) - (Math.Round((double)(1280 / 8))) / 8);
                x < (int)((camera.Pos.X / 8) + (Math.Round((double)(1280 / 8))) / 8); x++)
            {
                for (int y = (int)((camera.Pos.Y / 8) - (Math.Round((double)(720 / 8))) / 8);
                    y < (int)((camera.Pos.Y / 8) + (Math.Round((double)(720 / 8))) / 8); y++)
                {
                    
                    if(x >= 0 && x < 120 && y >= 0 && y < 120)
                    {
                        if (World.World.BlockEntities[x][y] != null && World.World.BlockEntities[x][y].Hitbox.Intersects(new Rectangle((int)Math.Round(hitbox.X + movement.X * 2),
                            (int)Math.Round(hitbox.Y + movement.Y * 2), 8, 8)) && World.World.BlockEntities[x][y].TestColisions == true)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool TestBulletColisions(Vector2 pos)
        {
            if (id == 1 && GameplayScreen.Player2.Hitbox.Intersects(new Rectangle((int)pos.X, (int)pos.Y, 8, 8)))
            {
                GameplayScreen.Player2.Life -= this.damages;
                return true;
            }
            else if (id == 2 && GameplayScreen.Player1.Hitbox.Intersects(new Rectangle((int)pos.X, (int)pos.Y, 8, 8)))
            {
                GameplayScreen.Player1.Life -= this.damages;
                return true;
            }
            else if (World.World.GetClosestBlock(pos) != null && World.World.GetClosestBlock(pos).Hitbox.Intersects(new Rectangle((int)pos.X, (int)pos.Y, 8, 8)))
            {
                World.World.BlockEntities[World.World.GetClosestBlock(pos).Hitbox.X / 8][World.World.GetClosestBlock(pos).Hitbox.Y / 8].Activate(damages);
                return true;
            }
            return false;
        }

        private void TestItemsColisions()
        {
            foreach (Item item in World.World.Items)
            {
                if (item != null && this.hitbox.Intersects(item.Hitbox))
                {
                    Item.Contact.Play();
                    inventory.Add(item);
                    if (item.Type == ItemType.XP)
                    {
                        xp += 10;
                    }
                    World.World.Items.Remove(item);
                    break;
                }
            }
        }

        /*private bool TestColisionsPerPixels(Entity entity)
        {
            int top = Math.Max(Hitbox.Top, entity.Hitbox.Top);
            int bottom = Math.Min(Hitbox.Bottom, entity.Hitbox.Bottom);
            int left = Math.Max(Hitbox.Left, entity.Hitbox.Left);
            int right = Math.Min(Hitbox.Right, entity.Hitbox.Right);
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color colorA = this.textureData[(x - Hitbox.Left) + (y - Hitbox.Top) * Hitbox.Width];
                    Color colorB = entity.textureData[(x - entity.Hitbox.Left) + (y - entity.Hitbox.Top) * entity.Hitbox.Width];
                    if (colorA.A != 0 && colorB.A != 0)
                        return true;
                }
            }
            return false;
        }*/

        public override void Explode()
        {
            int xp_count = xp + (level - 1) * 100 + 50;
            xp_count /= 10;
            for (int i = 0; i < xp_count; i++)
            {
                World.World.Items.Add(new ItemXP(new Vector2(hitbox.X, hitbox.Y), xp_count*9));
            }
            level = 1;
            Random rnd = new Random();
            Vector2 newpos = Vector2.Zero;
            do
            {
                newpos = new Vector2(rnd.Next(0, 120) * 8, rnd.Next(0, 120) * 8);
            }
            while (World.World.GetClosestTile(newpos).Type == World.TileType.Water ||
                World.World.GetClosestBlock(newpos) != null);
            hitbox = new Rectangle((int)newpos.X, (int)newpos.Y, 8, 8);
            this.maxLife = 10;
            this.maxStamina = 10;
            this.life = maxLife;
            this.stamina = maxStamina;
        }
    }
}
