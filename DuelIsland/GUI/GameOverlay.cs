using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuelIsland.Entity;
using DuelIsland.Entity.Player;
using DuelIsland.Items;
using DuelIsland.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DuelIsland.GUI
{
    public class GameOverlay
    {
        static Texture2D bars = ResourceManager.GetTexture(@"Textures\Tilesets\bars");
        static Texture2D background = ResourceManager.GetTexture(@"Textures\gameOverlay");
        static SpriteFont font = ResourceManager.GetFont(@"Fonts\silkscreen");
        Rectangle lifeRect = new Rectangle(0, 46, 201, 23),
            staminaRect = new Rectangle(0, 23, 201, 23),
            xpRect = new Rectangle(0, 0, 201, 23),
            emptyRect = new Rectangle(0, 69, 201, 23);
        EntityWizard player1, player2;
        int tmp_calc;
        // TODO: Use the static access of the players from the GameplayScreen class

        public GameOverlay()
        {
            this.player1 = new EntityWizard(Vector2.Zero, "Player 1", @"Textures\Spritesheets\char_warrior_male", 1);
            this.player2 = new EntityWizard(Vector2.Zero, "Player 2", @"Textures\Spritesheets\char_warrior_female", 2);
        }

        public void Update(EntityWizard player1, EntityWizard player2)
        {
            this.player1 = player1;
            this.player2 = player2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            #region Background
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            #endregion
            #region Player 1
            spriteBatch.Draw(bars, new Vector2(20, 630), emptyRect, Color.White);
            spriteBatch.Draw(bars, new Vector2(20, 660), emptyRect, Color.White);
            spriteBatch.Draw(bars, new Vector2(20, 690), emptyRect, Color.White);
            tmp_calc = (int)(player1.Life * 201) / player1.MaxLife;
            spriteBatch.Draw(bars, new Vector2(20, 630), new Rectangle(lifeRect.X, lifeRect.Y, tmp_calc, lifeRect.Height), Color.White);
            tmp_calc = (int)(player1.Stamina * 201) / player1.MaxStamina;
            spriteBatch.Draw(bars, new Vector2(20, 660), new Rectangle(staminaRect.X, staminaRect.Y, tmp_calc, staminaRect.Height), Color.White);
            tmp_calc = (int)(player1.XP * 201) / player1.MaxXP;
            spriteBatch.Draw(bars, new Vector2(20, 690), new Rectangle(xpRect.X, xpRect.Y, tmp_calc, xpRect.Height), Color.White);
            spriteBatch.DrawString(font, player1.Name, new Vector2(580 - (font.MeasureString(player1.Name).X*1.5f), 630), Color.Black, 0f, 
                Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, "Level " + player1.Level.ToString(), 
                new Vector2(580 - font.MeasureString("Level " + player1.Level.ToString()).X, 670), Color.Black);
            #endregion
            #region Player 2
            spriteBatch.Draw(bars, new Vector2(1059, 630), emptyRect, Color.White);
            spriteBatch.Draw(bars, new Vector2(1059, 660), emptyRect, Color.White);
            spriteBatch.Draw(bars, new Vector2(1059, 690), emptyRect, Color.White);
            tmp_calc = (int)(player2.Life * 201) / player2.MaxLife;
            spriteBatch.Draw(bars, new Vector2(1059, 630), new Rectangle(lifeRect.X, lifeRect.Y, tmp_calc, lifeRect.Height), Color.White);
            tmp_calc = (int)(player2.Stamina * 201) / player2.MaxStamina;
            spriteBatch.Draw(bars, new Vector2(1059, 660), new Rectangle(staminaRect.X, staminaRect.Y, tmp_calc, staminaRect.Height), Color.White);
            tmp_calc = (int)(player2.XP * 201) / player2.MaxXP;
            spriteBatch.Draw(bars, new Vector2(1059, 690), new Rectangle(xpRect.X, xpRect.Y, tmp_calc, xpRect.Height), Color.White);
            spriteBatch.DrawString(font, player2.Name, new Vector2(700, 630), Color.Black, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, "Level " + player2.Level.ToString(), new Vector2(700, 670), Color.Black);
            #endregion
        }
    }
}
