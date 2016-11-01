using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DuelIsland.GUI;
using DuelIsland.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DuelIsland.Screens
{
    public class CharacterCreatorScreen : MenuScreen
    {
        SpriteBatch spriteBatch;
        SpriteFont font;
        float pauseAlpha;
        Texture2D background;
        MenuEntry b_male, b_female, b_ok;
        string s_sex;
        Vector2 pos_sex;
        static Rectangle sourceRectangle = new Rectangle(0, 0, 8, 8);
        string assetNameSex1, assetNameSex2;
        int currentPlayer, nb_player;

        public CharacterCreatorScreen(int nb_player) : base("Character Choose")
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            currentPlayer = 1;
            this.nb_player = nb_player;
            b_male = new MenuEntry("Male");
            b_female = new MenuEntry("Female");
            b_ok = new MenuEntry("Ok");
            MenuEntry back = new MenuEntry("Back");
            this.drawTitle = false;
            pos_sex = new Vector2(20, 30);
            b_male.Selected += MaleMenuEntrySelected;
            b_male.Static = true;
            b_male.Position = new Vector2(20, 80);
            b_female.Selected += FemaleMenuEntrySelected;
            b_female.Static = true;
            b_female.Position = new Vector2(20, 110);
            b_ok.Selected += OkMenuEntrySelected;
            b_ok.Static = true;
            b_ok.Position = new Vector2(20, 200);
            back.Selected += OnCancel;
            back.Static = true;
            back.Position = new Vector2(20, 240);

            MenuEntries.Add(b_male);
            MenuEntries.Add(b_female);
            MenuEntries.Add(b_ok);
            MenuEntries.Add(back);
        }

        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            spriteBatch = ScreenManager.SpriteBatch;
            font = ResourceManager.GetFont(@"Fonts\silkscreen");
            background = ResourceManager.GetTexture(@"Textures\backgroundCharacterCreator");
            s_sex = "Sex :";
            assetNameSex1 = "male";
            assetNameSex2 = "female";
            ScreenManager.Game.ResetElapsedTime();
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
            #region Ecran recouvert
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);
            #endregion
            if (IsActive)
            {
                // App code
            }
        }

        #region Handle Input

        /// <summary>
        /// Event handler for when the Frobnicate menu entry is selected.
        /// </summary>
        void MaleMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (currentPlayer == 1)
                assetNameSex1 = "male";
            else
                assetNameSex2 = "male";
        }

        /// <summary>
        /// Event handler for when the Frobnicate menu entry is selected.
        /// </summary>
        void FemaleMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (currentPlayer == 1)
                assetNameSex1 = "female";
            else
                assetNameSex2 = "female";
        }

        /// <summary>
        /// Event handler for when the Frobnicate menu entry is selected.
        /// </summary>
        void OkMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (currentPlayer == 1)
                currentPlayer = 2;
            else
                LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new GameplayScreen(@"Textures\Spritesheets\char_wizard_" + assetNameSex1, 
                    @"Textures\Spritesheets\char_wizard_" + assetNameSex2));
        }

        protected new void OnCancel(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new BackgroundScreen(), PlayerIndex.One);
            ScreenManager.AddScreen(new MainMenuScreen(), PlayerIndex.One);
        }

        #endregion

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.None, RasterizerState.CullCounterClockwise);
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.DrawString(font, "Current player : " + currentPlayer.ToString(), new Vector2(900, 10), Color.Black);
            #region Bouton et texte correspondant
            spriteBatch.DrawString(font, s_sex, pos_sex, Color.White, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
            #endregion
            spriteBatch.Draw(ResourceManager.GetTexture(@"Textures\Spritesheets\char_wizard_" + ((currentPlayer == 1) ? assetNameSex1 : assetNameSex2)), 
                new Rectangle(752, 232, 256, 256), sourceRectangle, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
            #region Transition
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
            #endregion
        }
    }
}
