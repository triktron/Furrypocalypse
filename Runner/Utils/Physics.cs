using Microsoft.Xna.Framework;
using Runner.Graphics;
using Runner.Runners;
using System.Collections.Generic;
using System.Diagnostics;

namespace Runner.Utils
{
    class Physics
    {
        static public int PlayerPosByIndex(int index, int count)
        {
            int minOffset = 20;
            int MaxOffset = Game.self.GraphicsDevice.Viewport.Width / 3;

            int totalSpace = MaxOffset - minOffset;

            float offsetPerPlayer = (float)totalSpace / count;
            int playerPos = (int)(offsetPerPlayer * (count - index)) + minOffset;
            return playerPos;
        }

        static private bool WantsToJump(int runnerOffset)
        {
            float offset = 0.1f;
            return InputDelay.IsPressed((int)(offset * runnerOffset));
        }

        static public void SetPlayerPos(List<BaseRunner> runners)
        {
            for (int i = 0;i < runners.Count;i++)
            {
                runners[i].Position += new Vector2(PlayerPosByIndex(i, runners.Count),0);
            }
        }

        static public void UpdateRunner(BaseRunner runner, float timeMuly, int runnerIndex, int runnerCount, int fartest)
        {
            runner.Position += new Vector2(0, 4);

            int desiredOffset = PlayerPosByIndex(runnerIndex, runnerCount);
            if (runner.Position.X < desiredOffset) runner.Position += new Vector2(1, 0) * timeMuly;

            if (runner.isOnGround) runner.VerticalSpeed = 0;


            int jumpOffset = fartest - (int)runner.Position.X;
            if (WantsToJump(jumpOffset) && runner.isOnGround)
                {
                    runner.resetLoop();
                    runner.VerticalSpeed = runner.Volecety;
                }
                else if (WantsToJump(jumpOffset))
                {
                    runner.VerticalSpeed += runner.Gravety / 1.5f;
                }

            runner.Position -= new Vector2(0, runner.VerticalSpeed * Game.self.GraphicsDevice.Viewport.Height) * timeMuly;
            runner.VerticalSpeed -= runner.Gravety;

            if (runner.VerticalSpeed < -0.1f) 
                runner.VerticalSpeed = -0.1f;

            if (runner.isAgainstWall && runner.isOnGround)
            {
                runner.CurrentState = BaseRunner.State.Idle;
            } 
            else if (runner.isOnGround)
            {
                runner.CurrentState = BaseRunner.State.Running;
            } 
            else
            {
                if (runner.CurrentState != BaseRunner.State.Jump)
                {
                    runner.CurrentState = BaseRunner.State.Jump;
                    runner.resetLoop();
                }

                runner.CurrentState = BaseRunner.State.Jump;
            }
        }

        static public void UpdateEnemy(BaseRunner runner, float timeMuly, int runnerIndex, int runnerCount, int fartest)
        {
            runner.Position -= new Vector2(5,-4);

            
            runner.CurrentState = BaseRunner.State.Idle;
        }

        

        static public void PushOutOfWalls(BaseRunner runner, Platform platform)
        {
            Rectangle rect = Rectangle.Intersect(runner.Hitbox, platform.Destintation());

            if (rect == Rectangle.Empty) return;

           
            int maxJump = (runner.Destination.Height / 4);

            if (rect.Height < maxJump || rect.Width == runner.Hitbox.Width)
            {
                runner.isOnGround = true;
                runner.Position -= new Vector2(0, rect.Height);
            } else 
            {
                runner.isAgainstWall = true;
                runner.Position -= new Vector2(rect.Width, 0);
            }
        }

        public static bool RunnerColide(BaseRunner runner, Rectangle hitbox)
        {
            Rectangle rect = Rectangle.Intersect(runner.Hitbox, hitbox);

            return rect != Rectangle.Empty;
        }

        public static Rectangle PlaceOnGround(Rectangle rect, List<Platform> platforms)
        {
            foreach (Platform platform in platforms)
            {
                if (platform.Destintation().X < rect.X && platform.Destintation().X + platform.Destintation().Width > rect.X + rect.Width)
                {
                    rect.Y = platform.Destintation().Y - rect.Height;
                }
            }

            return rect;
        }
    }
}
