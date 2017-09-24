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
        extraplayer,
        ai
    }
    public class Menu : GameState
    {
        private SpriteFont mainFont;
        private Text text;
        private Button classicButton, multiballButton, extraplayerButton, aiButton;
        private string message = "";

        public Menu() { }

        public void Load()
        {
            message = "ZIEK HARDE PONG BOYZ";
            text = new Text(message, Vector2.Zero, new Vector2(16, 3));
            text.colour = Color.White;
            mainFont = AssetManager.GetResource<SpriteFont>("mainFont");
            classicButton = new Button("Classic Pong", "button", () => { loadClassic(); }, new Vector2(0, 3), new Vector2(8, 3));
            classicButton.SetupColours(new Color(64, 64, 64), new Color(96, 96, 96), new Color(32, 32, 32), Color.Red);
            multiballButton = new Button("Multiball Pong", "button", () => { loadMultiball(); }, new Vector2(8, 3), new Vector2(8, 3));
            multiballButton.SetupColours(new Color(64, 64, 64), new Color(96, 96, 96), new Color(32, 32, 32), Color.Red);
            extraplayerButton = new Button("Extra players Pong", "button", () => { loadExtra(); }, new Vector2(0, 6), new Vector2(8, 3));
            extraplayerButton.SetupColours(new Color(64, 64, 64), new Color(96, 96, 96), new Color(32, 32, 32), Color.Red);
            aiButton = new Button("vs AI Pong", "button", () => { loadAI(); }, new Vector2(8, 6), new Vector2(8, 3));
            aiButton.SetupColours(new Color(64, 64, 64), new Color(96, 96, 96), new Color(32, 32, 32), Color.Red);
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
        private void loadExtra()
        {
            DataManager.SetData<MODE>("mode", MODE.extraplayer);
            GameStateManager.RequestChange(new GameStateChange("extra", CHANGETYPE.LOAD));
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
            extraplayerButton.Update();
            aiButton.Update();
        }

        public void Draw(GameTime time, SpriteBatch batch, GraphicsDevice device)
        {
            device.Clear(Color.Black);
            batch.Begin();
            text.Draw(batch, mainFont);
            classicButton.Draw(batch, mainFont);
            multiballButton.Draw(batch, mainFont);
            extraplayerButton.Draw(batch, mainFont);
            aiButton.Draw(batch, mainFont);
            batch.End();
        }
    }
}