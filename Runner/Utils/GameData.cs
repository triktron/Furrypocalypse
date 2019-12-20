using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Utils
{
    internal static class GameData
    {
        public static int[,] ScreenSizes = new int[,]{
            {256, 144},
            {426, 240},
            {640, 360},
            {768, 432},
            {800, 450},
            {848, 480},
            {854, 480},
            {960, 540},
            {1024, 576},
            {1280, 720 },
            {1366, 768},
            {1600, 900 },
            {1920, 1080}
        };

        public static Vector2 MainMenuSize = new Vector2(0.44f, 0.8f);
        public static Vector2 MainMenuMinSize = new Vector2(400, -1);
        public static Vector2 UIOffset = new Vector2(20, 20);
        public static Vector2 ButtonSize = new Vector2(0, 0.2f);
        public static Vector2 ButtonMaxSize = new Vector2(0, 50);
        public static string GameName = "Furrypocalypse";
        public static float TitleScale = 1.4f;

        public static Vector2 HighscoreSize = new Vector2(0.5f, 0.9f);

        public static Vector2 SettingsSize = new Vector2(600, 0.8f);
        public static uint MinUIScale = 50;
        public static uint MaxUIScale = 200;

        public static Vector2 PauseSize = new Vector2(0.4f, 0.7f);
    }
}