using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Pong
{
    public class Ball : Entity
    {
        private SoundEffect effect;
        private Vector2 speed;

        public Ball(ContentManager Content)
        {
            sprite = Content.Load<Texture2D>("ball");
            effect = Content.Load<SoundEffect>("wallbounce");
            Reset();
        }

        public void Reset()
        {
            Pos = new Vector2((Pong.ScreenSize.X - sprite.Width) / 2f, (Pong.ScreenSize.Y - sprite.Height) / 2f);
            float xspeed = Pong.Random.NextDouble() <= 0.5f ? 4f : -4f;
            speed = new Vector2( xspeed, (float)(Pong.Random.NextDouble() * 7));
        }

        public override void Update(GameTime gameTime)
        {
            float dx, dy;

            dx = Pos.X + speed.X;
            dy = Pos.Y + speed.Y;

            if (dy + sprite.Height > Pong.ScreenSize.Y)
            {
                dy = Pong.ScreenSize.Y - sprite.Height;
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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Pos, Color.White);
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