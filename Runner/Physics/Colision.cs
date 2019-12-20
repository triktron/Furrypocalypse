using Microsoft.Xna.Framework;
using System;

namespace Runner.Physics
{
    class Colision
    {
        public enum Side
        {
            Top,
            Bottom,
            Left,
            Right,
            None
        }

        public static Side GetColisonSide(Rectangle rect1, Rectangle rect2)
        {
            Point midRect1 = new Point(rect1.Left + rect1.Width / 2,
                                           rect1.Top + rect1.Height / 2);

            Point midRect2 = new Point(rect2.Left + rect2.Width / 2,
                                       rect2.Top + rect2.Height / 2);

            // no collision
            bool coll_X = rect1.Right >= rect2.Left && rect1.Left <= rect2.Right;
            bool coll_Y = rect1.Bottom >= rect2.Top && rect1.Top <= rect2.Bottom;
            if (!(coll_X && coll_Y)) return Side.None;

            // calculate bias flag in each direction
            bool bias_X = midRect1.X < midRect2.X;
            bool bias_Y = midRect1.Y < midRect2.Y;

            // calculate penetration depths in each direction
            float pen_X = bias_X ? (rect1.Right - rect2.Left)
                               : (rect2.Right - rect1.Left);
            float pen_Y = bias_Y ? (rect1.Bottom - rect2.Top)
                               : (rect2.Bottom - rect1.Top);
            float diff = pen_X - pen_Y;


            // X penetration greater
            if (diff >= 0)
                if (bias_Y)
                {
                    return Side.Top;
                }
                else
                {
                    return Side.Bottom;
                }

            // Y pentration greater
            else if (diff < 0)
                if (bias_X)
                {
                    return Side.Left;
                }
                else
                {
                    return Side.Right;
                }

            return Side.None;
        }

        public static Vector2 Resolve(Rectangle rect1, Rectangle rect2)
        {
            Side side = GetColisonSide(rect1, rect2);

            if (side == Side.Left) return new Vector2(rect2.X - rect1.Width, rect1.Location.Y);
            if (side == Side.Right) return new Vector2(rect2.X + rect2.Width, rect1.Location.Y);
            if (side == Side.Top) return new Vector2(rect1.Location.X, rect2.Y - rect1.Height);
            if (side == Side.Bottom) return new Vector2(rect1.Location.X, rect2.Y + rect1.Height);

            return rect1.Location.ToVector2();
        }
    }
}
