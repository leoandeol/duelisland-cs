#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
#endregion

namespace DuelIsland.Screens
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    public class OptionsMenuScreen : MenuScreen
    {
        #region Fields

        MenuEntry fullscreenMenuEntry;

        static string[] languages = { "English", "Francais" };

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("Options")
        {
            fullscreenMenuEntry = new MenuEntry(string.Empty);

            SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back");

            fullscreenMenuEntry.Selected += FullscreenMenuEntrySelected;
            back.Selected += OnCancel;

            MenuEntries.Add(fullscreenMenuEntry);
            MenuEntries.Add(back);
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            fullscreenMenuEntry.Text = "Fullscreen: " + (Game.Graphics.IsFullScreen ? "on" : "off");
        }


        #endregion

        #region Handle Input

        /// <summary>
        /// Event handler for when the Frobnicate menu entry is selected.
        /// </summary>
        void FullscreenMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Game.Graphics.IsFullScreen = !Game.Graphics.IsFullScreen;
            Game.Graphics.ApplyChanges();
            SetMenuEntryText();
        }

        #endregion
    }
}
