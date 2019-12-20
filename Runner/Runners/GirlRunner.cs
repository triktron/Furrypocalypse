using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Runners
{
    class GirlRunner : BaseRunner
    {
        public override string TexturePath { get { return "Graphics/Runners/Girl"; } }
        public override float presetScale { get { return 1; } }

        public override Rectangle _hitBox { get { return new Rectangle(66*2 + 30, 29*2 + 10, 133, 271); } } 
        public override Point RunWindow => new Point(76, 76 + 19);

        public override Point JumpWindow => new Point(46,46+29);

        public override Point IdleWindow => new Point(30,30+15);

        public override Point DeadWindow => new Point(0,29);

        public override Point TextureSize => new Point(12, 10);
    }
}
