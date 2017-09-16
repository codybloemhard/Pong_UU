using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Managers
{
    public interface GameState
    {
        void Load();
        void Unload();
        void Update(GameTime time);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        GameStateChange RequestStateChange();
    }
    
    public enum CHANGETYPE
    {
        LOAD,
        SWITCH
    }

    public class GameStateChange
    {
        public string newstate;
        public CHANGETYPE type;
        public GameStateChange(string s, CHANGETYPE t)
        {
            newstate = s;
            type = t;
        }
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

        private void PollRequest()
        {
            GameStateChange change = currentstate.RequestStateChange();
            if (change != null)
            {
                if (change.type == CHANGETYPE.LOAD) currentstate.Unload();
                SetActiveState(change.newstate);
                if (change.type == CHANGETYPE.LOAD) currentstate.Load();
            }
        }

        public void Update(GameTime time)
        {
            if (currentstate == null) return;
            PollRequest();
            currentstate.Update(time);
            PollRequest();
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
            states[name].Load();
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