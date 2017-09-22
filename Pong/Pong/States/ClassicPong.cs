using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Pong.Core;

namespace Pong.States
{
    public class ClassicPong : GameState
    {
        private GameObject.GameObjectManager objects;
        private Ball ball;
        private Paddle paddle0, paddle1;
        private Song song;
        private Colourizer colours;

        public ClassicPong() { }

        public void Load()
        {
            objects = new GameObject.GameObjectManager();
            //Construct all objects and add them
            ball = new Ball();
            paddle0 = new Paddle(0, Keys.W, Keys.S);
            paddle1 = new Paddle(1, Keys.Up, Keys.Down);
            colours = new Colourizer();
            objects.Add(ball);
            objects.Add(paddle0);
            objects.Add(paddle1);
            objects.Add(colours);
            //Init the manager before we play
            objects.Init();
            //load the music
            song = AssetManager.GetResource<Song>("music");
            PlaySong();
        }

        public void Unload()
        {
            objects.Clear();
            song.Dispose();
        }
        
        public void Update(GameTime gameTime)
        {
            objects.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice device)
        {
            device.Clear(Color.Black);
            spriteBatch.Begin();

            objects.Draw(gameTime, spriteBatch);

            Color uiLivesColour = colours.PeekNextColour();
            for (int i = 0; i < paddle0.Lives; i++)
                spriteBatch.Draw(ball.Sprite, new Vector2(i * ball.Sprite.Width, 0), uiLivesColour);
            for (int i = 0; i < paddle1.Lives; i++)
                spriteBatch.Draw(ball.Sprite, new Vector2(Grid.ScreenSize.X - (i + 1) * ball.Sprite.Width, 0), uiLivesColour);

            spriteBatch.End();
        }
        
        //method to play the music
        public void PlaySong()
        {
            MediaPlayer.Play(song);
            MediaPlayer.Volume = 0.2f;
        }
    }
}