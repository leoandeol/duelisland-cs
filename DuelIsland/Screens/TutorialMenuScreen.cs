using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuelIsland.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DuelIsland.Screens
{
    public class TutorialMenuScreen : MenuScreen
    {
        #region Fields

        SpriteFont font;
        static string text = "First, each player choose his\ncharacter's sex, then play !\n Player 1 Commands : Z, Q, S, D and Space\nPlayer 2 Commands : Arrows and Enter";
        static Vector2 text_pos = new Vector2(1280, 150);

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public TutorialMenuScreen() : base("Credits")
        {
            MenuEntry back = new MenuEntry("Back");
            back.Selected += OnCancel;
            back.Static = true;
            back.Position = new Vector2(620, 650);
            MenuEntries.Add(back);
            font = ResourceManager.GetFont(@"Fonts\silkscreen");
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GraphicsDevice graphics = ScreenManager.GraphicsDevice;
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            text_pos.X = (1280 / 2) - (font.MeasureString(text).X / 2);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.None, RasterizerState.CullCounterClockwise);

            spriteBatch.DrawString(font, text, text_pos, Color.White);

            spriteBatch.End();
        }

        #endregion
    }
}
