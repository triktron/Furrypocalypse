using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Runner.Graphics
{
    class Background
    {
        private string[][] layers =
        {
            new string[] { 
                "sky",
                "clouds_1",
                "clouds_2",
                "rocks_1",
                "rocks_2",
                "clouds_3",
                "clouds_4", },
            new string[]
            {
                "sky",
                "clouds_3",
                "rocks_3",
                "rocks_2",
                "clouds_2",
                "rocks_1",
                "pines",
                "clouds_1",
                "birds",
            },
            new string[]
            {
                "sky",
                "rocks",
                "clouds_2",
                "ground_1",
                "plant",
                "ground_3",
                "ground_2",
                "clouds_1",
            },
            new string[]
            {
                "sky",
                "rocks",
                "ground",
                "clouds_2",
                "clouds_1",
            }
        };
        public int ID;
        private List<Texture2D> layer_text = new List<Texture2D>();
        private List<int> layer_offset = new List<int>();

        public int speed = 100;
        private int speedIncrese = 3;

        /// <summary>
        /// Loads the background sprites
        /// </summary>
        /// <param name="content">The Content Manager of the Game</param>
        public void Load(ContentManager content)
        {
            foreach (string layerName in layers[ID])
            {
                layer_text.Add(content.Load<Texture2D>("Graphics/Backgrounds/" + (ID+1) + "/" + layerName));
                layer_offset.Add(0);
            }
        }

        /// <summary>
        /// Updates the background position
        /// </summary>
        /// <param name="gameTime">The Gametime object of that step</param>
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < layers[ID].Length - 1;i++)
            {
                layer_offset[i] -= (int)(gameTime.ElapsedGameTime.TotalMilliseconds / speed * (speedIncrese * (i + 1)));

                layer_offset[i] %= DestinationSize(layer_text[i]).X;
            }
        }

        /// <summary>
        /// Draws the Background on the surface provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < layer_offset.Count - 1; i++)
            {
                Point size = DestinationSize(layer_text[i]);
                Rectangle dest = new Rectangle(new Point(layer_offset[i], 0), size);
                Rectangle source = layer_text[i].Bounds;

                while (dest.X < Game.Graphics.GraphicsDevice.Viewport.Width)
                {
                    spriteBatch.Draw(layer_text[i], dest, source, Color.White);
                    dest.X += dest.Width;
                }
            }
        }

        private Point DestinationSize(Texture2D texture)
        {
            int windowHeight = Game.Graphics.GraphicsDevice.Viewport.Height;
            float heightRatio = (float)windowHeight / texture.Height;

            return new Point((int)(texture.Width * heightRatio), (int)(texture.Height * heightRatio));
        }

        public int Height
        {
            get
            {
                return DestinationSize(layer_text[0]).Y;
            }
        }
    }
}
