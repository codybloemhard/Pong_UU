using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Core
{
    public abstract partial class GameObject
    {
        public partial class GameObjectManager { }

        private bool dirtybounds = true;
        private Vector2 pos;
        protected string tag = "";
        protected GameObjectManager manager;
        protected Texture2D sprite;
        protected Rectangle bounds;

        public GameObject() { }
        public GameObject(string tag)
        {
            this.tag = tag;
        }

        public abstract void Init();
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

        public GameObject FindWithTag(string tag)
        {
            return manager.FindWithTag(tag);
        }

        public GameObject[] FindAllWithTag(string tag)
        {
            return manager.FindAllWithTag(tag);
        }

        public GameObject[] FindAllWithTags(string[] tags)
        {
            return manager.FindAllWithTags(tags);
        }

        //this method uses the rectangle created in GetBounds to check if two sprites collide
        public bool Collides(GameObject e)
        {
            if (GetBounds().Intersects(e.GetBounds())) return true;
            return false;
        }

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value;  dirtybounds = true; }
        }

        public void UpdatePos(float x, float y)
        {
            pos.X = x;
            pos.Y = y;
            dirtybounds = true;
        }
    }
}