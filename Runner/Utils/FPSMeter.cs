using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Utils
{
    class FPSMeter
    {
            private double frames = 0;
            private double updates = 0;
            private double elapsed = 0;
            private double last = 0;
            private double now = 0;
            private double frameTime = 0;
            public double msgFrequency = 1f;
            public string msg = "Mesuring FPS";

            /// <summary>
            /// The msgFrequency here is the reporting time to update the message.
            /// </summary>
            public void Update(GameTime gameTime)
            {
            // I do this because i usually want to get the time now.
            // As well as count the updates draws ect.

            now = gameTime.TotalGameTime.TotalMilliseconds / 1000;

                elapsed = (double)(now - last);
                if (elapsed > msgFrequency)
                {
                    //msg = " Fps: " + Math.Round(frames / elapsed,3) + " Elapsed time: " + Math.Round(60f/(frames / elapsed), 3) + "ms\nUpdates: " + updates.ToString() + " Frames: " + frames.ToString();
                    elapsed = 0;
                    frames = 0;
                    updates = 0;
                    last = now;
                }
                updates++;
            }

            public string GetFps()
            {
                frames++;
                return msg;
            }
    }
}
