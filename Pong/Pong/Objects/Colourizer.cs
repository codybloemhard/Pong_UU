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
        private static Color[] colours;
        private static List<float> blendIndices;
        private Color backColour;
        private int currentCol = 0;
        private Ball ball, extraBall;
        private GameObject paddle0, paddle1;
        private MODE mode;
        
        static Colourizer()
        {
            colours = new Color[12];
            colours[0] = new Color(255, 0, 0);
            colours[2] = new Color(255, 255, 0);
            colours[4] = new Color(0, 255, 0);
            colours[6] = new Color(0, 255, 255);
            colours[8] = new Color(0, 0, 255);
            colours[10] = new Color(255, 0, 255);
            blendIndices = new List<float>();
        }

        public Colourizer()
        {
            backColour = Color.Black;
            for (int i = 0; i < 5; i++)
                colours[i * 2 + 1] = Lerp(0.5f, colours[i * 2], colours[i * 2 + 2]);
            colours[11] = Lerp(0.5f, colours[10], colours[0]);
            tag = "colourizer";
        }

        public override void Init()
        {
            mode = DataManager.GetData<MODE>("mode");
            ball = FindWithTag("ball") as Ball;
            if (mode == MODE.multiball)
                extraBall = FindWithTag("extraball") as Ball;
            if(mode != MODE.ai)
            {
                GameObject[] paddles = FindAllWithTag("paddle");
                paddle0 = paddles[0];
                paddle1 = paddles[1];
            }
            else
            {
                paddle0 = FindWithTag("paddle");
                paddle1 = FindWithTag("autopaddle");
            }
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
        //fancy
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
        //cycle to the next colour
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
        /*returns a continues stream of colors LERPed from the basic colours
        and at a specified speed with offset, multiple channels or streams possible.*/
        public static Color ContinuesColourBlend(int channel, float rate, float offset)
        {
            if (channel >= blendIndices.Count)
                blendIndices.Add(rate);
            int blendIndex = channel;
            blendIndices[blendIndex] += rate;
            if (blendIndices[blendIndex] >= 1.0f)
                blendIndices[blendIndex] = 0.0f;
            float localIndex = blendIndices[blendIndex] + offset;
            if (localIndex >= 1.0f) localIndex = localIndex - ((int)localIndex);
            int left = (int)(localIndex * 12);
            int right = left + 1;
            if (right > 11)
                right = 0;
            float difference = (localIndex * 12) - left;
            Color c = Lerp(difference, colours[left], colours[right]);
            return c;
        }
        //LERP = Linear Interpolation
        public static Color Lerp(float t, Color a, Color b)
        {
            a.R = Lerp(t, a.R, b.R);
            a.G = Lerp(t, a.G, b.G);
            a.B = Lerp(t, a.B, b.B);
            a.A = Lerp(t, a.A, b.A);
            Color col = a;
            return col;
        }
        //LERP = Linear Interpolation
        public static byte Lerp(float t, byte a, byte b)
        {
            return (byte)(a + t * (b - a));
        }
    }
}