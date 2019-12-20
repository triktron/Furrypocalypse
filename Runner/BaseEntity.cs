using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Runner.Physics;
using GeoSketch;
using Primitives2D;

namespace Runner
{
    abstract class BaseEntity
    {
        public virtual Vector2 Speed { get; set; }
        public virtual Vector2 InitialSpeed { get; set; }
        public abstract float presetScale { get; }
        public virtual Vector2 Gravety { get; set; }

        virtual public Vector2 Position { get; set; }
        public Texture2D Texture;
        public virtual string TexturePath { get; }
        public abstract Rectangle _hitBox { get; }

        private float scaleMultiplier = 1;
        public virtual float Scale { 
            get { return (float)Game.Graphics.GraphicsDevice.Viewport.Height / 1400 * scaleMultiplier; } 
            set { scaleMultiplier = value; } 
        }

        public bool IsActive = true;

        public bool FlipFacing;

        public int HealAmount = 2;
        public int Health = 100;
        public int MaxHealth = 100;
        public bool ShowHealth = true;

        public ColisionSide ColisionSide = new ColisionSide();

            public void Load(ContentManager content)
            {
                Texture = content.Load<Texture2D>(TexturePath);
            }

            public void Update(GameTime gameTime)
            {
            if (ShouldDie()) IsActive = false;
            }

            public void Draw(SpriteBatch spriteBatch)
            {
            if (Game.self.Settings.Debug)
            {
                if (Game.self.Settings.UseSlowGeoSketch)
                {
                    spriteBatch.DrawRectangle((int)Hitbox.X, (int)Hitbox.Y, (int)Hitbox.Width, (int)Hitbox.Height, Color.Transparent, Color.Black, 4);
                    spriteBatch.DrawRectangle((int)Destination.X, (int)Destination.Y, (int)Destination.Width, (int)Destination.Height, Color.Transparent, Color.Red, 4);
                    spriteBatch.DrawCircle((int)Position.X, (int)Position.Y, 5, Color.Transparent, Color.Green, 2);
                }
                else
                {
                    spriteBatch.DrawRectangle(Hitbox, Color.Black, 4);
                    spriteBatch.DrawRectangle(Destination, Color.Red, 4);
                    spriteBatch.DrawCircle(Position, 5, 10, Color.Green, 2);
                }
            }
        }

        public virtual Rectangle Destination
        {
            get
            {
                Point Size = new Point((int)(Texture.Width * Scale * presetScale), (int)(Texture.Height * Scale * presetScale));
                Vector2 pos = Position - (_hitBox.Location.ToVector2() * Scale * presetScale);

                return new Rectangle(pos.ToPoint(), Size);
            }
        }

        public Rectangle Hitbox
        {
            get
            {
                Vector2 pos = Position;// _hitBox.Location.ToVector2() * Scale * presetScale;
                Vector2 size = _hitBox.Size.ToVector2() * Scale * presetScale;

                /*if (FlipFacing)
                {
                    pos.X = Destination.Width - size.X - pos.X;
                }*/

                //pos += Position;

                return new Rectangle(pos.ToPoint(), size.ToPoint());
            }
        }

        internal bool ShouldDie()
        {
            return Destination.Y - Destination.Height > Game.Graphics.GraphicsDevice.Viewport.Height ||
                    Destination.X < -Destination.Width ||
                    HasNoHealth();
        }

        internal bool HasNoHealth()
        {
            return Health < 0;
        }

    }

    public class ColisionSide
    {
        public bool Top;
        public bool Bottom;
        public bool Left;
        public bool Right;
    }
}
