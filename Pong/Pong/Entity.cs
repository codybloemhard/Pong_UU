using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    abstract class Entity
    {

        protected Vector2 pos;
        protected Texture2D sprite;

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        //GetBounds creates a rectangle that matches the dimensions of the drawn sprite
        public Rectangle GetBounds()
        {
            return new Rectangle((int) pos.X, (int) pos.Y, sprite.Width, sprite.Height);
        }

        //this method uses the rectangle created in GetBounds to check if two sprites collide
        public bool Collides(Entity e)
        {
            if (GetBounds().Intersects(e.GetBounds()))
            {
                return true;
            }

            return false;
        }

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

    }
}
