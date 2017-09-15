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
    class Ball : Entity
    {
        private SoundEffect effect1;
        private Vector2 speed;

        public Ball(ContentManager Content, Vector2 speed)
        {
            this.sprite = Content.Load<Texture2D>("ball");
            this.speed = speed;
            Reset();            
            effect1 = Content.Load<SoundEffect>("wallbounce");
        }

        public void Reset()
        {
            pos = new Vector2((Pong.ScreenSize.X - sprite.Width) / 2, (Pong.ScreenSize.Y - sprite.Height) / 2);
            float xspeed = Pong.Random.NextDouble() <= 0.5D ? 4F : -4F;
            speed = new Vector2( xspeed, (float)(Pong.Random.NextDouble() * 7));
        }

        public override void Update(GameTime gameTime)
        {
            float dx, dy;

            dx = pos.X + speed.X;
            dy = pos.Y + speed.Y;

            if (dy + sprite.Height > Pong.ScreenSize.Y)
            {
                dy = Pong.ScreenSize.Y - sprite.Height;
                speed.Y *= -1;
                effect1.Play(0.2f, 0.0f, 0.0f);
            }

            if (dy < 0)
            {
                dy = 0;
                speed.Y *= -1;
                effect1.Play(0.2f, 0.0f, 0.0f);
            }

            pos.X = dx;
            pos.Y = dy;
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, pos, Color.White);
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
