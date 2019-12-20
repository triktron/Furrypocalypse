using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Graphics
{
    class SpriteSheet
    {
        private Texture2D Sheet;
        private int Columns;
        private int Rows;
        public Point IndexWindow;
        private int CurrentSpriteIndex;
        private double SpriteDuration = 400;
        private double CurrentSpriteDuration;
        private bool FacingLeft;

        public bool Loop = true;
        public Vector2 ScreenPos;

        /// <summary>
        /// Initializes a new SpriteSheet
        /// </summary>
        /// <param name="texture">The texture of the spitesheet</param>
        /// <param name="rows">The amount of rows the texture has</param>
        /// <param name="columns">The amount of columns the texture has</param>
        public SpriteSheet(Texture2D texture, int rows, int columns, bool facingLeft)
        {
            Sheet = texture;
            Rows = rows;
            Columns = columns;
            FacingLeft = facingLeft;
        }

        /// <summary>
        /// Draws the SpriteSheet on the surface provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            Draw(spriteBatch, 1, offset);
        }

        /// <summary>
        /// Get the source frame width
        /// </summary>
        private float SourceCellWidth
        {
            get
            {
                return (float)Sheet.Width / Columns;
            }
        }

        /// <summary>
        /// Get the frame height
        /// </summary>
        private float SourceCellHeight
        {
            get
            {
                return (float)Sheet.Height / Rows;
            }
        }

        /// <summary>
        /// Get the source frame rectangle
        /// </summary>
        private Rectangle sourceRectangle
        {
            get
            {
                int currentRow = CurrentSpriteIndex / Columns;
                int CurrentColumn = CurrentSpriteIndex % Columns;

                return new Rectangle((int)(CurrentColumn * SourceCellWidth),
                    (int)(currentRow * SourceCellHeight),
                    (int)SourceCellWidth,
                    (int)SourceCellHeight);
            }
        }

        /// <summary>
        /// Get the destination rectangle of the sprite
        /// </summary>
        /// <param name="scale">The Scale of the sprite</param>
        public Rectangle Destination(float scale, Vector2 offset)
        {
            Vector2 Pos = ScreenPos - offset;
            Vector2 Size = new Vector2(SourceCellWidth, SourceCellHeight) * scale;

            return new Rectangle(Pos.ToPoint(), Size.ToPoint());
        }

        /// <summary>
        /// Draws the Sprite on the surface provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="scale">The Scale of the sprite</param>
        internal void Draw(SpriteBatch spriteBatch, float scale, Vector2 offset)
        {
            spriteBatch.Draw(this.Sheet, Destination(scale, offset), sourceRectangle, Color.White, 0f, Vector2.Zero, FacingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 1f);
        }

        /// <summary>
        /// Go to the next frame
        /// </summary>
        public void NextSprite()
        {
                CurrentSpriteIndex++;
            Clamp();
        }

        public void Clamp()
        {
            if (IsIndexOutsideWindow() && Loop)
            {
                CurrentSpriteIndex = IndexWindow.X;
            }
            else if (IsIndexOutsideWindow())
            {
                CurrentSpriteIndex = IndexWindow.Y;
            }
        }

        /// <summary>
        /// Returns if the animation is done
        /// </summary>
        public bool IsIndexOutsideWindow()
        {
            return CurrentSpriteIndex >= IndexWindow.Y || CurrentSpriteIndex < IndexWindow.X;
        }

        /// <summary>
        /// Is the amount of time per frame done
        /// </summary>
        /// <param name="gameTime">The Gametime object of that step</param>
        private bool IsTimeEleapsed(GameTime gameTime)
        {
            CurrentSpriteDuration += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            return CurrentSpriteDuration >= SpriteDuration / (IndexWindow.Y - IndexWindow.X);
        }

        /// <summary>
        /// Update the animation
        /// </summary>
        /// <param name="gameTime">The Gametime object of that step</param>
        public void Update(GameTime gameTime)
        {
            Clamp();

            if (IsTimeEleapsed(gameTime))
            {
                CurrentSpriteDuration = 0;
                NextSprite();
            }
        }

        /// <summary>
        /// Reset the animation loop
        /// </summary>
        public void resetLoop()
        {
            CurrentSpriteIndex = IndexWindow.X;
        }
    }
}