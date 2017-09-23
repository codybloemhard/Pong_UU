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
        //private Text text;
        private Button button;
        private string message = ""; 

        public GameOverMenu()
        {
        }

        public void Load()
        {
            message = "Player " + DataManager.GetData<int>("loser") + " lost! Press [SPACE] to restart.";
            //text = new Text(message, Vector2.Zero, Grid.ScreenSize);
            //text.colour = Color.White;
            mainFont = AssetManager.GetResource<SpriteFont>("mainFont");
            button = new Button("Press to play!", "paddle", () => { loadGame(); }, new Vector2(1,1), new Vector2(3, 1));
            button.SetupColours(new Color(64,64,64), new Color(96, 96, 96), new Color(32, 32, 32), Color.Red);
        }

        private void loadGame()
        {
            GameStateManager.RequestChange(new GameStateChange("classic", CHANGETYPE.LOAD));
        }

        public void Unload()
        {
            
        }
        
        public void Update(GameTime time)
        {
            //if (Keyboard.GetState().IsKeyDown(Keys.Space))
            //GameStateManager.RequestChange(new GameStateChange("classic", CHANGETYPE.LOAD));
            button.Update();
        }

        public void Draw(GameTime time, SpriteBatch batch, GraphicsDevice device)
        {
            device.Clear(Color.Black);
            batch.Begin();
            //text.Draw(batch, mainFont);
            button.Draw(batch, mainFont);
            batch.End();
        }
    }
}