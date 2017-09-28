using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Pong.Core;

namespace Pong
{
    public class Ball : GameObject
    {
        private SoundEffect effect;
        private Vector2 speed;
        private Colourizer colours;

        public Ball()
        {
            tag = "ball";
            sprite = AssetManager.GetResource<Texture2D>("ball");
            effect = AssetManager.GetResource<SoundEffect>("wallbounce");
            Size = new Vector2(0.25f, 0.25f);
        }

        public override void Init()
        {
            Pos = new Vector2((Grid.GridSize.X - Size.X) / 2f, (Grid.GridSize.Y - Size.Y) / 2f);
            float xspeed = Pong.Random.NextDouble() <= 0.5f ? 1f : -1f;
            float yspeed = (float)Pong.Random.NextDouble() * 2 - 1;
            speed = new Vector2(xspeed, yspeed);
            speed.Normalize();
            speed *= 0.1f;
            if (colours == null)
                colours = FindWithTag("colourizer") as Colourizer;
            colours.LiveOff();
            colour = colours.Colour;
        }

        public override void Update(GameTime gameTime)
        {
            float dx, dy;

            dx = Pos.X + speed.X;
            dy = Pos.Y + speed.Y;

            if (dy + Size.Y > Grid.GridSize.Y)
            {
                dy = Grid.GridSize.Y - Size.Y;
                speed.Y *= -1;
                effect.Play(0.2f, 0.0f, 0.0f);
            }
            if (dy < 0)
            {
                dy = 0;
                speed.Y *= -1;
                effect.Play(0.2f, 0.0f, 0.0f);
            }

            Pos = new Vector2(dx, Pos.Y);
            Pos = new Vector2(Pos.X, dy);
        }

        public override void Draw(GameTime time, SpriteBatch batch)
        {
            base.Draw(time, batch);
        }

        public Vector2 Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Texture2D Sprite
        {
            get { return sprite; }
        }
    }
}