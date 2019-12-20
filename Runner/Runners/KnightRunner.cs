using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Runners
{
    class KnightRunner : BaseRunner
    {
        public override string TexturePath { get { return "Graphics/Runners/Knight"; } }
        public override float presetScale { get { return 1.2f; } }
        public override Rectangle _hitBox { get { return new Rectangle(110, 77, 134, 227); } } // check if is correct

        public override Point RunWindow => new Point(50, 50 + 9);

        public override Point JumpWindow => new Point(40,40+9);

        public override Point IdleWindow => new Point(20,20+9);

        public override Point DeadWindow => new Point(10,10+9);

        public override Point TextureSize => new Point(7, 10);
    }
}
