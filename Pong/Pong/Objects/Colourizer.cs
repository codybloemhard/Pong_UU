using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Pong.Core;
using Pong.States;

namespace Pong
{
    public class Colourizer : GameObject
    {
        private Color[] colours;
        private Color backColour;
        private int currentCol = 0;
        private Ball ball, extraBall;
        private Paddle paddle0, paddle1;
        private MODE mode;
        
        public Colourizer()
        {
            colours = new Color[12];
            backColour = Color.Black;

            colours[0] = new Color(255, 0, 0);
            colours[2] = new Color(255, 255, 0);
            colours[4] = new Color(0, 255, 0);
            colours[6] = new Color(0, 255, 255);
            colours[8] = new Color(0, 0, 255);
            colours[10] = new Color(255, 0, 255);

            for(int i = 0; i < 5; i++)
            {
                colours[i * 2 + 1] = Lerp(0.5f, colours[i * 2], colours[i * 2 + 2]);
            }
            colours[11] = Lerp(0.5f, colours[10], colours[0]);

            tag = "colourizer";
        }

        public override void Init()
        {
            mode = DataManager.GetData<MODE>("mode");
            ball = FindWithTag("ball") as Ball;
            if (mode == MODE.multiball)
                extraBall = FindWithTag("extraball") as Ball;
            GameObject[] paddles = FindAllWithTag("paddle");
            paddle0 = paddles[0] as Paddle;
            paddle1 = paddles[1] as Paddle;
        }

        public override void Update(GameTime gameTime)
        {
            int rate = 12;
            if(backColour.R > 0)
            {
                backColour.R = (byte)MathHelper.Clamp(backColour.R - rate, 0, 255);
                backColour.G = (byte)MathHelper.Clamp(backColour.G - rate, 0, 255);
                backColour.B = (byte)MathHelper.Clamp(backColour.B - rate, 0, 255);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }

        new public Color Colour
        {
            get { return colours[currentCol]; }
        }
        public Color BackColour
        {
            get { return backColour; }
        }
        public void LiveOff()
        {
            backColour = Color.White;
        }
        //Like peeking a stack
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
            if (mode == MODE.multiball)
                extraBall.Colour = colours[currentCol];
            paddle0.Colour = colours[currentCol];
            paddle1.Colour = colours[currentCol];
        }
        //LERP = Linear Interpolation
        public Color Lerp(float t, Color a, Color b)
        {
            a.R = Lerp(t, a.R, b.R);
            a.G = Lerp(t, a.G, b.G);
            a.B = Lerp(t, a.B, b.B);
            a.A = Lerp(t, a.A, b.A);
            Color col = a;
            return col;
        }
        //LERP = Linear Interpolation
        public byte Lerp(float t, byte a, byte b)
        {
            return (byte)(a + t * (b - a));
        }
    }
}