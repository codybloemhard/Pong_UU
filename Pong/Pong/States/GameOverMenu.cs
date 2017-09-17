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
        private GameStateChange needstate = null;
        private string message = ""; 

        public GameOverMenu()
        {
        }

        public void Load()
        {
            needstate = null;
            message = "Player " + DataManager.GetData<int>("loser") + " lost! Press [SPACE] to restart.";
            mainFont = AssetManager.GetResource<SpriteFont>("mainFont");
        }

        public void Unload()
        {
            
        }

        public GameStateChange RequestStateChange()
        {
            return needstate;
        }

        public void Update(GameTime time)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                needstate = new GameStateChange("classic", CHANGETYPE.LOAD);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            UI.WriteCenter(spriteBatch, message, mainFont, Color.White);
            spriteBatch.End();
        }
    }
}