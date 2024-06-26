﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//Simpele GameStateManager, spreekt voorzich.
namespace Pong.Core
{
    public interface GameState
    {
        void Load();
        void Unload();
        void Update(GameTime time);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice device);
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
        private static GameStateManager instance;

        public GameStateManager()
        {
            if (instance != null) return;
            states = new Dictionary<string, GameState>();
            currentstate = null;
            instance = this;
        }

        private void SetState(string name)
        {
            if (states.ContainsKey(name))
                currentstate = states[name];
        }

        public static void RequestChange(GameStateChange change)
        {
            if (change == null) return;
            if (change.type == CHANGETYPE.LOAD) instance.currentstate.Unload();
            instance.SetState(change.newstate);
            if (change.type == CHANGETYPE.LOAD) instance.currentstate.Load();
        }

        public void Update(GameTime time)
        {
            Input.Update();
            if (currentstate == null) return;
            currentstate.Update(time);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice device)
        {
            if (currentstate == null) return;
            currentstate.Draw(gameTime, spriteBatch, device);
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

        public void SetStartingState(string name)
        {
            if (currentstate != null) return;
            SetState(name);
        }
    }
}