using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Pong.Core;

namespace Pong
{
    public class Colourizer : GameObject
    {
        private Color[] colours;
        private int currentCol = 0;
        private Ball ball;
        private Paddle paddle0, paddle1;

        public Colourizer()
        {
            colours = new Color[6];
            colours[0] = Color.Red;
            colours[1] = Lerp(0.5f, new Color(255, 0, 0), new Color(0, 255, 0));
            colours[2] = Color.Green;
            colours[3] = Lerp(0.5f, new Color(0, 255, 0), new Color(0, 0, 255));
            colours[4] = Color.Blue;
            colours[5] = Lerp(0.5f, new Color(0, 0, 255), new Color(255, 0, 0));
            tag = "colourizer";
        }

        public override void Init()
        {
            ball = FindWithTag("ball") as Ball;
            GameObject[] paddles = FindAllWithTag("paddle");
            paddle0 = paddles[0] as Paddle;
            paddle1 = paddles[1] as Paddle;
        }

        public override void Update(GameTime gameTime) { }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }

        public Color GetColour()
        {
            return colours[currentCol];
        }

        public Color PeekNextColour()
        {
            int index = currentCol + 1;
            if (index > colours.Length - 1)
                index = 0;
            return colours[index];
        }

        public void NextColour()
        {
            currentCol++;
            if (currentCol > colours.Length - 1)
                currentCol = 0;
            ball.Colour = colours[currentCol];
            paddle0.Colour = colours[currentCol];
            paddle1.Colour = colours[currentCol];
        }
        
        public Color Lerp(float t, Color a, Color b)
        {
            a.R = Lerp(t, a.R, b.R);
            a.G = Lerp(t, a.G, b.G);
            a.B = Lerp(t, a.B, b.B);
            a.A = Lerp(t, a.A, b.A);
            Color col = a;
            return col;
        }

        public byte Lerp(float t, byte a, byte b)
        {
            return (byte)(a + t * (b - a));
        }
    }
}