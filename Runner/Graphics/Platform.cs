using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Runner.Graphics
{
    class Platform
    {
        public float height = .1f;
        public int width = 4;
        public int offset = 50;

        private float scale = 0.6f;
        public bool IsActive = true;

        /// <summary>
        /// Initializes a Platform
        /// </summary>
        /// <param name="offset">The Offset in pixels from the left</param>
        /// <param name="height">The Offset in pixels from the bottom</param>
        /// <param name="width">The Width of the platform in segments</param>
        public Platform(int offset, float height, int width)
        {
            this.offset = offset;
            this.height = height;
            this.width = width;
        }

        static public class Textures
        {
            static public Texture2D TopLeft;
            static public Texture2D TopMid;
            static public Texture2D TopRight;

            static public Texture2D BottomLeft;
            static public Texture2D BottomMid;
            static public Texture2D BottomRight;
        }

        /// <summary>
        /// Loads the platform sprites
        /// </summary>
        /// <param name="content">The Content Manager of the Game</param>
        public static void Load(ContentManager content)
        {
            Textures.TopLeft = content.Load<Texture2D>("Graphics/Platforms/Tile_1");
            Textures.TopMid = content.Load<Texture2D>("Graphics/Platforms/Tile_2");
            Textures.TopRight = content.Load<Texture2D>("Graphics/Platforms/Tile_3");

            Textures.BottomLeft = content.Load<Texture2D>("Graphics/Platforms/Tile_4");
            Textures.BottomMid = content.Load<Texture2D>("Graphics/Platforms/Tile_5");
            Textures.BottomRight = content.Load<Texture2D>("Graphics/Platforms/Tile_6");
        }

        /// <summary>
        /// Get the Destination rectagle
        /// </summary>
        public Rectangle Destintation()
        {
            Vector2 size = new Vector2(Destintation(Textures.TopLeft).Width + Destintation(Textures.TopMid).Width * width + Destintation(Textures.TopRight).Width, (1 - height) * Game.Graphics.GraphicsDevice.Viewport.Height);
            Point pos = new Point(offset, (int)((1 - height) * Game.Graphics.GraphicsDevice.Viewport.Height));

            return new Rectangle(pos, size.ToPoint());
        }

        /// <summary>
        /// Get the Destination rectagle with respect to the texture position
        /// </summary>
        /// <param name="tex">the Texture of the sprite</param>
        /// <param name="pos">the Position of the sprite</param>
        private Rectangle Destintation(Texture2D tex, Point pos = new Point())
        {
            return new Rectangle(pos, new Point((int)(scale * tex.Width), (int)(scale * tex.Height)));
        }

        /// <summary>
        /// Draws the Platform on the surface provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Point TopLeftPos = Destintation().Location;
            spriteBatch.Draw(Textures.TopLeft, Destintation(Textures.TopLeft, TopLeftPos), Color.White);

            TopLeftPos.Y += Destintation(Textures.TopLeft).Height;
            while (TopLeftPos.Y < Game.Graphics.GraphicsDevice.Viewport.Height)
            {
                spriteBatch.Draw(Textures.BottomLeft, Destintation(Textures.BottomLeft, TopLeftPos), Color.White);
                TopLeftPos.Y += Destintation(Textures.BottomLeft).Height;
            }

            Rectangle TopMidPos = Destintation();
            TopMidPos.X += Destintation(Textures.TopLeft).Width;

            for (int i = 0;i < width;i++)
            {
                spriteBatch.Draw(Textures.TopMid, Destintation(Textures.TopMid, TopMidPos.Location), Color.White);
                TopMidPos.X += Destintation(Textures.TopMid).Width;
            }

            Point pos = Destintation().Location;
            pos += Destintation(Textures.TopLeft).Size;

            Point size = Destintation().Size;
            size.X -= Destintation(Textures.TopLeft).Width + Destintation(Textures.TopRight).Width;

            spriteBatch.Draw(Textures.BottomMid, new Rectangle(pos, size), Color.White);


            Rectangle TopRightPos = Destintation();
            TopRightPos.X += TopRightPos.Width - Destintation(Textures.TopRight).Width;
            spriteBatch.Draw(Textures.TopRight, Destintation(Textures.TopRight, TopRightPos.Location), Color.White);

            TopRightPos.Y += Destintation(Textures.TopRight).Height;
            while (TopRightPos.Y < Game.Graphics.GraphicsDevice.Viewport.Height)
            {
                spriteBatch.Draw(Textures.BottomRight, Destintation(Textures.BottomRight, TopRightPos.Location), Color.White);
                TopRightPos.Y += Destintation(Textures.BottomRight).Height;
            }
        }
    }
}
