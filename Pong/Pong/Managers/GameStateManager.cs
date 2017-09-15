using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Managers
{
    public interface GameState
    {
        void Init();
        void Update(GameTime time);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }

    public class GameStateManager
    {
        private Dictionary<string, GameState> states;
        private GameState currentstate;

        public GameStateManager()
        {
            states = new Dictionary<string, GameState>();
            currentstate = null;
        }

        public void Update(GameTime time)
        {
            if (currentstate == null) return;
            currentstate.Update(time);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (currentstate == null) return;
            currentstate.Draw(gameTime, spriteBatch);
        }

        public void AddState(string name, GameState state)
        {
            if (state == null) return;
            states.Add(name, state);
            states[name].Init();
        }

        public void RemoveState(string name)
        {
            if (states.ContainsKey(name))
                states.Remove(name);
        }

        public void SetActiveState(string name)
        {
            if (states.ContainsKey(name))
                currentstate = states[name];
        }
    }
}