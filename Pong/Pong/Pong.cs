using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong
{
    
    class Pong : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        static Vector2 screenSize;
        static Random random = new Random();
        static GameWorld gameWorld;

        public Pong()
        {
            graphics = new GraphicsDeviceManager(this);
            screenSize = new Vector2(900F, 600F);
            graphics.PreferredBackBufferWidth = (int) screenSize.X;
            graphics.PreferredBackBufferHeight = (int) screenSize.Y;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameWorld = new GameWorld(Content);
        }
        
        protected override void UnloadContent()
        {
            
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gameWorld.Update(gameTime);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(0, 0, 50));

            gameWorld.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }

        public static GameWorld GameWorld
        {
            get { return gameWorld; }
        }

        public static Random Random
        {
            get { return random; }
        }

        public static Vector2 ScreenSize
        {
            get { return screenSize; }
        }

    }
}
