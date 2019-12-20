﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Runner.Obsticals
{
    class ObsticalSaw : BaseOpstical
    {
        public override AlignOptions Aligned => AlignOptions.Top;

        public override string TexturePath { get { return "Graphics/Obsticals/Saw"; } }

        public override Vector2 Speed { set => throw new NotImplementedException(); }

        public override Rectangle _hitBox { get { return new Rectangle(35, 18, 292, 470); } } // check if is correct
        public override float presetScale { get { return 1; } }

    }
}
