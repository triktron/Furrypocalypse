using Microsoft.Xna.Framework;
using Runner.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Runner.Runners;
using System.Diagnostics;

namespace Runner.Physics
{
    class Gravety
    {
        public static void Update(ref BaseRunner entity, List<Platform> ColisonObjects, GameTime gameTime)
        {
            entity.Speed += entity.Gravety * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            
            entity.Position += entity.Speed;

            entity.ColisionSide.Top = false;
            entity.ColisionSide.Bottom = false;
            entity.ColisionSide.Left = false;
            entity.ColisionSide.Right = false;

            foreach (Platform colisonObject in ColisonObjects)
            {
                Colision.Side side = Colision.GetColisonSide(entity.Hitbox, colisonObject.Destintation());

                if (side.Equals(Colision.Side.Top)) entity.ColisionSide.Top = true;
                if (side.Equals(Colision.Side.Bottom)) entity.ColisionSide.Bottom = true;
                if (side.Equals(Colision.Side.Left)) entity.ColisionSide.Left = true;
                if (side.Equals(Colision.Side.Right)) entity.ColisionSide.Right = true;

                entity.Position = Colision.Resolve(entity.Hitbox, colisonObject.Destintation());
            }

            if (entity.ColisionSide.Top) entity.Speed = new Vector2(entity.Speed.X, entity.Speed.Y > 0 ? 0 : entity.Speed.Y);
        }

        public static void Jump(ref BaseRunner entity, GameTime gameTime)
        {
            entity.Speed = new Vector2(entity.Speed.X, -entity.InitialSpeed.Y);
            entity.JumpsLeft--;
        }
    }
}
