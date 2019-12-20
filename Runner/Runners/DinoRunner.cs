using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Runners
{
    class DinoRunner : BaseRunner
    {
        public override string TexturePath { get { return "Graphics/Runners/Dino"; } }
        public override float presetScale { get { return 1; } }

        public override Rectangle _hitBox { get { return new Rectangle(100, 70, 212/3*2, 290); } }

        public override Point RunWindow => new Point(30, 30 + 7);

        public override Point JumpWindow => new Point(18,18+11);

        public override Point IdleWindow => new Point(8,8+9);

        public override Point DeadWindow => new Point(0,9);

        public override Point TextureSize => new Point(7, 7);
    }
}
