using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DuelIsland.Component
{
    public class FPSCountComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        SpriteFont font;
        Vector2 position;
        float fps;
        Color color;
        bool show;
        KeyboardState keyboardState, oldKeyboardState;

        public bool ShowFPSCount
        {
            get { return show; }
            set { show = value; }
        }

        public FPSCountComponent(Game game, SpriteFont font, Vector2 position, Color color)
            : base(game)
        {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            this.position = position;
            this.font = font;
            this.color = color;
            show = false;
            fps = 0;
        }

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            // TODO: Calcul de la poisition du texte && fix touche
            /*if (keyboardState.IsKeyDown(Keys.F1) && !oldKeyboardState.IsKeyDown(Keys.F1))
            {
                show = !show;
            }*/
            fps = (float)(1 / (float)(gameTime.ElapsedGameTime.Milliseconds / 1000f));
            oldKeyboardState = keyboardState;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (show)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, fps.ToString(), position, color);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
