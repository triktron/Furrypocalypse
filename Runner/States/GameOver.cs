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
    internal class GameOver
    {
        /// <summary>
        /// Sets the current screen to game over
        /// </summary>
        public static void ShowGameOver()
        {
            Gameplay.ScorePanel.Visible = false;
            Game.self.State = Game.GameState.GameOver;
            UserInterface.Active.Root.Find<Panel>("gameover").Visible = true;
            SoundManager.Play("gameover");
        }

        /// <summary>
        /// Loads the GameOver screen
        /// </summary>
        /// <param name="content">The Content Manager of the Game</param>
        public void Load(ContentManager content)
        {
            UserInterface.Active.AddEntity(UIElements.GenerateGameOverScreen());
        }

        /// <summary>
        /// Updates the GameOver state
        /// </summary>
        /// <param name="gameTime">The Gametime object of that step</param>
        public void Update(GameTime gameTime)
        {
            Game.self.Window.AllowUserResizing = true;
        }

        /// <summary>
        /// Draws the GameOver screen on the surface provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}