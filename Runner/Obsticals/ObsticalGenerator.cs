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
    class ObsticalGenerator
    {
        List<BaseOpstical> Obsticals = new List<BaseOpstical>();
        GraphicsDeviceManager Graphics;
        ContentManager Content;
        float Scale;
        Random rand = new Random();

        public void Load(ContentManager content)
        {
            Content = content;
        }

        public void update(GameTime gameTime)
        {
            for (int i = Obsticals.Count - 1;i >= 0; i--)
            {
                Obsticals[i].Update(gameTime);
                if (Obsticals[i].Pos.X < 0) Obsticals.RemoveAt(i);
            }

            if (rand.Next(60) == 200) GenerateNewObstical();
        }

        private void GenerateNewObstical()
        {
            ObsticalSpike spike = new ObsticalSpike();
            spike.Pos.X = Graphics.GraphicsDevice.Viewport.Width;
            //spike.Scale = Scale;
            spike.Load(Content);
            spike.SetGraphics(Graphics);

            Obsticals.Add(spike);
        }

        public bool IsHit(Rectangle player)
        {
            return Obsticals.Any(x => (Rectangle.Intersect(player, x.Destination) != Rectangle.Empty));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (BaseOpstical obstical in Obsticals) obstical.Draw(spriteBatch);
        }

        internal void Clear()
        {
            Obsticals.Clear();
        }

        internal void SetGraphics(GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
            float windowHeight = Graphics.GraphicsDevice.Viewport.Height;
            Scale = windowHeight / 2000;
        }
    }
}
