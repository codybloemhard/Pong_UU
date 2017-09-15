using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Pong
{
    class Paddle : Entity
    {

        float speed;
        int player, lives;
        Keys keyUp, keyDown;
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
                    this.pos = new Vector2(0, (Pong.ScreenSize.Y - sprite.Height) / 2);
                    break;
                case 2:
                    this.sprite = Content.Load<Texture2D>("paddle2");
                    this.pos = new Vector2((Pong.ScreenSize.X - sprite.Width), (Pong.ScreenSize.Y - sprite.Height) / 2);
                    break;
                default:
                    break;
            }
            this.keyUp = keyUp;
            this.keyDown = keyDown;
            this.speed = 5.0F;
            this.lives = 3;
        }

        //
        private void LoseLife()
        {
            lives -= 1;
            GameWorld.Ball.Reset();
            effect2.Play(0.2f, 0.0f, 0.0f);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(keyUp) && pos.Y > 0)
                pos.Y -= speed;

            if (Keyboard.GetState().IsKeyDown(keyDown) && pos.Y < Pong.ScreenSize.Y - sprite.Height)
                pos.Y += speed;

            if (Collides(GameWorld.Ball))
            {
                GameWorld.Ball.Speed = new Vector2(GameWorld.Ball.Speed.X * -1.05F, ((GameWorld.Ball.Pos.Y + GameWorld.Ball.GetBounds().Height / 2) - (pos.Y + sprite.Height / 2)) / 10);
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
                    if (GameWorld.Ball.Pos.X > pos.X + sprite.Width)
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
            spriteBatch.Draw(sprite, pos, Color.White);

        }

        public int Lives
        {
            get { return lives; }
        }

    }
}
