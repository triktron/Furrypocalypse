using Runner.Runners;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Physics
{
    class Positioner
    {
        public static void SetDesiredPlayerPos(ref List<BaseRunner> runners)
        {
            int from = (int)(0.2f * Game.Graphics.GraphicsDevice.Viewport.Width);
            int to = (int)(0.4f * Game.Graphics.GraphicsDevice.Viewport.Width);
            int space = to - from;
            float spaceBetween = (float)space / runners.Count;

            

            for (int i = 0; i < runners.Count; i++)
            {
                runners[i].DesiredPos = spaceBetween * i + from;
            }
        }
    }
}
