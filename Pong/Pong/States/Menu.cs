using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Pong.Core;

namespace Pong.States
{
    public enum MODE
    {
        classic,
        multiball,
        ai
    }
    public class Menu : GameState
    {
        private SpriteFont mainFont, titleFont;
        private Text text;
        private Button classicButton, multiballButton, aiButton;
        private string message = "";

        public Menu() { }

        public void Load()
        {
            message = "ZIEK HARDE PONG BOYZ";
            text = new Text(message, Vector2.Zero, new Vector2(16, 4));
            text.colour = Color.White;
            mainFont = AssetManager.GetResource<SpriteFont>("mainFont");
            titleFont = AssetManager.GetResource<SpriteFont>("menuFont");
            classicButton = new Button("Classic Pong", "button", () => { loadClassic(); }, new Vector2(1, 4), new Vector2(4, 3));
            classicButton.SetupColours(Color.Red, new Color(96, 96, 96), new Color(32, 32, 32), Color.Red);
            multiballButton = new Button("Multiball Pong", "button", () => { loadMultiball(); }, new Vector2(6, 4), new Vector2(4, 3));
            multiballButton.SetupColours(new Color(0, 255, 0), new Color(96, 96, 96), new Color(32, 32, 32), Color.Red);
            aiButton = new Button("vs AI Pong", "button", () => { loadAI(); }, new Vector2(11, 4), new Vector2(4, 3));
            aiButton.SetupColours(Color.Blue, new Color(96, 96, 96), new Color(32, 32, 32), Color.Red);
        }

        private void loadClassic()
        {
            DataManager.SetData<MODE>("mode", MODE.classic);
            GameStateManager.RequestChange(new GameStateChange("classic", CHANGETYPE.LOAD));
        }
        private void loadMultiball()
        {
            DataManager.SetData<MODE>("mode", MODE.multiball);
            GameStateManager.RequestChange(new GameStateChange("classic", CHANGETYPE.LOAD));
        }
        private void loadAI()
        {
            DataManager.SetData<MODE>("mode", MODE.ai);
            GameStateManager.RequestChange(new GameStateChange("ai", CHANGETYPE.LOAD));
        }

        public void Unload()
        {

        }

        public void Update(GameTime time)
        {
            classicButton.Update();
            multiballButton.Update();
            aiButton.Update();
        }

        public void Draw(GameTime time, SpriteBatch batch, GraphicsDevice device)
        {
            device.Clear(Color.Black);
            batch.Begin();
            text.Draw(batch, titleFont);
            classicButton.Draw(batch, mainFont);
            multiballButton.Draw(batch, mainFont);
            aiButton.Draw(batch, mainFont);
            batch.End();
        }
    }
}