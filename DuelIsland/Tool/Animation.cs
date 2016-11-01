using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuelIsland.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DuelIsland.Tool
{
    public class Animation
    {
        int heightBegin, nb_frames, frameLenght, frameColumn, elapsedTime;
        Texture2D texture;
        Rectangle sourceRectangle;

        public Animation(int heightBegin, int tileWidth, int tileHeight, int nb_frames, int frameLenght, string textureAssetName)
        {
            this.heightBegin = heightBegin;
            this.nb_frames = nb_frames;
            this.frameLenght = frameLenght;
            this.texture = ResourceManager.GetTexture(textureAssetName);
            this.sourceRectangle = new Rectangle(0, heightBegin, tileWidth, tileHeight);
            this.frameColumn = 0;
            this.elapsedTime = 0;
        }

        public void Update(GameTime gameTime)
        {

            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedTime >= frameLenght)
            {
                elapsedTime = 0;
                frameColumn++;
            }
            if (frameColumn >= nb_frames)
                frameColumn = 0;
            sourceRectangle = new Rectangle(frameColumn * sourceRectangle.Width, heightBegin, sourceRectangle.Width, sourceRectangle.Height);

        }

        public void Draw(SpriteBatch spritebatch, Vector2 position)
        {
            spritebatch.Draw(texture, position, sourceRectangle, Color.White);
        }

        public void Draw(SpriteBatch spritebatch, Rectangle hitbox)
        {
            spritebatch.Draw(texture, hitbox, sourceRectangle, Color.White);
        }
    }
}
