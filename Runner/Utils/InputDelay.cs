using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Utils
{
    internal static class InputDelay
    {
        private static int delayLength = 2000;

        private static bool[] History = new bool[delayLength];
        private static int index;
        private static bool LastJump;

        public static void Update()
        {
            index++;
            if (index >= delayLength) index = 0;

            History[index] = Keyboard.GetState().IsKeyDown(Keys.Space) && !LastJump;
            LastJump = Keyboard.GetState().IsKeyDown(Keys.Space);
        }

        public static bool IsPressed(int i)
        {
            int offset = index - i;
            if (offset < 0) offset += delayLength;
            if (i == 0)
            {
                i = 200;
            }

            return History[offset % delayLength];
        }
    }
}