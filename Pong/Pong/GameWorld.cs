using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Pong
{
    class GameWorld
    {

        static List<Entity> entities = new List<Entity>();
        static int gamestate, loser;
        static Ball ball;
        static Paddle paddle1, paddle2;
        SpriteFont mainFont;
        ContentManager Content;
        Song song;        


        public GameWorld(ContentManager Content)
        {
            //set the gamestate to the start menu
            gamestate = 0;
            //load the content needed for the main menu
            this.Content = Content;
            mainFont = Content.Load<SpriteFont>("mainFont");
            //load the music
            this.song = Content.Load<Song>("music");
        }
        

        public static void GameOver(int player)
        {
            loser = player;
            //set the gamestate to the game-over screen
            gamestate = 2;
            //stop the music
            MediaPlayer.Stop();
            
        }

        //method to play the music
        public void PlaySong()
        {
                MediaPlayer.Play(song);
                MediaPlayer.Volume = 0.2f;
        }

        public void InitGame()
        {
            //initialize the ball and load it to the game
            ball = new Ball(Content, new Vector2(-5, -3));
            entities.Add(ball);
            //initialize 2 paddles and load them to the game
            paddle1 = new Paddle(Content, 1, Keys.W, Keys.S);
            entities.Add(paddle1);
            paddle2 = new Paddle(Content, 2, Keys.Up, Keys.Down);
            entities.Add(paddle2);
            //start the music
            PlaySong();
        }

        //method to write text to the center of the screen
        public void WriteCenter(SpriteBatch spriteBatch, String s, SpriteFont font, Color col)
        {
            spriteBatch.DrawString(font, s, new Vector2(Pong.ScreenSize.X / 2, Pong.ScreenSize.Y / 2), col, 0, mainFont.MeasureString(s) / 2, 1F, SpriteEffects.None, 1F);
        }

        public void Update(GameTime gameTime)
        {
            switch (gamestate)
            {
                //case 0 sets the gamestate to 1, which is the playing state, if the spacebar is pressed down
                case 0:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    { 
                        entities.Clear();
                        InitGame();
                        gamestate = 1;
                    }
                    break;
                
                //case 1 updates the entities present in the game
                case 1:
                    foreach (Entity entity in entities)
                    {
                        entity.Update(gameTime);
                    }
                    break;
                
                //case 2 is the game-over screen and sets the game back to the starting screen if the spacebar is pressed down
                case 2:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        gamestate = 0;
                    break;
                default:
                    break;

            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            switch (gamestate)
            {
                //case 0 is the start menu and writes the starting text to the screen
                case 0:
                    WriteCenter(spriteBatch, "Press [Space] to begin.", mainFont, Color.Red);
                    break;

                //case 1 draws all the present entities on screen, checks the amount of lives left for each player and draws balls in the
                //corners of the screen accordingly
                case 1:
                    foreach (Entity entity in entities)
                    {
                        entity.Draw(gameTime, spriteBatch);
                    }

                    for (int i = 0; i < paddle1.Lives; i++)
                    {
                        spriteBatch.Draw(ball.Sprite, new Vector2(i * ball.Sprite.Width, 0), Color.White);
                    }
                    for (int i = 0; i < paddle2.Lives; i++)
                    {
                        spriteBatch.Draw(ball.Sprite, new Vector2(Pong.ScreenSize.X - (i + 1) * ball.Sprite.Width, 0), Color.White);
                    }
                    break;

                //case 2 writes the end game text on the screen and checks which player lost
                case 2:
                    WriteCenter(spriteBatch, "Player " + loser + " lost!", mainFont, Color.Red);
                    break;

                default:
                    break;
            }

            spriteBatch.End();
        }

        public static Ball Ball
        {
            get { return ball; }
        }

        public Paddle Paddle1
        {
            get { return Paddle1; }
        }

        public Paddle Paddle2
        {
            get { return Paddle2; }
        }

    }
}
