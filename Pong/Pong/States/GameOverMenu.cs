using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Pong.Core;

namespace Pong.States
{
    public class GameOverMenu : GameState
    {
        private SpriteFont mainFont;
        private string message = ""; 

        public GameOverMenu()
        {
        }

        public void Load()
        {
            message = "Player " + DataManager.GetData<int>("loser") + " lost! Press [SPACE] to restart.";
            mainFont = AssetManager.GetResource<SpriteFont>("mainFont");
        }

        public void Unload()
        {
            
        }
        
        public void Update(GameTime time)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                GameStateManager.RequestChange(new GameStateChange("classic", CHANGETYPE.LOAD));
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice device)
        {
            device.Clear(Color.Black);
            spriteBatch.Begin();
            UI.WriteCenter(spriteBatch, message, mainFont, Color.White);
            spriteBatch.End();
        }
    }
}