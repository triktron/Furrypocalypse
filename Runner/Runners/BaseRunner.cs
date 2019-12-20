using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Runner.Graphics;
using Runner.Utils;
using Primitives2D;
using System.Diagnostics;

namespace Runner.Runners
{
    internal abstract class BaseRunner : BaseEntity
    {
        public abstract Point RunWindow { get; }
        public abstract Point JumpWindow { get; }
        public abstract Point IdleWindow { get; }
        public abstract Point DeadWindow { get; }
        public abstract Point TextureSize { get; }

        public bool AutoAnimationState = true;

        public int JumpsLeft;
        public int MaxJumps = 1;

        public int Strength = 5;

        public enum State
        {
            Running,
            Jump,
            Idle,
            Dead
        }

        public State CurrentState;

        public SpriteSheet SpriteSheet;
        public float DesiredPos;

        /// <summary>
        /// Loads the Runner sprites
        /// </summary>
        /// <param name="content">The Content Manager of the Game</param>
        public void Load(ContentManager content)
        {
            base.Load(content);
            SpriteSheet = new SpriteSheet(Texture, TextureSize.X, TextureSize.Y, FlipFacing);

            float HeightScale = (float)Game.Graphics.GraphicsDevice.Viewport.Height / 1024;

            Gravety = new Vector2(0, 0.025f) / HeightScale;
            InitialSpeed = new Vector2(0, 20);
        }

        /// <summary>
        /// returns the hitbox rectangle of the current spritesheet
        /// </summary>

        /// <summary>
        /// returns the healthbar rectangle
        /// </summary>
        public Rectangle Healthbar(float scale)
        {
            Vector2 size = new Vector2(Health, 10) * scale;

            return new Rectangle(Hitbox.Location, size.ToPoint());
        }

        /// <summary>
        /// Draws the Runner on the surface provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteSheet.Draw(spriteBatch, Scale * presetScale, _hitBox.Location.ToVector2() * Scale * presetScale);

            if (ShowHealth)
            {
                spriteBatch.FillRectangle(Healthbar(1), Color.Black, 4);
                spriteBatch.FillRectangle(Healthbar(0.9f), Color.Red, 4);
            }

            base.Draw(spriteBatch);
        }

        /// <summary>
        /// is the animation loop of the current sprite done
        /// </summary>
        public bool IsAnmationDone()
        {
            return SpriteSheet.IsIndexOutsideWindow();
        }

        /// <summary>
        /// Reset the animation loop
        /// </summary>
        internal void resetLoop()
        {
            SpriteSheet.resetLoop();
        }

        /// <summary>
        /// Updates the runners animation
        /// </summary>
        /// <param name="gameTime">The Gametime object of that step</param>
        public void Update(GameTime gameTime)
        {
            Health += HealAmount;
            if (Health > MaxHealth) Health = MaxHealth;

            if (AutoAnimationState)
            {
                if (ColisionSide.Left)
                {
                    SpriteSheet.Loop = true;
                    SetAnimation(State.Idle);
                }
                else if (ColisionSide.Top)
                {
                    SpriteSheet.Loop = true;
                    SetAnimation(State.Running);
                    JumpsLeft = MaxJumps;
                }
                else if (CurrentState != State.Jump)
                {
                    SpriteSheet.Loop = false;
                    SetAnimation(State.Jump);
                    SpriteSheet.resetLoop();
                }
            }

            if (Math.Abs(DesiredPos - Position.X) > 5)
            {
                float speed = DesiredPos - Position.X;
                speed = 5 * Math.Sign(DesiredPos - Position.X);
                Speed = new Vector2((int)speed, Speed.Y);
            }
            else
            {
                Speed = new Vector2(0, Speed.Y);
            }

            //Debug.WriteLine((float)Game.Graphics.GraphicsDevice.Viewport.Height / 1024);

            SpriteSheet.Update(gameTime);
            base.Update(gameTime);
        }

        public void SetAnimation(State state)
        {
            CurrentState = state;
            if (state == State.Dead) SpriteSheet.IndexWindow = DeadWindow;
            if (state == State.Idle) SpriteSheet.IndexWindow = IdleWindow;
            if (state == State.Jump) SpriteSheet.IndexWindow = JumpWindow;
            if (state == State.Running) SpriteSheet.IndexWindow = RunWindow;
        }

        /// <summary>
        /// get or set the current spritesheets position
        /// </summary>
        public override Vector2 Position
        {
            get
            {
                return SpriteSheet.ScreenPos;
            }

            set
            {
                SpriteSheet.ScreenPos = value;
            }
        }

        public override Rectangle Destination
        {
            get
            {
                return SpriteSheet.Destination(Scale * presetScale, _hitBox.Location.ToVector2() * Scale * presetScale);
            }
        }
    }
}