using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Runner.Graphics;
using Runner.Runners;
using Runner.Utils;
using System.Collections.Generic;
using GeoSketch;
using System.Linq;
using System;
using System.Diagnostics;
using Runner.Obsticals;
using Primitives2D;
using Runner.Physics;

namespace Runner.States
{
    internal class Gameplay
    {
        private Background background = new Background();
        private PlatformGenerator platfromGen = new PlatformGenerator();

        private List<Platform> platforms = new List<Platform>();

        private Paragraph Score;
        static public float Points;
        static public Panel ScorePanel;

        static private ContentManager Content;

        private static List<BaseRunner> runners = new List<BaseRunner>();
        private static List<BaseRunner> Enemies = new List<BaseRunner>();
        //static List<BaseOpstical> Obsticals = new List<BaseOpstical>();

        private FPSMeter fpsmeter = new FPSMeter();

        private bool godMode = false;

        /// <summary>
        /// Loads the GamePlay screen
        /// </summary>
        /// <param name="content">The Content Manager of the Game</param>
        public void Load(ContentManager content)
        {
            AddUI();

            background.Load(content);

            Platform.Load(content);

            Content = content;
        }

        /// <summary>
        /// Adds a runner to the game
        /// </summary>
        static public void AddRunner()
        {
            int type = Game.random.Next(3);

            if (Game.self.Settings.DefaultTeam == 0 || (Game.self.Settings.DefaultTeam == 2 && Game.random.Next(2) == 0))
            {
                if (type == 0) runners.Add(new DogRunner());
                if (type == 1) runners.Add(new CatRunner());
                if (type == 2) runners.Add(new DinoRunner());
            }
            else
            {
                if (type == 0) runners.Add(new BoyRunner());
                if (type == 1) runners.Add(new GirlRunner());
                if (type == 2) runners.Add(new KnightRunner());
            }

            runners.Last().Load(Content);
        }

        static public void GenerateEnemy()
        {
            int type = Game.random.Next(3);

            if (Game.self.Settings.DefaultTeam == 1 || (Game.self.Settings.DefaultTeam == 2 && Game.random.Next(2) == 0))
            {
                if (type == 0) Enemies.Add(new DogRunner());
                if (type == 1) Enemies.Add(new CatRunner());
                if (type == 2) Enemies.Add(new DinoRunner());
            }
            else
            {
                if (type == 0) Enemies.Add(new BoyRunner());
                if (type == 1) Enemies.Add(new GirlRunner());
                if (type == 2) Enemies.Add(new KnightRunner());
            }

            Enemies.Last().FlipFacing = true;
            Enemies.Last().Strength = 6;
            Enemies.Last().Load(Content);
            Enemies.Last().Position = new Vector2(Game.Graphics.GraphicsDevice.Viewport.Width, 0);
        }

        /// <summary>
        /// Add the UI of Gameplay to the screen
        /// </summary>
        private void AddUI()
        {
            ScorePanel = new Panel(new Vector2(450, 60), PanelSkin.Fancy, Anchor.TopCenter);
            ScorePanel.Visible = false;
            if (Game.self.Settings.Debug) ScorePanel.Size = new Vector2(450, 100);
            UserInterface.Active.AddEntity(ScorePanel);

            Score = new Paragraph("Score: 0", Anchor.Center);
            ScorePanel.AddChild(Score);
        }

        /// <summary>
        /// Opens the GamePlay screen and resets the players
        /// </summary>
        public static void OpenGameUI()
        {
            UserInterface.Active.Root.Find<Panel>("menu").Visible = false;
            ScorePanel.Visible = true;
            Points = 0;
            Game.self.State = Game.GameState.Gameplay;

            runners.Clear();
            //Enemies.Clear();
            AddRunner();
            AddRunner();
            AddRunner();
            AddRunner();
            AddRunner();
            //Physics.SetPlayerPos(runners);
        }

        /// <summary>
        /// Updates the GamePlay state
        /// </summary>
        /// <param name="gameTime">The Gametime object of that step</param>
        public void Update(GameTime gameTime)
        {
            Game.self.Window.AllowUserResizing = false;

            InputDelay.Update();

            if (Cheats.CheckCheat(Cheats.Codes.ExtraLife)) AddRunner();

            float timeMulty = (float)gameTime.ElapsedGameTime.TotalMilliseconds * 60 / 1000;
            Points += timeMulty / 15;
            UpdatePlatforms(timeMulty);

            UpdatePlayer(gameTime, timeMulty);

            if (Cheats.CheckCheat(Cheats.Codes.DoomGod)) godMode = !godMode;

            UpdateEnemies(gameTime, timeMulty);

            /*if (Game.random.Next(50) == 0)
            {
                BaseOpstical obstical = new ObsticalSpike();
                obstical.Load(Content);
                //obstical.Scale = 0.2f;
                Rectangle rect = obstical.Destination;
                rect.X = Game.Graphics.GraphicsDevice.Viewport.Width;
                //rect = Physics.PlaceOnGround(rect, platforms);

                if (rect.Y != 0)
                {
                    obstical.Pos = rect.Location.ToVector2();
                    Obsticals.Add(obstical);
                }
            }

            for (int i = Obsticals.Count - 1; i >= 0; i--)
            {
                Obsticals[i].Pos -= new Vector2(5, 0);
                if (Obsticals[i].Destination.X + Obsticals[i].Destination.Width < 0) Obsticals.RemoveAt(i);
            }*/

            fpsmeter.Update(gameTime);
        }

        private void UpdatePlatforms(float timeMulty)
        {
            for (int i = platforms.Count - 1; i >= 0; i--)
            {
                PlatformUpdater.UpdatePlatform(platforms[i], timeMulty);

                if (!platforms[i].IsActive) platforms.RemoveAt(i);
            }

            while (platforms.Count == 0 || platforms.Last().offset < Game.Graphics.GraphicsDevice.Viewport.Width)
            {
                platfromGen.Next(platforms);
            }
        }

        private void UpdatePlayer(GameTime gameTime, float timeMulty)
        {
            BaseRunner fartest = runners[0];
            foreach (BaseRunner runner in runners)
            {
                if (fartest.Position.X < runner.Position.X) fartest = runner;
            }

            for (int i = runners.Count - 1; i >= 0; i--)
            {
                BaseRunner runner = runners[i];
                //Physics.UpdateRunner(runner, timeMulty, i, runners.Count, (int)runners[0].Position.X);

                runner.Update(gameTime);
                Gravety.Update(ref runner, platforms, gameTime);

                if (InputDelay.IsPressed((int)(fartest.Position.X - runner.Position.X) / 10) && fartest.JumpsLeft > 0)
                {
                    Gravety.Jump(ref runner, gameTime);
                    SoundManager.Play("jump");
                }

                //int damage = Enemies.FindAll(x => Colision.GetColisonSide(runner.Hitbox, x.Hitbox) != Colision.Side.None).Count * runner.Strength;

                //if (damage > 0) runner.Position -= new Vector2(5,0);

                //runner.Health -= damage;

                if (!runner.IsActive)
                {
                    runners.RemoveAt(i);
                    SoundManager.Play("die");

                    if (godMode) AddRunner();
                }
            }

            if (runners.Count == 0) GameOver.ShowGameOver();

            //runners.Sort((x, y) => x.Destination.X.CompareTo(y.Destination.X));
        }

        private void UpdateEnemies(GameTime gameTime, float timeMulty)
        {
            if (Game.random.Next(200) == 0) GenerateEnemy();

            Debug.WriteLine(string.Join(" ", Enemies));

            for (int i = Enemies.Count - 1; i >= 0; i--)
            {
                BaseRunner runner = Enemies[i];
                //Physics.UpdateRunner(runner, timeMulty, i, runners.Count, (int)runners[0].Position.X);

                runner.DesiredPos = -100;

                runner.Update(gameTime);
                Gravety.Update(ref runner, platforms, gameTime);

                if (!runner.IsActive)
                {
                    Enemies.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Draws the GamePlay screen on the surface provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            DrawPlatforms(spriteBatch);

            Positioner.SetDesiredPlayerPos(ref runners);

            /*foreach (BaseOpstical obstical in Obsticals)
            {
                obstical.Draw(spriteBatch);
            }*/

            DrawCharachter(spriteBatch, runners);
            DrawCharachter(spriteBatch, Enemies);

            Score.Text = "Score:" + (int)Points + " and Lives Left:" + runners.Count;
            if (Game.self.Settings.Debug) Score.Text += "\n" + fpsmeter.GetFps();
        }

        private void DrawPlatforms(SpriteBatch spriteBatch)
        {
            foreach (Platform platform in platforms)
            {
                platform.Draw(spriteBatch);
                if (Game.self.Settings.Debug)
                    if (Game.self.Settings.UseSlowGeoSketch)
                        spriteBatch.DrawRectangle(platform.Destintation().X, platform.Destintation().Y, platform.Destintation().Width, platform.Destintation().Height, Color.Transparent, Color.Black, 4);
                    else
                        spriteBatch.DrawRectangle(platform.Destintation(), Color.Black, 4);
            }
        }

        private static void DrawCharachter(SpriteBatch spriteBatch, List<BaseRunner> charachters)
        {
            foreach (BaseRunner runner in charachters)
            {
                runner.Draw(spriteBatch);
            }
        }
    }
}