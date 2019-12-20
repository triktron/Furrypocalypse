using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Runner.Runners
{
    class DogRunner : BaseRunner
    {
        public override string TexturePath { get { return "Graphics/Runners/Dog"; } }
        public override float presetScale { get { return 1; } }

        public override Rectangle _hitBox { get { return new Rectangle(45+70,65+27,192,299); } }

        public override Point RunWindow => new Point(46, 46 + 7);

        public override Point JumpWindow => new Point(38,38+7);

        public override Point IdleWindow => new Point(28,28+9);

        public override Point DeadWindow => new Point(0,9);

        public override Point TextureSize => new Point(10, 8);
    }
}