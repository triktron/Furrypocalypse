using GeonBit.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Runner.States;
using Runner.Utils;
using System;

namespace Runner
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager Graphics;
        private SpriteBatch spriteBatch;

        private SpashScreen _spashScreen = new SpashScreen();
        private MainMenu _mainMenu = new MainMenu();
        private Gameplay _gameplay = new Gameplay();
        private GameOver _gameOver = new GameOver();
        private Credits _credits = new Credits();

        public GameSettings Settings;

        public static Game self;

        public static Random random;

        public enum GameState
        {
            SplashScreen,
            MainMenu,
            Gameplay,
            GameOver,
            Credits,
        }

        public GameState State;

        public Game()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        private void EnterFullscreen()
        {
            Graphics.IsFullScreen = true;
            Graphics.ApplyChanges();
        }

        private void LeaveFullscreen()
        {
            Graphics.IsFullScreen = false;
            Graphics.ApplyChanges();
        }

        public void Fullscreen(bool fullscreen)
        {
            if (Graphics.IsFullScreen == fullscreen) return;
            if (fullscreen)
            {
                EnterFullscreen();
            }
            else
            {
                LeaveFullscreen();
            }
        }

        public void SetScreenAspect()
        {
            Graphics.PreferredBackBufferHeight = (int)((float)Graphics.PreferredBackBufferWidth * 9 / 16);
        }

        protected override void Initialize()
        {
            self = this;

            random = new Random(DateTime.Now.Millisecond);

            Settings = GameSettings.Load();

            UserInterface.Initialize(Content, BuiltinThemes.hd);
            UserInterface.Active.UseRenderTarget = true;
            UserInterface.Active.GlobalScale = Settings.MenuScale;

            Graphics.PreferredBackBufferWidth = Settings.ScreenWidth;
            Fullscreen(Settings.Fullscreen);

            Window.ClientSizeChanged += (Object sender, EventArgs ee) =>
            {
                SetScreenAspect();
                Graphics.ApplyChanges();
            };

            IsFixedTimeStep = true;
            Graphics.SynchronizeWithVerticalRetrace = false;
            SetScreenAspect();
            Graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _spashScreen.Load(Content);
            _mainMenu.Load(Content);
            _gameplay.Load(Content);
            _gameOver.Load(Content);
            _credits.Load(Content);

            SoundManager.Load(Content);

            //Gameplay.OpenGameUI();
        }

        protected override void Update(GameTime gameTime)
        {
            UserInterface.Active.Update(gameTime);

            Cheats.Update(gameTime);

            if (State == GameState.SplashScreen) _spashScreen.Update(gameTime);
            if (State == GameState.MainMenu) _mainMenu.Update(gameTime);
            if (State == GameState.Gameplay) _gameplay.Update(gameTime);
            if (State == GameState.GameOver) _gameOver.Update(gameTime);
            if (State == GameState.Credits) _credits.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            UserInterface.Active.Draw(spriteBatch);
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            if (State == GameState.SplashScreen) _spashScreen.Draw(spriteBatch);
            if (State == GameState.MainMenu) _mainMenu.Draw(spriteBatch);
            if (State == GameState.Gameplay) _gameplay.Draw(spriteBatch);
            if (State == GameState.Credits) _credits.Draw(spriteBatch);
            if (State == GameState.GameOver)
            {
                _gameplay.Draw(spriteBatch);
                _gameOver.Draw(spriteBatch);
            }

            spriteBatch.End();

            UserInterface.Active.DrawMainRenderTarget(spriteBatch);

            base.Draw(gameTime);
        }
    }
}