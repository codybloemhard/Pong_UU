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
        private Text text;
        private Button replayButton, menuButton;
        private string message = ""; 

        public GameOverMenu() { }

        public void Load()
        {
            message = "Player " + DataManager.GetData<int>("loser") + " lost!";
            text = new Text(message, Vector2.Zero, Grid.ScreenSize);
            text.colour = Color.White;
            mainFont = AssetManager.GetResource<SpriteFont>("mainFont");
            replayButton = new Button("Play again!", "button", () => { loadGame(); }, new Vector2(2,5), new Vector2(4, 2));
            replayButton.SetupColours(new Color(64, 64, 64), new Color(96, 96, 96), new Color(32, 32, 32), Color.Red);
            menuButton = new Button("Go to menu!", "button", () => { loadMenu(); }, new Vector2(10, 5), new Vector2(4, 2));
            menuButton.SetupColours(new Color(64, 64, 64), new Color(96, 96, 96), new Color(32, 32, 32), Color.Red);
        }

        private void loadGame()
        {
            GameStateManager.RequestChange(new GameStateChange("classic", CHANGETYPE.LOAD));
        }
        private void loadMenu()
        {
            GameStateManager.RequestChange(new GameStateChange("menu", CHANGETYPE.LOAD));
        }

        public void Unload()
        {
            
        }
        
        public void Update(GameTime time)
        {
            replayButton.Update();
            menuButton.Update();
        }

        public void Draw(GameTime time, SpriteBatch batch, GraphicsDevice device)
        {
            device.Clear(Color.Black);
            batch.Begin();
            text.Draw(batch, mainFont);
            replayButton.Draw(batch, mainFont);
            menuButton.Draw(batch, mainFont);
            batch.End();
        }
    }
}