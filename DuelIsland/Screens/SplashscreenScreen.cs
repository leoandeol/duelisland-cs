using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DuelIsland.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DuelIsland.Screens
{
    public class SplashscreenScreen : AbstractGameScreen
    {
        SpriteBatch spriteBatch;
        float pauseAlpha;
        Texture2D background;
        Stopwatch timer;

        public SplashscreenScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1);
            TransitionOffTime = TimeSpan.FromSeconds(1);
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            spriteBatch = ScreenManager.SpriteBatch;
            background = ResourceManager.GetTexture(@"Textures\background");
            timer = new Stopwatch();
            timer.Start();
            //this.ScreenManager.Game.ResetElapsedTime();
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
                if (timer.ElapsedMilliseconds > 5000)
                {
                    ScreenManager.AddScreen(new BackgroundScreen(), null);
                    ScreenManager.AddScreen(new MainMenuScreen(), null);
                }
            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.None, RasterizerState.CullCounterClockwise);
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.End();
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
