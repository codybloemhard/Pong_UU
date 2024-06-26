﻿using System;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Pong.Core;
using Pong.States;
//Een NPC die optimaal speelt.
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
        private int score;

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
            score = 0;
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
            /*Als receiving == true dan komt de bal op de autopaddle af
            Dan berekenen waar de paddle heen moet gaan. Als de bal richting
            de speler gaat dan gaat de autopaddle naar het midden van het scherm.
            */
            bool recieving = ball.Speed.X > 0;
            if (!recieving)
            {
                float middle = (Grid.GridSize.Y / 2.0f) - (Size.Y / 2.0f);
                if (Pos.Y < middle)
                    Pos += Vector2.UnitY * MathHelper.Clamp(middle - Pos.Y, 0, speed);
                else
                    Pos -= Vector2.UnitY * MathHelper.Clamp(Pos.Y - middle, 0, speed);
                calculated = false;
            }
            else
            {
                if (!calculated)
                {
                    futureY = CalcY(ball.Speed, ball.Pos, 0);
                    futureY -= Size.Y / 2.0f;
                    calculated = true;
                }
                if (Pos.Y < futureY)
                    Pos += Vector2.UnitY * MathHelper.Clamp(futureY - Pos.Y, 0, speed);
                else
                    Pos -= Vector2.UnitY * MathHelper.Clamp(Pos.Y - futureY, 0, speed);
                Pos = new Vector2(Pos.X, MathHelper.Clamp(Pos.Y, 0, Grid.GridSize.Y - Size.Y));
            }
        }
        /*hier rekenen we uit waar de bal terecht gaat komen,
        inclusief reflecties. De autoPaddle speelt het spel dus
        optimaal :)*/
        private float CalcY(Vector2 bdir, Vector2 bpos, int i)
        {
            if (i > 20) return 0.0f;//tegen stackoverflows
            bdir.Normalize();
            float mult = 1.0f / bdir.X;
            bdir *= mult;
            bool up = bdir.Y < 0;
            float m = bdir.Y;
            float xtg = Grid.GridSize.X - bpos.X;
            float ytg = 0;
            if (up)
                ytg = bpos.Y;
            else
                ytg = Grid.GridSize.Y - bpos.Y;
            if (ytg / Math.Abs(m) > xtg)
            {
                return (xtg *m) + bpos.Y;
            }
            float newpx, newpy;
            if (up)
            {
                newpy = 0.0f;
                newpx = bpos.X + (ytg / -bdir.Y);
            }
            else
            {
                newpy = Grid.GridSize.Y;
                newpx = bpos.X + (ytg / bdir.Y);
            }
            Vector2 newbdir = bdir;
            newbdir.Y *= -1;
            Vector2 newbpos = new Vector2(newpx, newpy);
            return CalcY(newbdir, newbpos, i + 1);
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
                score++;
                DataManager.SetData<int>("score", score);
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