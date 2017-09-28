using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
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
        private Ball ball, extraBall;
        private Colourizer colours;
        private MODE mode;

        public Paddle(int player, Keys keyUp, Keys keyDown)
        {
            tag = "paddle";
            mode = DataManager.GetData<MODE>("mode");
            effect1 = AssetManager.GetResource<SoundEffect>("paddlebounce");
            effect2 = AssetManager.GetResource<SoundEffect>("minuslife");
            sprite = AssetManager.GetResource<Texture2D>("paddle");
            Size = new Vector2(0.2f, 1.5f);
            this.player = player;
            //these 2 cases create the two different paddles on screen
            if (player == 0)
                Pos = new Vector2(0, (Grid.GridSize.Y - Size.Y) / 2f);
            else if(player == 1)
                Pos = new Vector2((Grid.GridSize.X - Size.X), (Grid.GridSize.Y - Size.Y) / 2f);
            this.keyUp = keyUp;
            this.keyDown = keyDown;
            speed = 0.15f;
            if (mode == MODE.classic) lives = 3;
            else if (mode == MODE.multiball) lives = 10;
        }

        public override void Init()
        {
            ball = FindWithTag("ball") as Ball;
            colours = FindWithTag("colourizer") as Colourizer;
            colour = colours.Colour;
            if (mode == MODE.multiball)
                extraBall = FindWithTag("extraball") as Ball;
        }

        private void LoseLife()
        {
            lives--;
            effect2.Play(0.2f, 0.0f, 0.0f);
            if(lives <= 0)
            {
                MediaPlayer.Stop();
                DataManager.SetData<int>("loser", player + 1);
                GameStateManager.RequestChange(new GameStateChange("gameover", CHANGETYPE.LOAD));
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(keyUp) && Pos.Y > 0)
                Pos -= Vector2.UnitY * speed;
            if (Keyboard.GetState().IsKeyDown(keyDown) && Pos.Y < Grid.GridSize.Y - Size.Y)
                Pos += Vector2.UnitY * speed;

            HandleBall(ball);
            if (mode == MODE.multiball)
                HandleBall(extraBall);
        }

        private void HandleBall(Ball b)
        {
            if (Collides(b))
            {
                b.Speed = new Vector2(b.Speed.X * -1.05f, b.Speed.Y * 1.05f);
                effect1.Play(0.2f, 0.0f, 0.0f);
                colours.NextColour();
            }
            bool loselife = false;
            if (player == 0 && b.Pos.X + b.GetBounds().w < 0)
                loselife = true;
            else if (player == 1 && b.Pos.X > Pos.X + Size.X)
                loselife = true;
            if (loselife)
            {
                LoseLife();
                b.Init();
                b.Colour = colour;
            }
        }

        public override void Draw(GameTime time, SpriteBatch batch)
        {
            base.Draw(time, batch);
        }

        public int Lives { get { return lives; } }
    }
}