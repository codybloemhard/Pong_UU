using System;
using System.Timers;
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
    public class AutoPaddle : GameObject
    {
        private float speed;
        private int player, lives;
        private SoundEffect effect1, effect2;
        private Ball ball, extraBall;
        private Colourizer colours;
        private MODE mode;
        private bool calculated = false;
        private float futureY = 0.0f;

        public AutoPaddle(int player)
        {
            tag = "autopaddle";
            mode = DataManager.GetData<MODE>("mode");
            effect1 = AssetManager.GetResource<SoundEffect>("paddlebounce");
            effect2 = AssetManager.GetResource<SoundEffect>("minuslife");
            sprite = AssetManager.GetResource<Texture2D>("paddle");
            Size = new Vector2(0.2f, 1.5f);
            this.player = player;
            //these 2 cases create the two different paddles on screen
            if (player == 0)
                Pos = new Vector2(0, (Grid.GridSize.Y - Size.Y) / 2f);
            else if (player == 1)
                Pos = new Vector2((Grid.GridSize.X - Size.X), (Grid.GridSize.Y - Size.Y) / 2f);
            speed = 0.15f;
            if (mode != MODE.multiball) lives = 3;
            else lives = 10;
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
            if (lives <= 0)
            {
                MediaPlayer.Stop();
                DataManager.SetData<int>("loser", player + 1);
                GameStateManager.RequestChange(new GameStateChange("gameover", CHANGETYPE.LOAD));
            }
        }

        public override void Update(GameTime gameTime)
        {
            HandleBall(ball);
            if (mode == MODE.multiball)
            {
                HandleBall(extraBall);
            }

            bool recieving = ball.Speed.X > 0;
            if (!recieving)
            {
                float deltay = (Grid.GridSize.Y / 2) - Pos.X + Size.X;
                if(deltay > 0)
                    deltay = MathHelper.Clamp(deltay, 0, speed);
                else
                    deltay = MathHelper.Clamp(deltay, -speed, 0);
                Pos += Vector2.UnitY * deltay;
                calculated = false;
            }
            else
            {
                if (!calculated)
                {
                    futureY = CalcY(ball.Speed, ball.Pos);
                    Console.WriteLine(futureY);
                    calculated = true;
                }
                if (Pos.Y < futureY)
                    Pos += Vector2.UnitY * MathHelper.Clamp(futureY - Pos.Y, 0, speed);
                else
                    Pos -= Vector2.UnitY * MathHelper.Clamp(Pos.Y - futureY, 0, speed);
            }
        }

        private float CalcY(Vector2 bdir, Vector2 bpos)
        {
            bdir.Normalize();
            //float mult = 1.0f / bdir.X;
            //bdir *= mult;
            float xtogo = Grid.GridSize.X - bpos.X;
            float ytogo;
            if (bdir.Y > 0)
                ytogo = Grid.GridSize.Y - bpos.Y;
            else
                ytogo = bpos.Y;
            if (xtogo < ytogo)
                return xtogo * bdir.Y;
            Vector2 newbdir = bdir;
            newbdir.Y *= -1;
            float newby;
            if (bdir.Y > 0)
                newby = Grid.GridSize.Y;
            else
                newby = 0;
            Vector2 newbpos = new Vector2(ytogo * bdir.X, newby);
            return CalcY(newbdir, newbpos);
        }

        private void HandleBall(Ball b)
        {
            if (Collides(b))
            {
                int xdir = b.Speed.X > 0 ? -1 : 1;
                //resolve collision
                b.Pos = new Vector2(Pos.X + (Size.X * xdir) + (0.05f * xdir), b.Pos.Y);
                /*stuur de bal met een hoek weg die afhangt van de plek waar de bal 
                raakt ten opzichte van de surface-normal.
                */
                float ballSpeed = b.Speed.Length();
                float my = Pos.Y + Size.Y / 2.0f;
                float myb = b.Pos.Y + b.Size.Y / 2.0f;
                float bouncePaddleDeviation = myb - my;
                float rotationDegrees = (bouncePaddleDeviation / (this.Size.Y / 2)) * 50;
                float y = ((float)Math.Tan(MathHelper.ToRadians(rotationDegrees)));
                Vector2 rotationVector = new Vector2(xdir, y);
                rotationVector.Normalize();
                rotationVector *= ballSpeed * 1.05f;
                b.Speed = rotationVector;

                effect1.Play(0.2f, 0.0f, 0.0f);
                colours.NextColour();
            }
            //als de bal buiten het scherm is, reset etc.
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