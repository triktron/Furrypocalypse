using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Runner.Graphics;
using Runner.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.States
{
    internal class Credits
    {
        private static Background Background = new Background();
        private static int offset;

        private static Panel creditsPanel;

        /// <summary>
        /// Sets the current screen to game over
        /// </summary>
        public static void ShowCredits()
        {
            Game.self.State = Game.GameState.Credits;
            UserInterface.Active.Root.Find<Panel>("menu").Visible = false;
            Background.speed = Game.self.Settings.BGSpeed;
            offset = Game.Graphics.GraphicsDevice.Viewport.Height;
            creditsPanel.Visible = true;
        }

        /// <summary>
        /// Loads the GameOver screen
        /// </summary>
        /// <param name="content">The Content Manager of the Game</param>
        public void Load(ContentManager content)
        {
            Background.ID = new Random(DateTime.Now.Millisecond).Next(4);
            Background.Load(content);

            creditsPanel = new Panel(new Vector2(0.7f, 1f), PanelSkin.None, Anchor.TopCenter);
            creditsPanel.AdjustHeightAutomatically = true;
            creditsPanel.Visible = false;
            UserInterface.Active.AddEntity(creditsPanel);

            AddCredit("Game Code written by Triktron (Luca Vanesche)", "https://triktron.com");

            AddSpace();

            AddCredit("Big thanks to my class mates for the inspiration and feedback");
            AddCredit("And the Teachers for supporting me");

            AddSpace();

            AddCredit("UI code written by RonenNess", "https://github.com/RonenNess");
            AddCredit("He made the awesome Library called GeonBit.UI", "https://github.com/RonenNess/GeonBit.UI");
            AddCredit("The sprites used to make the UI are from Michele Bucelli (\"Buch\")", "http://opengameart.org/content/golden-ui");

            AddSpace();

            AddCredit("Debugging of the application has been made possible by John McDonald and Gary Texmo");
            AddCredit("A fork of the code can be found on Ellisonch Github page", "https://github.com/ellisonch/2d-xna-primitives");
            AddSpace(2);
            AddCredit("And Jef Daels his Geosketch Lib", "https://www.nuget.org/packages/GeoSketch/");

            AddSpace();

            AddCredit("The Sprites are sourced from gameart2d.com", "https://www.gameart2d.com/freebies.html");

            AddSpace();

            AddCredit("More To Add Later...");
        }

        private void AddCredit(string text, string url = null)
        {
            Paragraph paragraph = new Paragraph(text, Anchor.AutoInline);
            paragraph.AlignToCenter = true;
            paragraph.Scale = 1.4f;
            paragraph.WrapWords = false;
            if (url != null) paragraph.OnClick = X => System.Diagnostics.Process.Start(url);

            creditsPanel.AddChild(paragraph);
        }

        private void AddSpace(int space = 6)
        {
            creditsPanel.AddChild(new LineSpace(space));
        }

        /// <summary>
        /// Updates the GameOver state
        /// </summary>
        /// <param name="gameTime">The Gametime object of that step</param>
        public void Update(GameTime gameTime)
        {
            Game.self.Window.AllowUserResizing = true;
            Background.Update(gameTime);
            offset--;
            creditsPanel.Offset = new Vector2(0, offset);

            if (creditsPanel.GetActualDestRect().Bottom < 0 || Cheats.CheckCheat(Cheats.Codes.Konami))
            {
                creditsPanel.Visible = false;
                MainMenu.OpenMenu();
            }
        }

        /// <summary>
        /// Draws the GameOver screen on the surface provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Background.Draw(spriteBatch);
        }
    }
}