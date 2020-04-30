using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
/*
 * Things to do...take out curves on mountains...create classes for player...fix class on backgrounds...create enemy class...create AI for enemy...create rewards...
 * change images to one sprite sheet...create collision class...create sound/effects class...create scoring system...create title screen...create credits...
 * create bird fights...create cloud enter and cloud exit...create 2 more levels (with transitions?)...damage points and falling animation...4/26/2012
*/


namespace Project1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Scrolling scrolling1;
        Scrolling scrolling2;
        Scrolling scrolling3;
        Scrolling scrolling4;
        Scrolling scrolling5;
        Scrolling scrolling6;
   
        Texture2D player;//this is the player image
     
        Vector2 playerPos;//this is the player position in xy coordinates 
        Vector2 playerOrg;//this is the starting point of player
        
        Rectangle playerRect;

        String direction;

        int timer = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            scrolling1 = new Scrolling(Content.Load<Texture2D>(@"Backgrounds\backgroundMid"), new Rectangle(0, 0, 2048, 500));
            scrolling2 = new Scrolling(Content.Load<Texture2D>(@"Backgrounds\backgroundMid"), new Rectangle(2048, 0, 2048, 500));
            scrolling3 = new Scrolling(Content.Load<Texture2D>(@"Backgrounds\backgroundBack2"), new Rectangle(0, 0, 2048, 500));
            scrolling4 = new Scrolling(Content.Load<Texture2D>(@"Backgrounds\backgroundBack2"), new Rectangle(2048, 0, 2048, 500));
            scrolling5 = new Scrolling(Content.Load<Texture2D>(@"Backgrounds\backgroundBack3"), new Rectangle(0, 0, 2048, 500));
            scrolling6 = new Scrolling(Content.Load<Texture2D>(@"Backgrounds\backgroundBack3"), new Rectangle(2048, 0, 2048, 500));

            player = Content.Load<Texture2D>(@"Sprites\BirdR");
            playerPos = new Vector2(10, 10);
            direction = "R";

            MediaPlayer.Play(Content.Load<Song>(@"Media\Music2"));
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            //scrolling background
            if (scrolling1.rectangle.X + scrolling1.texture.Width <= 0)
                scrolling1.rectangle.X = scrolling2.rectangle.X + scrolling2.texture.Width;
            if (scrolling2.rectangle.X + scrolling2.texture.Width <= 0)
                scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling1.texture.Width;
            if (scrolling3.rectangle.X + scrolling3.texture.Width <= 0)
                scrolling3.rectangle.X = scrolling4.rectangle.X + scrolling4.texture.Width;
            if (scrolling4.rectangle.X + scrolling4.texture.Width <= 0)
                scrolling4.rectangle.X = scrolling3.rectangle.X + scrolling3.texture.Width;
            if (scrolling5.rectangle.X + scrolling5.texture.Width <= 0)
                scrolling5.rectangle.X = scrolling6.rectangle.X + scrolling6.texture.Width;
            if (scrolling6.rectangle.X + scrolling6.texture.Width <= 0)
                scrolling6.rectangle.X = scrolling5.rectangle.X + scrolling5.texture.Width;

            scrolling1.Update();
            scrolling2.Update();
            scrolling3.Update2();
            scrolling4.Update2();
            scrolling5.Update3();
            scrolling6.Update3();

            playerOrg = new Vector2(playerPos.X / 2, playerPos.Y / 2);
            playerRect = new Rectangle(
                (int)playerPos.X,
                (int)playerPos.Y,
                player.Width,
                player.Height);

            //Player interaction
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left))
            {
                player = Content.Load<Texture2D>(@"Sprites\BirdL");
                direction = "L";
                playerPos.X -= 4;
            }

            else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right))
            {
                player = Content.Load<Texture2D>(@"Sprites\BirdR");
                direction = "R";
                playerPos.X += 4;
            }

            else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down))
            {
                if ((direction == "R") || (direction == "RU"))
                {
                    player = Content.Load<Texture2D>(@"Sprites\BirdRD");
                    direction = "RD";
                }
                if ((direction == "L") || (direction == "LU"))
                {
                    player = Content.Load<Texture2D>(@"Sprites\BirdLD");
                    direction = "LD";
                }

                playerPos.Y += 4;
            }

            else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up))
            {
                if ((direction == "R") || (direction == "RD"))
                {
                    player = Content.Load<Texture2D>(@"Sprites\BirdRU");
                    direction = "RU";
                }
                if ((direction == "L") || (direction == "LD"))
                {
                    player = Content.Load<Texture2D>(@"Sprites\BirdLU");
                    direction = "LU";
                }

                playerPos.Y -= 4;
            }

            else
            {
                if ((direction == "RU") || (direction == "RD"))
                {
                    player = Content.Load<Texture2D>(@"Sprites\BirdR");
                    direction = "R";
                }
                if ((direction == "LU") || (direction == "LD"))
                {
                    player = Content.Load<Texture2D>(@"Sprites\BirdL");
                    direction = "L";
                }
            }


            //Animation for bird flapping
            timer++;

            if (timer < 30)
            {
                if (direction == "R")
                {
                    player = Content.Load<Texture2D>(@"Sprites\FBirdR");
                }

                if (direction == "L")
                {
                    player = Content.Load<Texture2D>(@"Sprites\FBirdL");
                }
            }
            else if ((timer >= 30) && (timer < 60))
            {
                if (direction == "R")
                {
                    player = Content.Load<Texture2D>(@"Sprites\BirdR");
                }

                if (direction == "L")
                {
                    player = Content.Load<Texture2D>(@"Sprites\BirdL");
                }
            }
            else
                timer = 0;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            scrolling5.Draw(spriteBatch);
            scrolling6.Draw(spriteBatch);
            scrolling3.Draw(spriteBatch);
            scrolling4.Draw(spriteBatch);
            spriteBatch.Draw(player, playerPos, null, Color.White, 0f, playerOrg, 1, SpriteEffects.None, 0f);
            scrolling1.Draw(spriteBatch);
            scrolling2.Draw(spriteBatch);
            
            spriteBatch.End();
       
            base.Draw(gameTime);
        }
    }
}
