using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Pong.Managers;
using Pong.Core;

namespace Pong.States
{
    public class ClassicPong : GameState
    {
        private GameObject.GameObjectManager objects;
        private Ball ball;
        private Paddle paddle0, paddle1;
        private ContentManager content;
        private Song song;
        private GameStateChange needstate = null;

        public ClassicPong(ContentManager content)
        {
            this.content = content;
        }
        
        public void GameOver(int player)
        {
            MediaPlayer.Stop();
            DataManager.StoreInt("loser", player);
            needstate = new GameStateChange("gameover", CHANGETYPE.LOAD);
        }

        //method to play the music
        public void PlaySong()
        {
            MediaPlayer.Play(song);
            MediaPlayer.Volume = 0.2f;
        }

        public void Load()
        {
            needstate = null;
            objects = new GameObject.GameObjectManager();
            //Construct all objects and add them
            ball = new Ball(content);
            paddle0 = new Paddle(content, 0, Keys.W, Keys.S);
            paddle1 = new Paddle(content, 1, Keys.Up, Keys.Down);
            objects.Add(ball);
            objects.Add(paddle0);
            objects.Add(paddle1);
            //Init the manager before we play
            objects.Init();
            //load the music
            song = content.Load<Song>("music");
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
            if (paddle0.Lives <= 0) GameOver(1);
            if (paddle1.Lives <= 0) GameOver(2);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            objects.Draw(gameTime, spriteBatch);

            for (int i = 0; i < paddle0.Lives; i++)
                spriteBatch.Draw(ball.Sprite, new Vector2(i * ball.Sprite.Width, 0), Color.White);
            for (int i = 0; i < paddle1.Lives; i++)
                spriteBatch.Draw(ball.Sprite, new Vector2(Pong.ScreenSize.X - (i + 1) * ball.Sprite.Width, 0), Color.White);

            spriteBatch.End();
        }

        public GameStateChange RequestStateChange() { return needstate; }
    }
}