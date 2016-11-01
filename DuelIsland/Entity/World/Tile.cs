using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DuelIsland.Manager;

namespace DuelIsland.Entity.World
{
    public enum TileType
    {
        Grass,
        Sand, 
        Water
    }

    public class Tile
    {
        private static Texture2D texture = ResourceManager.GetTexture(@"Textures\Tilesets\terrain");
        private Rectangle hitbox;
        private TileType type;
        private Rectangle sourceRectangle;

        public Rectangle Hitbox
        {
            get { return hitbox; }
            set { hitbox = value; }
        }

        public TileType Type
        {
            get { return type; }
        }

        public Tile(TileType type, Vector2 pos)
        {
            this.type = type;
            this.hitbox = new Rectangle((int)pos.X, (int)pos.Y, 8, 8);
            switch (this.type)
            {
                case TileType.Grass:
                    sourceRectangle = new Rectangle(0, 0, 8, 8);
                    break;
                case TileType.Sand:
                    sourceRectangle = new Rectangle(8, 0, 8, 8);
                    break;
                case TileType.Water:
                    sourceRectangle = new Rectangle(16, 0, 8, 8);
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitbox, sourceRectangle, Color.White);
        }

        public void SetType(TileType type)
        {
            this.type = type;
            switch (this.type)
            {
                case TileType.Grass:
                    sourceRectangle = new Rectangle(0, 0, 8, 8);
                    break;
                case TileType.Sand:
                    sourceRectangle = new Rectangle(8, 0, 8, 8);
                    break;
                case TileType.Water:
                    sourceRectangle = new Rectangle(16, 0, 8, 8);
                    break;
            }
        }
    }
}
