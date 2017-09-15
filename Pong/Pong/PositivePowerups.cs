using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    class Powerups : Entity
    {
        private SoundEffect posPickup;
        private bool powerupOnScreen;

        public Powerups(ContentManager Content)
        {
            this.sprite = Content.Load<Texture2D>("Powerup");
            Spawn();
            posPickup = Content.Load<SoundEffect>("Pickup");
        }

        public void Spawn()
        {
            double y = Pong.Random.NextDouble();
            float spawnDeviation = (float)y;
            pos = new Vector2((Pong.ScreenSize.X - sprite.Width) / 2, (Pong.ScreenSize.Y - sprite.Height) / 2 * spawnDeviation);
            powerupOnScreen = true;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, pos, Color.White);
        }
    }
}
