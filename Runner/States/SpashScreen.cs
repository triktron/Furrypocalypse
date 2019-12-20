using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Runner.States
{
    class SpashScreen
    {
        Texture2D Texture;
        int _autoSkipTime = 5;

        public void Load(ContentManager content)
        {
            Texture = content.Load<Texture2D>("Graphics/splashscreen");
        }

        public void Update(GameTime gameTime)
        {
            if (IsTimePassed(gameTime) || IsAnyKeyPressed())
            {
                MainMenu.OpenMenu();
            }
        }

        private bool IsTimePassed(GameTime gameTime)
        {
            return gameTime.TotalGameTime.TotalSeconds > _autoSkipTime;
        }

        private bool IsAnyKeyPressed()
        {
            return Keyboard.GetState().GetPressedKeys().Length > 0;
        }

        public void Draw(SpriteBatch spriteBatch)   
        {
            int halfScreenHeight = Game.Graphics.GraphicsDevice.Viewport.Height / 2;
            int halfScreenWidth = Game.Graphics.GraphicsDevice.Viewport.Width / 2;

            int splaschscreenHelfHeight = Texture.Height / 2;
            int splaschscreenHelfWidth = Texture.Width / 2;

            Vector2 pos = new Vector2(halfScreenWidth - splaschscreenHelfWidth, halfScreenHeight - splaschscreenHelfHeight);

            spriteBatch.Draw(Texture, pos, Color.White);
        }
    }
}
