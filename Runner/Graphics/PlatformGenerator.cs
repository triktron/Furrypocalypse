using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Graphics
{
    class PlatformGenerator
    {
        /// <summary>
        /// Generate and Add the next Platform to the provided list
        /// </summary>
        /// <param name="platforms">The list of platforms to add to</param>
        public void Next(List<Platform> platforms)
        {
            float maxHeight = .5f;
            float minHeight = 0.05f;

            int minWidth = 2;
            int maxWidth = 7;

            int gapRand = 10;
            int minGapWidth = 1;
            int maxGapWidth = 2;

            int offset = -50;
            if (platforms.Count > 0) offset += platforms.Last().offset + platforms.Last().Destintation().Width;

            if (platforms.Count > 0 && platforms.Last().height > 0 && Game.random.Next(gapRand) == 0)
            {
                platforms.Add(new Platform(offset, -1f,
                    Game.random.Next(minGapWidth, maxGapWidth)));
            } else
            {
                platforms.Add(new Platform(offset,
                    (float)Game.random.NextDouble() * maxHeight + minHeight,
                    Game.random.Next(minWidth, maxWidth)));
            } 
        }
    }
}
