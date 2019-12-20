using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Obsticals
{
    abstract class BaseOpstical : BaseEntity
    {
        public enum AlignOptions
        {
            None,
            Top,
            Bottom,
            Right,
        }

        public abstract AlignOptions Aligned { get; }
        public Texture2D Texture;
        public abstract string TexturePath { get; }

        public Vector2 Pos;
        GraphicsDeviceManager Graphics;
        public override float Scale { get { return 1; } }
        public override Vector2 Speed { get { return new Vector2(-5,0); } }

        public void Load(ContentManager content)
        {
            Texture = content.Load<Texture2D>(TexturePath);
        }

        public void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Aligned == AlignOptions.Top) Pos.Y = 0;
            if (Aligned == AlignOptions.Bottom) Pos.Y = Graphics.GraphicsDevice.Viewport.Height - Destination.Height;
            if (Aligned == AlignOptions.Right) Pos.Y = Graphics.GraphicsDevice.Viewport.Width - Destination.Width;
        }

        public Rectangle Destination
        {
            get
            {
                return new Rectangle(Pos.ToPoint(), new Point((int)(Texture.Width * Scale), (int)(Texture.Height * Scale)));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Destination, Color.White);
        }

        internal void SetGraphics(GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
        }
    }
}
