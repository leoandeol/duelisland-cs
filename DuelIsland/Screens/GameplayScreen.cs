using System;
using System.Threading;
using DuelIsland.Entity;
using DuelIsland.Entity.Player;
using DuelIsland.Entity.World;
using DuelIsland.GUI;
using DuelIsland.Manager;
using DuelIsland.Tool;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DuelIsland.Screens
{
    public class GameplayScreen : AbstractGameScreen
    {
        SpriteBatch spriteBatch;
        float pauseAlpha;
        World world;
        static EntityWizard player1, player2;
        static Camera2D camera1, camera2;
        KeyboardState keyboardState, oldKeyboardState;
        GameOverlay overlay;
        string tmp, tmp2;
        Viewport viewA, viewB, defaultView;

        public GameplayScreen(string player1AssetName, string player2AssetName)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(1.5);
            tmp = player1AssetName;
            tmp2 = player2AssetName;
        }

        public static EntityWizard Player1
        {
            get { return player1; }
        }

        public static EntityWizard Player2
        {
            get { return player2; }
        }

        public static Camera2D Camera1
        {
            get { return camera1; }
        }

        public static Camera2D Camera2
        {
            get { return camera2; }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public override void LoadContent()
        {
            camera1 = new Camera2D();
            camera2 = new Camera2D();
            world = new World();
            Vector2 newpos = Vector2.Zero;
            Random rnd = new Random();
            do
            {
                newpos = new Vector2(rnd.Next(0, 120) * 8, rnd.Next(0, 120) * 8);
            }
            while (World.GetClosestTile(newpos).Type == TileType.Water ||
                World.GetClosestBlock(newpos) != null);
            player1 = new EntityWizard(newpos, "Player 1", tmp, 1);
            do
            {
                newpos = new Vector2(rnd.Next(0, 120) * 8, rnd.Next(0, 120) * 8);
            }
            while (World.GetClosestTile(newpos).Type == TileType.Water ||
                World.GetClosestBlock(newpos) != null);
            player2 = new EntityWizard(newpos, "Player 2", tmp2, 2);
            overlay = new GameOverlay();
            camera1.Zoom = 5f; new Rectangle((int)newpos.X, (int)newpos.Y, 8, 8);
            camera1.Pos = new Vector2(player1.Hitbox.X, player1.Hitbox.Y) + new Vector2(8, 8);
            camera2.Zoom = 5f;
            camera2.Pos = new Vector2(player2.Hitbox.X, player2.Hitbox.Y) + new Vector2(8, 8);
            viewA = new Viewport(0, 0, 630, 720);
            viewB = new Viewport(650, 0, 630, 720);
            defaultView = new Viewport(0, 0, 1280, 720);
            ScreenManager.Game.ResetElapsedTime();

            spriteBatch = new SpriteBatch(ScreenManager.Game.GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public override void UnloadContent() { }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                oldKeyboardState = keyboardState;
                keyboardState = Keyboard.GetState();
                camera1.Pos = new Vector2(player1.Hitbox.X, player1.Hitbox.Y) + new Vector2(8, 8);
                camera2.Pos = new Vector2(player2.Hitbox.X, player2.Hitbox.Y) + new Vector2(8, 8);
                player1.Update(gameTime, keyboardState, oldKeyboardState);
                player2.Update(gameTime, keyboardState, oldKeyboardState);
                if (player1.Life <= 0)
                {
                    player1.Explode();
                }
                else if (player2.Life <= 0)
                {
                    player2.Explode();
                }
                if (keyboardState.IsKeyDown(Keys.Subtract) && !oldKeyboardState.IsKeyDown(Keys.Subtract))
                {
                    camera1.Zoom--;
                    camera2.Zoom--;
                }
                else if (keyboardState.IsKeyDown(Keys.Add) && !oldKeyboardState.IsKeyDown(Keys.Add))
                {
                    camera1.Zoom++;
                    camera2.Zoom++;
                }
                /*if (player1.Life <= 0)
                {
                    ScreenManager.AddScreen(new BackgroundScreen(), null);
                    ScreenManager.AddScreen(new MainMenuScreen(), null);
                }*/
                overlay.Update(player1, player2);
                world.Update(gameTime, camera1);
                world.Update(gameTime, camera2);
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputManager input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {

            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Black);

            ScreenManager.GraphicsDevice.Viewport = viewA;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera1.get_transformation(ScreenManager.GraphicsDevice));
            world.Draw(spriteBatch, camera1);
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            spriteBatch.End();

            ScreenManager.GraphicsDevice.Viewport = viewB;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera2.get_transformation(ScreenManager.GraphicsDevice));
            world.Draw(spriteBatch, camera2);
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            spriteBatch.End();

            ScreenManager.GraphicsDevice.Viewport = defaultView;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.None, RasterizerState.CullCounterClockwise);
            overlay.Draw(spriteBatch);
            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }
    }
}