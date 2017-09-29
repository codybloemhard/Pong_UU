using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Pong.Core;

namespace Pong.States
{
    public class ClassicPong : GameState
    {
        private GameObject.GameObjectManager objects;
        private Ball ball, extraball;
        private Paddle paddle0, paddle1;
        private AutoPaddle autoPaddle;
        private Song song;
        private Colourizer colours;
        private UITextureElement[] livesUI;
        private int maxlives = 0;
        private ParticleEmitter particles;
        private MODE mode;

        public ClassicPong() { }

        public void Load()
        {
            objects = new GameObject.GameObjectManager();
            //Construct all objects and add them
            ball = new Ball();
            paddle0 = new Paddle(0, Keys.W, Keys.S);
            
            colours = new Colourizer();
            mode = DataManager.GetData<MODE>("mode");
            if (mode == MODE.multiball)
            {
                extraball = new Ball();
                extraball.tag = "extraball";
                objects.Add(extraball);
            }
            if(mode == MODE.ai)
            {
                autoPaddle = new AutoPaddle(1);
                objects.Add(autoPaddle);
            }
            else
            {
                paddle1 = new Paddle(1, Keys.Up, Keys.Down);
                objects.Add(paddle1);
            }
            objects.Add(ball);
            objects.Add(paddle0);
            objects.Add(colours);
            //Init the manager before we play
            objects.Init();
            //init ui elements
            maxlives = paddle0.Lives;
            livesUI = new UITextureElement[maxlives * 2];
            float livesSize = 0.25f;
            for (int i = 0; i < maxlives; i++) {
                livesUI[i] = new UITextureElement("ball", new Vector2(i * livesSize, 0), new Vector2(livesSize, livesSize));
                livesUI[i + maxlives] = new UITextureElement("ball", new Vector2(16 - livesSize - i * livesSize, 0), new Vector2(livesSize, livesSize));
            }
            particles = new ParticleEmitter("ball", 500);
            //load the music
            song = AssetManager.GetResource<Song>("music");
            PlaySong();
        }

        public void Unload()
        {
            objects.Clear();
            song.Dispose();
        }
        
        public void Update(GameTime gameTime)
        {
            objects.Update(gameTime);
            emitBallParticles(ball);
            if (mode == MODE.multiball)
                emitBallParticles(extraball);
            particles.Update();
        }

        private void emitBallParticles(GameObject o)
        {
            Vector2 rdir = new Vector2((float)Pong.Random.NextDouble() - 0.5f, (float)Pong.Random.NextDouble() - 0.5f);
            Vector2 ppos = o.Pos + o.Size * 0.5f - new Vector2(0.05f, 0.05f);
            particles.Emit(ppos, new Vector2(0.1f, 0.1f), rdir, colours.Colour, 0.01f, 60, 0.95f, 1.05f, 2);
        }

        public void Draw(GameTime time, SpriteBatch batch, GraphicsDevice device)
        {
            device.Clear(colours.BackColour);
            batch.Begin();

            particles.Draw(batch);
            objects.Draw(time, batch);

            Color uiLivesColour = colours.PeekNextColour();
            int player2lives = 0;
            if (mode != MODE.ai) player2lives = paddle1.Lives;
            else player2lives = autoPaddle.Lives;
            for (int i = 0; i < maxlives * 2; i++)
            {
                if (i > paddle0.Lives - 1 && i < maxlives) continue;
                if (i > maxlives - 1 + player2lives && i > maxlives - 1) continue;
                livesUI[i].colour = uiLivesColour;
                livesUI[i].Draw(batch);
            }
            
            batch.End();
        }
        
        public void PlaySong()
        {
            MediaPlayer.Play(song);
            MediaPlayer.Volume = 0.2f;
        }
    }
}