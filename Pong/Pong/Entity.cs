using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public abstract class Entity
    {
        private bool dirtybounds = true;
        private Vector2 pos;
        protected Texture2D sprite;
        protected Rectangle bounds;

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        //GetBounds creates a rectangle that matches the dimensions of the drawn sprite
        /*System with a diryflag ensures we do not have to calculate new bounds
        Everytime we either check for collision or updadate our position.*/
        public Rectangle GetBounds()
        {
            if (!dirtybounds) return bounds;
            bounds.X = (int)pos.X;
            bounds.Y = (int)pos.Y;
            bounds.Width = sprite.Width;
            bounds.Height = sprite.Height;
            dirtybounds = false;
            return bounds;
        }

        //this method uses the rectangle created in GetBounds to check if two sprites collide
        public bool Collides(Entity e)
        {
            if (GetBounds().Intersects(e.GetBounds())) return true;
            return false;
        }

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value;  dirtybounds = true; }
        }
    }
}