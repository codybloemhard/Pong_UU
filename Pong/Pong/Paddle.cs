using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Pong
{
    public class Paddle : Entity
    {
        private float speed;
        private int player, lives;
        private Keys keyUp, keyDown;
        private SoundEffect effect1, effect2;

        public Paddle(ContentManager Content, int player, Keys keyUp, Keys keyDown)
        {
            effect1 = Content.Load<SoundEffect>("paddlebounce");
            effect2 = Content.Load<SoundEffect>("minuslife");
            this.player = player;
            switch (player)
            {
                //these 2 cases create the two different paddles on screen
                case 1:
                    this.sprite = Content.Load<Texture2D>("paddle1");
                    this.Pos = new Vector2(0, (Pong.ScreenSize.Y - sprite.Height) / 2);
                    break;
                case 2:
                    this.sprite = Content.Load<Texture2D>("paddle2");
                    this.Pos = new Vector2((Pong.ScreenSize.X - sprite.Width), (Pong.ScreenSize.Y - sprite.Height) / 2);
                    break;
                default:
                    break;
            }
            this.keyUp = keyUp;
            this.keyDown = keyDown;
            speed = 5.0F;
            lives = 3;
        }

        private void LoseLife()
        {
            lives--;
            GameWorld.Ball.Reset();
            effect2.Play(0.2f, 0.0f, 0.0f);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(keyUp) && Pos.Y > 0)
                Pos -= Vector2.UnitY * speed;

            if (Keyboard.GetState().IsKeyDown(keyDown) && Pos.Y < Pong.ScreenSize.Y - sprite.Height)
                Pos += Vector2.UnitY * speed;

            if (Collides(GameWorld.Ball))
            {
                GameWorld.Ball.Speed = new Vector2(GameWorld.Ball.Speed.X * -1.05F, ((GameWorld.Ball.Pos.Y + GameWorld.Ball.GetBounds().Height / 2) - (Pos.Y + sprite.Height / 2)) / 10);
                effect1.Play(0.2f, 0.0f, 0.0f);
            }

            switch (player)
            {
                case 1:
                    if (GameWorld.Ball.Pos.X + GameWorld.Ball.GetBounds().Width < 0)
                    {
                        LoseLife();
                    }
                    break;
                case 2:
                    if (GameWorld.Ball.Pos.X > Pos.X + sprite.Width)
                    {
                        LoseLife();
                    }
                    break;
                default:
                    break;
            }
            
            if (lives <= 0)
            {
                GameWorld.GameOver(player);
            }              
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Pos, Color.White);
        }

        public int Lives { get { return lives; } }
    }
}