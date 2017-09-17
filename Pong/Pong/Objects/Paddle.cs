using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Pong.Core;
using Pong.States;

namespace Pong
{
    public class Paddle : GameObject
    {
        private float speed;
        private int player, lives;
        private Keys keyUp, keyDown;
        private SoundEffect effect1, effect2;
        private Ball ball;

        public Paddle(int player, Keys keyUp, Keys keyDown)
        {
            tag = "paddle";
            effect1 = AssetManager.GetResource<SoundEffect>("paddlebounce");
            effect2 = AssetManager.GetResource<SoundEffect>("minuslife");
            this.player = player;
            //these 2 cases create the two different paddles on screen
            if(player == 0)
            {
                sprite = AssetManager.GetResource<Texture2D>("paddle1");
                Pos = new Vector2(0, (Pong.ScreenSize.Y - sprite.Height) / 2);
            }
            else if(player == 1)
            {
                this.sprite = AssetManager.GetResource<Texture2D>("paddle2");
                this.Pos = new Vector2((Pong.ScreenSize.X - sprite.Width), (Pong.ScreenSize.Y - sprite.Height) / 2);
            }
            this.keyUp = keyUp;
            this.keyDown = keyDown;
            speed = 8.0f;
            lives = 3;
        }

        public override void Init()
        {
            ball = FindWithTag("ball") as Ball;
        }

        private void LoseLife()
        {
            lives--;
            ball.Init();
            effect2.Play(0.2f, 0.0f, 0.0f);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(keyUp) && Pos.Y > 0)
                Pos -= Vector2.UnitY * speed;

            if (Keyboard.GetState().IsKeyDown(keyDown) && Pos.Y < Pong.ScreenSize.Y - sprite.Height)
                Pos += Vector2.UnitY * speed;

            if (Collides(ball))
            {
                ball.Speed = new Vector2(ball.Speed.X * -1.05f, ball.Speed.Y * 1.05f);
                effect1.Play(0.2f, 0.0f, 0.0f);
            }

            if (player == 0 && ball.Pos.X + ball.GetBounds().Width < 0)
                LoseLife();
            else if (player == 1 && ball.Pos.X > Pos.X + sprite.Width)
                LoseLife();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Pos, Color.White);
        }

        public int Lives { get { return lives; } }
    }
}