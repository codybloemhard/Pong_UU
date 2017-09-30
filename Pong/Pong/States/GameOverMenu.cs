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
        //Init alle UI elementen, gebruik lambda's voor de Actions.
        public void Load()
        {
            message = "Player " + DataManager.GetData<int>("loser") + " lost!";
            if (DataManager.GetData<MODE>("mode") == MODE.ai)
            {
                if (DataManager.GetData<int>("loser") == 1)
                    message = "I won :) You survived " + DataManager.GetData<int>("score") + " attacks!";
                else
                    message = "Yo ..u.. Be [ERROR]a t.  . [ERROR] m  e  [ERROR]";
            }
            text = new Text(message, Vector2.Zero, new Vector2(16, 5));
            text.colour = Color.White;
            mainFont = AssetManager.GetResource<SpriteFont>("mainFont");
            replayButton = new Button("Play again!", "button", () => { loadGame(); }, new Vector2(2,5), new Vector2(4, 2));
            replayButton.SetupColours(Color.Yellow, new Color(96, 96, 96), new Color(32, 32, 32), Color.Red);
            menuButton = new Button("Go to menu!", "button", () => { loadMenu(); }, new Vector2(10, 5), new Vector2(4, 2));
            menuButton.SetupColours(Color.Purple, new Color(96, 96, 96), new Color(32, 32, 32), Color.Red);
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
            replayButton.Draw(batch, mainFont);
            menuButton.Draw(batch, mainFont);
            text.Draw(batch, mainFont);
            batch.End();
        }
    }
}