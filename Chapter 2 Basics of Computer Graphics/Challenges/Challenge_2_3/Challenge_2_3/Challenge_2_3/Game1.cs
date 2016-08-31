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
Challenge 2.3. Write a program that displays a chart of all the colors in
the 12-bit RGB color model by mapping a diﬀerent color to each pixel.
*/

namespace Challenge_2_3
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        Texture2D point_sprite;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

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
            Color[] array_of_color = { Color.White };


            Rectangle point_rectangle = new Rectangle(0, 0, 1, 1);

            point_sprite = new Texture2D(GraphicsDevice, 1, 1);
            point_sprite.SetData<Color>(array_of_color);


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);

            // TODO: Add your drawing code here
            Vector2 my_location = new Vector2(50, 10);

            Color my_color = Color.Black;

            spriteBatch.Begin();

            for (byte r = 0; r < 16; r++)
            {
                my_color.R = (byte)(r * 16);

                for (byte g = 0; g < 16; g++)
                {
                    my_color.G = (byte)(g * 16);
                    for (byte b = 0; b < 16; b++)
                    {
                        my_color.B = (byte)(b * 16);
                        my_location.X++;

                        spriteBatch.Draw(point_sprite, my_location, my_color);
                    }
                    my_location.X = 50;
                    my_location.Y++;
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
