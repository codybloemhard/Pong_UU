using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pong.Core;

namespace Pong
{
    public class Powerups : GameObject
    {
        private SoundEffect posPickup;
        private bool powerupOnScreen;

        public Powerups(ContentManager Content)
        {
            sprite = Content.Load<Texture2D>("Powerup");
            posPickup = Content.Load<SoundEffect>("Pickup");
            Spawn();
        }

        public override void Init()
        {
            
        }

        public void Spawn()
        {
            double y = Pong.Random.NextDouble();
            float spawnDeviation = (float)y;
            Pos = new Vector2((Grid.ScreenSize.X - sprite.Width) / 2, (Grid.ScreenSize.Y - sprite.Height) / 2 * spawnDeviation);
            powerupOnScreen = true;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Pos, Color.White);
        }
    }
}