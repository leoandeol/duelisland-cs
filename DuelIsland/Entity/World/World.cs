using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuelIsland.Entity;
using DuelIsland.Entity.Block;
using DuelIsland.Entity.Player;
using DuelIsland.Items;
using DuelIsland.Screens;
using DuelIsland.Tool;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DuelIsland.Entity.World
{
    public class World
    {
        public static Tile[][] Tiles;
        public static EntityBlock[][] BlockEntities;
        public static List<Item> Items;
        Random rnd;

        public static Tile[][] Tile
        {
            get { return Tiles; }
            set { Tiles = value; }
        }

        public World()
        {
            rnd = new Random();
            Items = new List<Item>();
            Tiles = new Tile[120][];
            BlockEntities = new EntityBlock[120][];
            for (int x = 0; x < 120; x++)
            {
                Tiles[x] = new Tile[120];
                BlockEntities[x] = new EntityBlock[120];
                for (int y = 0; y < 120; y++)
                {
                    Tiles[x][y] = new Tile(TileType.Grass, new Vector2(x * 8, y * 8));
                    BlockEntities[x][y] = null;
                }
            }
            for (int x = 0; x < 40; x++)
            {
                for (int y = 0; y < 120; y++)
                {
                    Tiles[x][y] = new Tile(TileType.Water, new Vector2(x * 8, y * 8));
                }
            }
            for (int x = 0; x < 120; x++)
            {
                for (int y = 0; y < 40; y++)
                {
                    Tiles[x][y] = new Tile(TileType.Water, new Vector2(x * 8, y * 8));
                }
            }
            for (int x = 80; x < 120; x++)
            {
                for (int y = 0; y < 120; y++)
                {
                    Tiles[x][y] = new Tile(TileType.Water, new Vector2(x * 8, y * 8));
                }
            }
            for (int x = 0; x < 120; x++)
            {
                for (int y = 80; y < 120; y++)
                {
                    Tiles[x][y] = new Tile(TileType.Water, new Vector2(x * 8, y * 8));
                }
            }
            for (int x = 40; x < 80; x++)
            {
                for (int y = 40; y < 80; y++)
                {
                    TileType tmp = TileType.Grass;
                    int factor = 0;
                    switch ((int)Tiles[x - 1][y].Type)
                    {
                        case 0:
                            factor += 6;
                            break;
                        case 1:
                            factor += 4;
                            break;
                        case 2:
                            factor += 2;
                            break;
                    }
                    switch ((int)Tiles[x][y - 1].Type)
                    {
                        case 0:
                            factor += 6;
                            break;
                        case 1:
                            factor += 4;
                            break;
                        case 2:
                            factor += 2;
                            break;
                    }
                    switch ((int)Tiles[x - 1][y - 1].Type)
                    {
                        case 0:
                            factor += 6;
                            break;
                        case 1:
                            factor += 4;
                            break;
                        case 2:
                            factor += 2;
                            break;
                    }
                    int a = rnd.Next(0, factor);
                    if (a != 0)
                    {
                        int b = rnd.Next(1, 4);
                        switch (b)
                        {
                            case 1:
                                tmp = Tiles[x - 1][y].Type;
                                break;
                            case 2:
                                tmp = Tiles[x][y - 1].Type;
                                break;
                            case 3:
                                tmp = Tiles[x - 1][y - 1].Type;
                                break;
                        }
                    }
                    else
                    {
                        switch (rnd.Next(0, 6))
                        {
                            case 0:
                            case 1:
                            case 2:
                                tmp = TileType.Grass;
                                break;
                            case 3:
                            case 4:
                                tmp = TileType.Sand;
                                break;
                            case 5:
                                tmp = TileType.Water;
                                break;
                            default:
                                tmp = TileType.Grass;
                                break;
                        }
                    }
                    Tiles[x][y] = new Tile(tmp, new Vector2(x * 8, y * 8));
                }
            } 
            for (int x = 40; x < 80; x++)
            {
                for (int y = 40; y < 80; y++)
                {
                    if(Tiles[x][y].Type == TileType.Grass)
                    {
                        int a = rnd.Next(1, 30
                            );
                        if (a == 4)
                            BlockEntities[x][y] = new EntityTree(new Vector2(x * 8, y * 8));
                        else if (a == 2)
                            BlockEntities[x][y] = new EntityStone(new Vector2(x * 8, y * 8));
                        else if (a == 3)
                            BlockEntities[x][y] = new EntityFlowers(new Vector2(x * 8, y * 8));
                    }
                    else if (Tiles[x][y].Type == TileType.Sand)
                    {
                        int a = rnd.Next(1, 15);
                        if (a == 1)
                            BlockEntities[x][y] = new EntityCactus(new Vector2(x * 8, y * 8));
                    }
                }
            }
        }

        public void Update(GameTime gameTime, Camera2D camera)
        {
            for (int x = (int)((camera.Pos.X / 8) - (Math.Round((double)(1280 / 8))) / 8);
                x < (int)((camera.Pos.X / 8) + (Math.Round((double)(1280 / 8))) / 8); x++)
            {
                for (int y = (int)((camera.Pos.Y / 8) - (Math.Round((double)(720 / 8))) / 8);
                    y < (int)((camera.Pos.Y / 8) + (Math.Round((double)(720 / 8))) / 8); y++)
                {
                    
                    if(x >= 0 && x < 120 && y >= 0 && y < 120)
                    {
                        if (BlockEntities[x][y] != null && BlockEntities[x][y].Life <= 0)
                        {
                            BlockEntities[x][y] = null;
                        }
                        else if (BlockEntities[x][y] != null)
                        {
                            BlockEntities[x][y].Update(gameTime);
                        }
                    }
                }
            }
            foreach (Item item in Items)
            {
                item.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch, Camera2D camera)
        {
            for (int x = (int)((camera.Pos.X / 8) - (Math.Round((double)(1280 / 8))) / 8);
                x < (int)((camera.Pos.X / 8) + (Math.Round((double)(1280 / 8))) / 8); x++)
            {
                for (int y = (int)((camera.Pos.Y / 8) - (Math.Round((double)(720 / 8))) / 8);
                    y < (int)((camera.Pos.Y / 8) + (Math.Round((double)(720 / 8))) / 8); y++)
                {
                    if(x >= 0 && x < 120 && y >= 0 && y < 120)
                    {
                        Tiles[x][y].Draw(spriteBatch);
                    }
                }
            }
            foreach (Item item in World.Items)
            {
                item.Draw(spriteBatch);
            }
            for (int x = (int)((camera.Pos.X / 8) - (Math.Round((double)(1280 / 8))) / 8);
                x < (int)((camera.Pos.X / 8) + (Math.Round((double)(1280 / 8))) / 8); x++)
            {
                for (int y = (int)((camera.Pos.Y / 8) - (Math.Round((double)(720 / 8))) / 8);
                    y < (int)((camera.Pos.Y / 8) + (Math.Round((double)(720 / 8))) / 8); y++)
                {
                    if(x >= 0 && x < 120 && y >= 0 && y < 120)
                    {
                        if (x >= 0 && x <= 120 && y >= 0 && y <= 120 && BlockEntities[x][y] != null)
                        {
                            BlockEntities[x][y].Draw(spriteBatch);
                        }
                    }
                }
            }
        }

        public static Tile GetClosestTile(Vector2 pos)
        {
            pos /= 8;
            return Tiles[(int)Math.Round(pos.X)][(int)Math.Round(pos.Y)];
        }

        public static EntityBlock GetClosestBlock(Vector2 pos)
        {
            pos /= 8;
            return BlockEntities[(int)Math.Round(pos.X)][(int)Math.Round(pos.Y)];
        }
    }
}
