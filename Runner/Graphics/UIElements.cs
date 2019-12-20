using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Runner.States;
using Runner.Utils;

namespace Runner.Graphics
{
    internal class UIElements
    {
        public static Panel GenerateMainMenu()
        {
            // The Main Panel
            Panel menuPanel = new Panel(GameData.MainMenuSize, PanelSkin.Default, Anchor.TopLeft, GameData.UIOffset);
            menuPanel.Identifier = "menu";
            menuPanel.Visible = false;
            menuPanel.MinSize = GameData.MainMenuMinSize;
            menuPanel.AdjustHeightAutomatically = true;
            menuPanel.Draggable = true;

            //The Title
            menuPanel.AddChild(new Paragraph(GameData.GameName, anchor: Anchor.TopCenter, color: Color.Red, scale: GameData.TitleScale));
            menuPanel.AddChild(new HorizontalLine());

            // the buttons
            Vector2 MaxSize = new Vector2(0, 50);
            Button singleplayerButton = new Button("Singleplayer", size: GameData.ButtonSize, skin: ButtonSkin.Fancy);
            Button HighscoreButton = new Button("HighScores", size: GameData.ButtonSize, skin: ButtonSkin.Fancy);
            Button settingsButton = new Button("Settings", size: GameData.ButtonSize, skin: ButtonSkin.Fancy);
            Button creditsButton = new Button("Credits", size: GameData.ButtonSize, skin: ButtonSkin.Fancy);
            Button exitButton = new Button("Exit", size: GameData.ButtonSize, skin: ButtonSkin.Fancy);

            // set the button max size
            singleplayerButton.Identifier = "Singleplayer";
            HighscoreButton.Identifier = "HighScores";
            settingsButton.Identifier = "Settings";
            creditsButton.Identifier = "credits";
            exitButton.Identifier = "Exit";

            // set the button identifiers
            singleplayerButton.MaxSize = GameData.ButtonMaxSize;
            HighscoreButton.MaxSize = GameData.ButtonMaxSize;
            settingsButton.MaxSize = GameData.ButtonMaxSize;
            creditsButton.MaxSize = GameData.ButtonMaxSize;
            exitButton.MaxSize = GameData.ButtonMaxSize;

            // add the buttons to the menu
            menuPanel.AddChild(singleplayerButton);
            menuPanel.AddChild(HighscoreButton);
            menuPanel.AddChild(settingsButton);
            menuPanel.AddChild(creditsButton);
            menuPanel.AddChild(exitButton);

            // add the callbacks
            AddMenuCallbacks(menuPanel);

            return menuPanel;
        }

        private static void AddMenuCallbacks(Panel Menu)
        {
            Menu.Find<Button>("Singleplayer").OnClick = (Entity btn) =>
            {
                Gameplay.OpenGameUI();
            };

            Menu.Find<Button>("HighScores").OnClick = (Entity btn) =>
            {
                Menu.Visible = false;
                UserInterface.Active.Root.Find<Panel>("highscore").Visible = true;
            };

            Menu.Find<Button>("Settings").OnClick = (Entity btn) =>
            {
                Menu.Visible = false;
                UserInterface.Active.Root.Find<Panel>("Settings").Visible = true;
            };
            Menu.Find<Button>("credits").OnClick = (Entity btn) =>
            {
                Credits.ShowCredits();
            };
            Menu.Find<Button>("Exit").OnClick = (Entity btn) =>
            {
                Game.self.Exit();
            };
        }

        public static Panel GenerateHighscore()
        {
            // the Score panel
            Panel ScorePanel = new Panel(GameData.HighscoreSize, PanelSkin.Default, Anchor.TopLeft, GameData.UIOffset);
            ScorePanel.Visible = false;
            ScorePanel.Identifier = "highscore";
            ScorePanel.Draggable = true;

            // add the title
            ScorePanel.AddChild(new Paragraph("Highscores", anchor: Anchor.TopCenter, color: Color.Red, scale: GameData.TitleScale));
            ScorePanel.AddChild(new HorizontalLine());

            // add the back button
            Button backButton = new Button("Back", skin: ButtonSkin.Fancy);
            backButton.Identifier = "exit";
            ScorePanel.AddChild(backButton);

            // add the callbacks
            AddHighscoreCallbacks(ScorePanel);

            return ScorePanel;
        }

        private static void AddHighscoreCallbacks(Panel Score)
        {
            Score.Find<Button>("exit").OnClick = (Entity btn) =>
            {
                Score.Visible = false;
                UserInterface.Active.Root.Find<Panel>("menu").Visible = true;
            };
        }

        public static Panel GenerateSettingsMenu()
        {
            // the Settings panel
            Panel SettingsPanel = new Panel(GameData.SettingsSize, PanelSkin.Default, Anchor.TopLeft, GameData.UIOffset);
            SettingsPanel.Visible = false;
            SettingsPanel.Identifier = "Settings";
            //SettingsPanel.MaxSize = GameData.MainMenuMinSize;
            //SettingsPanel.AdjustHeightAutomatically = true;
            SettingsPanel.PanelOverflowBehavior = PanelOverflowBehavior.VerticalScroll;
            SettingsPanel.Draggable = true;

            // add the title
            SettingsPanel.AddChild(new Paragraph("Settings", anchor: Anchor.TopCenter, color: Color.Red, scale: GameData.TitleScale));
            SettingsPanel.AddChild(new HorizontalLine());

            // add the background speed slider
            SettingsPanel.AddChild(new Paragraph("Background Speed"));
            Slider backgroundSpeed = new Slider(5, 200);
            backgroundSpeed.Identifier = "backgroundSpeed";
            SettingsPanel.AddChild(backgroundSpeed);

            // add the username picker button
            Button nickButton = new Button("Pick Nickname", skin: ButtonSkin.Fancy);
            nickButton.Identifier = "pickName";
            SettingsPanel.AddChild(nickButton);

            // add the fullscreen button
            CheckBox fullscreen = new CheckBox("Fullscreen");
            fullscreen.Identifier = "fullscreen";
            SettingsPanel.AddChild(fullscreen);

            // add the menu scale slider
            SettingsPanel.AddChild(new Paragraph("Menu Scale"));
            Slider menuScale = new Slider(GameData.MinUIScale, GameData.MaxUIScale);
            menuScale.Identifier = "menuScale";
            SettingsPanel.AddChild(menuScale);

            // add the screen size dropdown
            SettingsPanel.AddChild(new Paragraph("Screen Size"));
            DropDown screenHeight = new DropDown();
            //screenHeight.AutoSetListHeight = true;
            screenHeight.Identifier = "screenHeight";
            screenHeight.AddItem("custom");
            for (int i = 0; i < GameData.ScreenSizes.GetLength(0); i++) screenHeight.AddItem(GameData.ScreenSizes[i, 0] + "x" + GameData.ScreenSizes[i, 1]);
            SettingsPanel.AddChild(screenHeight);

            // add the debug boxes
            CheckBox cheat = new CheckBox("Cheats");
            cheat.Identifier = "cheats";
            SettingsPanel.AddChild(cheat);

            CheckBox debug = new CheckBox("Debug");
            debug.Identifier = "debug";
            SettingsPanel.AddChild(debug);

            CheckBox geosketch = new CheckBox("Use Slow GeoSketch (not recommended)");
            geosketch.Identifier = "geosketch";
            SettingsPanel.AddChild(geosketch);

            // add the team picker
            SettingsPanel.AddChild(new Paragraph("Default Team"));
            DropDown team = new DropDown();
            team.AutoSetListHeight = true;
            team.Identifier = "team";
            team.AddItem("Furry");
            team.AddItem("Human");
            team.AddItem("Random");
            SettingsPanel.AddChild(team);

            // add the save and reset buttons
            Button saveButton = new Button("Save", skin: ButtonSkin.Fancy);
            saveButton.Identifier = "saveButton";
            SettingsPanel.AddChild(saveButton);

            Button resetButton = new Button("Reset", skin: ButtonSkin.Fancy);
            resetButton.Identifier = "resetButton";
            SettingsPanel.AddChild(resetButton);

            SetSettingsDefaults(SettingsPanel);
            AddSettingsCallbacks(SettingsPanel);

            return SettingsPanel;
        }

        private static void SetSettingsDefaults(Panel settings)
        {
            settings.Find<Slider>("backgroundSpeed").Value = Game.self.Settings.BGSpeed;
            settings.Find<Slider>("menuScale").Value = (int)(Game.self.Settings.MenuScale * 100);
            settings.Find<CheckBox>("fullscreen").Checked = Game.self.Settings.Fullscreen;
            settings.Find<DropDown>("team").SelectedIndex = Game.self.Settings.DefaultTeam;
            settings.Find<CheckBox>("cheats").Checked = Game.self.Settings.Cheats;
            settings.Find<CheckBox>("debug").Checked = Game.self.Settings.Debug;
            settings.Find<CheckBox>("geosketch").Checked = Game.self.Settings.UseSlowGeoSketch;

            int x = 0;
            for (int i = 0; i < GameData.ScreenSizes.GetLength(0); i++) if (GameData.ScreenSizes[i, 0] == Game.self.Settings.ScreenWidth) x = i + 1;
            settings.Find<DropDown>("screenHeight").SelectedIndex = x;
        }

        private static void AddSettingsCallbacks(Panel settings)
        {
            settings.Find<Slider>("backgroundSpeed").OnValueChange = (Entity entity) =>
            {
                Game.self.Settings.BGSpeed = settings.Find<Slider>("backgroundSpeed").Value;
                MainMenu.Background.speed = Game.self.Settings.BGSpeed;
            };

            settings.Find<Slider>("menuScale").OnValueChange = (Entity entity) =>
            {
                Game.self.Settings.MenuScale = (float)settings.Find<Slider>("menuScale").Value / 100;
                UserInterface.Active.GlobalScale = Game.self.Settings.MenuScale;
            };

            settings.Find<Button>("pickName").OnClick = (Entity entity) =>
            {
                //OpenNamePicker();
            };

            settings.Find<Button>("resetButton").OnClick = (Entity entity) =>
            {
                Game.self.Settings = GameSettings.Reset();
            };

            settings.Find<Button>("saveButton").OnClick = (Entity entity) =>
            {
                Game.self.Settings.Save();
                settings.Visible = false;
                UserInterface.Active.Root.Find<Panel>("menu").Visible = true;
            };

            DropDown screenHeight = settings.Find<DropDown>("screenHeight");
            screenHeight.OnValueChange = (Entity entity) =>
            {
                if (screenHeight.SelectedIndex == 0) return;
                Game.self.Settings.ScreenWidth = GameData.ScreenSizes[screenHeight.SelectedIndex - 1, 0];
                Game.Graphics.PreferredBackBufferWidth = Game.self.Settings.ScreenWidth;
                Game.self.SetScreenAspect();
                Game.Graphics.ApplyChanges();
            };

            settings.Find<DropDown>("team").OnValueChange = (Entity entity) =>
            {
                Game.self.Settings.DefaultTeam = settings.Find<DropDown>("team").SelectedIndex;
            };

            CheckBox fullscreen = settings.Find<CheckBox>("fullscreen");
            fullscreen.OnClick = (Entity btn) =>
            {
                Game.self.Settings.Fullscreen = fullscreen.Checked;
                Game.self.Fullscreen(fullscreen.Checked);

                Game.self.Settings.Save();
                settings.Visible = false;
                UserInterface.Active.Root.Find<Panel>("menu").Visible = true;
            };

            settings.Find<CheckBox>("cheats").OnClick = (Entity btn) =>
            {
                Game.self.Settings.Cheats = settings.Find<CheckBox>("cheats").Checked;
            };

            settings.Find<CheckBox>("debug").OnClick = (Entity btn) =>
            {
                Game.self.Settings.Debug = settings.Find<CheckBox>("debug").Checked;
            };

            settings.Find<CheckBox>("geosketch").OnClick = (Entity btn) =>
            {
                Game.self.Settings.UseSlowGeoSketch = settings.Find<CheckBox>("geosketch").Checked;
            };
        }

        public static Panel GenerateGameOverScreen()
        {
            Panel gameOver = new Panel(GameData.PauseSize, PanelSkin.Fancy, Anchor.Center);
            gameOver.Identifier = "gameover";
            gameOver.Visible = false;
            gameOver.AdjustHeightAutomatically = true;

            // add the title
            gameOver.AddChild(new Paragraph("Game Over", anchor: Anchor.TopCenter, color: Color.Red, scale: GameData.TitleScale));
            gameOver.AddChild(new HorizontalLine());

            // add the rank
            gameOver.AddChild(new Paragraph("current rank: 5", anchor: Anchor.AutoInline, color: Color.LightGray));

            // add the play again, home and highscore buttons
            Button again = new Button("Play Again", skin: ButtonSkin.Fancy);
            again.Identifier = "again";
            gameOver.AddChild(again);

            Button menu = new Button("Menu", skin: ButtonSkin.Fancy);
            menu.Identifier = "menu";
            gameOver.AddChild(menu);

            Button scores = new Button("Hichscores", skin: ButtonSkin.Fancy);
            scores.Identifier = "scores";
            gameOver.AddChild(scores);

            AddGameOverCallbacks(gameOver);

            return gameOver;
        }

        private static void AddGameOverCallbacks(Panel gameOver)
        {
            gameOver.Find<Button>("again").OnClick = (Entity btn) =>
            {
                gameOver.Visible = false;
                Gameplay.OpenGameUI();
            };

            gameOver.Find<Button>("menu").OnClick = (Entity btn) =>
            {
                gameOver.Visible = false;
                MainMenu.OpenMenu();
            };

            gameOver.Find<Button>("scores").OnClick = (Entity btn) =>
            {
                gameOver.Visible = false;
                MainMenu.OpenHighscore();
            };
        }
    }
}