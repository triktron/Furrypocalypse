using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Runner.Graphics;
using Runner.Runners;
using System;
using System.Collections.Generic;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Runner.Utils;
using System.Diagnostics;

namespace Runner.States
{
    internal class MainMenu
    {
        public static Background Background = new Background();

        private int _timePerRunner = 2000;
        private int _timeLeftToNextRunner = 2000;
        private int _currentRunner = 4;

        private List<BaseRunner> Runners = new List<BaseRunner>();

        /// <summary>
        /// Sets the current screen to MainMenu
        /// </summary>
        public static void OpenMenu()
        {
            UserInterface.Active.Root.Find<Panel>("menu").Visible = true;
            Gameplay.ScorePanel.Visible = false;
            Game.self.State = Game.GameState.MainMenu;
        }

        public static void OpenHighscore()
        {
            UserInterface.Active.Root.Find<Panel>("highscore").Visible = true;
            Gameplay.ScorePanel.Visible = false;
            Game.self.State = Game.GameState.MainMenu;
        }

        /// <summary>
        /// Loads the MainMenu screen
        /// </summary>
        /// <param name="content">The Content Manager of the Game</param>
        public void Load(ContentManager content)
        {
            Background.ID = new Random(DateTime.Now.Millisecond).Next(4);
            Background.Load(content);

            Runners.Add(new BoyRunner());
            Runners.Add(new CatRunner());
            Runners.Add(new DinoRunner());
            Runners.Add(new DogRunner());
            Runners.Add(new GirlRunner());
            Runners.Add(new KnightRunner());

            UserInterface.Active.AddEntity(UIElements.GenerateMainMenu());
            UserInterface.Active.AddEntity(UIElements.GenerateHighscore());
            UserInterface.Active.AddEntity(UIElements.GenerateSettingsMenu());
            Background.speed = Game.self.Settings.BGSpeed;

            foreach (BaseRunner runner in Runners) runner.Load(content);
        }

        /*
        public void SetScores()
        {
            Scores.ClearChildren();

            GlobalstatsIO_RankResponseData topplayers = Game.self.Settings.GameStatsToken.GetTopPlayers();
            foreach (GlobalstatsIO_StatisticValues score in topplayers.data)
            {
                Scores.AddChild(new Paragraph(score.rank + " - " + score.name + " with a score of " + score.value));
            }

            if (Game.self.Settings.GameStatsPlayer._id == null) return;

            GlobalstatsIO_RankResponse rank = Game.self.Settings.GameStatsToken.GetRank(Game.self.Settings.GameStatsPlayer._id);
            foreach (GlobalstatsIO_StatisticValues score in rank.better_ranks.data)
            {
                if (topplayers.data.Count >= score.rank) return;
                Scores.AddChild(new Paragraph(score.rank + " - " + score.name + " with a score of " + score.value));
            }

            if (topplayers.data.Count >= rank.user_rank.rank) return;
            Scores.AddChild(new Paragraph(rank.user_rank.rank + " - " + rank.user_rank.name + " with a score of " + rank.user_rank.value));

            foreach (GlobalstatsIO_StatisticValues score in rank.worse_ranks.data)
            {
                if (topplayers.data.Count >= score.rank) return;
                Scores.AddChild(new Paragraph(score.rank + " - " + score.name + " with a score of " + score.value));
            }
        }*/

        public void OpenNamePicker(string info = "Choose wisely as this name will be saved online!")
        {
            Panel NamePicker = new Panel(new Vector2(600, 300));
            UserInterface.Active.AddEntity(NamePicker);

            NamePicker.AddChild(new Paragraph("Please enter your desired player name."));
            NamePicker.AddChild(new Paragraph(info));

            TextInput text = new TextInput(false);
            text.Value = Game.self.Settings.PlayerName;
            NamePicker.AddChild(text);

            Button saveButton = new Button("Save", skin: ButtonSkin.Fancy);
            NamePicker.AddChild(saveButton);

            saveButton.OnClick = (Entity btn) =>
            {
                if (text.Value == "wisely")
                {
                    OpenPopup("Ha-ha. very funny.");
                    return;
                }
                else if (text.Value == "saved online")
                {
                    OpenNamePicker("As it shouldn't be changed later");
                    NamePicker.RemoveFromParent();
                    OpenPopup("Stop it.");
                    return;
                }
                else if (text.Value == "changed later")
                {
                    OpenNamePicker("Please enter another name.");
                    NamePicker.RemoveFromParent();

                    OpenPopup("Please enter another name.");
                    return;
                }
                else if (text.Value == "another name")
                {
                    OpenPopup("I'm getting sick of you.");
                    text.Value = "biggest prick in the universe";
                }

                Game.self.Settings.PlayerName = Censor.Text(text.Value);
                Game.self.Settings.Save();

                NamePicker.RemoveFromParent();
            };
        }

        public void OpenPopup(string info)
        {
            Panel popup = new Panel(new Vector2(400, 200));
            UserInterface.Active.AddEntity(popup);

            popup.AddChild(new Paragraph(info));

            Button button = new Button("Ok", skin: ButtonSkin.Fancy);
            popup.AddChild(button);

            button.OnClick = (Entity btn) =>
            {
                popup.RemoveFromParent();
            };
        }

        public void Update(GameTime gameTime)
        {
            Game.self.Window.AllowUserResizing = true;

            CheckNextRunner(gameTime);

            Background.Update(gameTime);
            //UpdateScoreSize();
            UpdateRunnerPos(gameTime);

            if (Cheats.CheckCheat(Cheats.Codes.Konami)) SoundManager.Play("gameover");
        }

        private void UpdateScoreSize()
        {
            /*Scores.Size = new Vector2(HighscorePanel.CalcDestRect().Width,
                HighscorePanel.CalcDestRect().Height -
                HighscorePanel.Children[2].CalcDestRect().Y -
                HighscorePanel.Children[2].CalcDestRect().Height
                );*/
        }

        private void UpdateRunnerPos(GameTime gameTime)
        {
            int windowHeight = Game.Graphics.GraphicsDevice.Viewport.Height;
            int windowWidth = Game.Graphics.GraphicsDevice.Viewport.Width;
            int offset = 40;

            Runners[_currentRunner].SpriteSheet.Loop = true;
            Runners[_currentRunner].AutoAnimationState = false;
            Runners[_currentRunner].SetAnimation(BaseRunner.State.Running);
            Runners[_currentRunner].Scale = 3;
            Runners[_currentRunner].Update(gameTime);
            Runners[_currentRunner].Position = new Vector2(windowWidth / 1.8f, windowHeight - Runners[_currentRunner].Hitbox.Height - offset);
        }

        private void CheckNextRunner(GameTime gameTime)
        {
            _timeLeftToNextRunner -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_timeLeftToNextRunner <= 0)
            {
                _currentRunner++;

                if (_currentRunner >= Runners.Count) _currentRunner = 0;
                Runners[_currentRunner].resetLoop();
                _timeLeftToNextRunner = _timePerRunner;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Background.Draw(spriteBatch);
            Runners[_currentRunner].ShowHealth = false;
            Runners[_currentRunner].Draw(spriteBatch);
        }
    }
}