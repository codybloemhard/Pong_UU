using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Pong.Core;
using Pong.States;

namespace Pong
{
    public class Pong : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameStateManager gamestates;

        private static Vector2 screenSize;
        private static Random random = new Random();

        public Pong()
        {
            graphics = new GraphicsDeviceManager(this);
            screenSize = new Vector2(900, 600);
            graphics.PreferredBackBufferWidth = (int)screenSize.X;
            graphics.PreferredBackBufferHeight = (int)screenSize.Y;
            Content.RootDirectory = "Content";
            AssetManager.content = Content;
            gamestates = new GameStateManager();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ClassicPong gameWorld = new ClassicPong();
            GameOverMenu gameoverMenu = new GameOverMenu();
            gamestates.AddState("classic", gameWorld);
            gamestates.AddState("gameover", gameoverMenu);
            gamestates.SetStartingState("gameover");
        }
        
        protected override void UnloadContent() { }
        
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
                return;
            }
            gamestates.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            gamestates.Draw(gameTime, spriteBatch, GraphicsDevice);
            base.Draw(gameTime);
        }
        
        public static Random Random { get { return random; } }
        public static Vector2 ScreenSize { get { return screenSize; } }
    }
}