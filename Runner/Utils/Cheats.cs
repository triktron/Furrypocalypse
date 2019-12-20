using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Utils
{
    internal class Cheats
    {
        private static List<Keys> lastKeysConbo = new List<Keys>();

        private static Keys[] lastKeys;
        private static Keys[] currentKeys;
        private static Keys[] pressedKeys;
        private static int maxKeys = 20;
        private static double LasteyPressedTime;
        private static double KeyTimeout = 2000;

        public static class Codes
        {
            public static Keys[] Konami = { Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Left, Keys.Right, Keys.Left, Keys.Right, Keys.A, Keys.B, Keys.Enter };
            public static Keys[] SonixDebug = { Keys.Up, Keys.C, Keys.Down, Keys.C, Keys.Left, Keys.C, Keys.Right, Keys.C, Keys.A, Keys.Enter };
            public static Keys[] DoomGod = { Keys.I, Keys.D, Keys.D, Keys.Q, Keys.D };

            public static Keys[] ExtraLife = { Keys.E, Keys.X, Keys.T, Keys.R, Keys.A };
        }

        public static void Update(GameTime gameTime)
        {
            lastKeys = currentKeys;
            currentKeys = Keyboard.GetState().GetPressedKeys();

            pressedKeys = currentKeys.Where(c => !Array.Exists(lastKeys, x => x.Equals(c))).ToArray();

            if (pressedKeys.Length == 1)
            {
                lastKeysConbo.Add(pressedKeys[0]);
                LasteyPressedTime = 0;
            }

            LasteyPressedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (LasteyPressedTime > KeyTimeout) lastKeysConbo.Clear();

            if (lastKeysConbo.Count > maxKeys) lastKeysConbo.RemoveAt(0);
        }

        public static bool CheckCheat(Keys[] CheatCode)
        {
            if (!Game.self.Settings.Cheats) return false;

            Debug.WriteLine(string.Join(" ", lastKeysConbo));
            int i = 0;
            while (i < CheatCode.Length)
            {
                Keys key = CheatCode[CheatCode.Length - 1 - i];
                if (lastKeysConbo.Count - 1 - i < 0 || key != lastKeysConbo[lastKeysConbo.Count - 1 - i]) return false;
                i++;
            }

            if (i == CheatCode.Length)
            {
                lastKeysConbo.Clear();
                return true;
            }
            else return false;
        }
    }
}