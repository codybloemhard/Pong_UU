using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Pong.Managers;
using Pong.Core;

namespace Pong.States
{
    public class GameOverMenu : GameState
    {
        private SpriteFont mainFont;
        private ContentManager content;
        private GameStateChange needstate = null;
        private string message = ""; 

        public GameOverMenu(ContentManager content)
        {
            this.content = content;
        }

        public void Load()
        {
            needstate = null;
            message = "Player " + DataManager.GetInt("loser") + " lost! Press [SPACE] to restart.";
            mainFont = content.Load<SpriteFont>("mainFont");
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