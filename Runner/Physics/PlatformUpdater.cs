using Runner.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Physics
{
    class PlatformUpdater
    {
        static public void UpdatePlatform(Platform platform, float timeMuly)
        {
            platform.offset -= (int)(timeMuly * 5);

            if (platform.offset < -platform.Destintation().Width) platform.IsActive = false;
        }
    }
}
