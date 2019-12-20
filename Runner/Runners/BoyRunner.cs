using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Runners
{
    class BoyRunner : BaseRunner
    {
        public override string TexturePath { get { return "Graphics/Runners/Boy"; } }
        public override float presetScale { get { return 1; } }

        public override Rectangle _hitBox { get { return new Rectangle(63-20, 75, 124, 285); } }

        public override Point RunWindow => new Point(45, 45+14);

        public override Point JumpWindow => new Point(30,30+14);

        public override Point IdleWindow => new Point(15,15+14);

        public override Point DeadWindow => new Point(0, 14);

        public override Point TextureSize => new Point(10, 8);
    }
}
