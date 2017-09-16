using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Core
{
    public static class UI
    {
        //method to write text to the center of the screen
        public static void WriteCenter(SpriteBatch spriteBatch, String s, SpriteFont font, Color col)
        {
            spriteBatch.DrawString(font, s, new Vector2(Pong.ScreenSize.X / 2, Pong.ScreenSize.Y / 2), col, 0, font.MeasureString(s) / 2, 1F, SpriteEffects.None, 1F);
        }
    }
}