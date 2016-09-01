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
Challenge 3.2. In the sample code we have worked through so far, the
source and destination data associated with each sprite have been stored
in individual variables. Your challenge is to create a more robust code
architecture to store and render multiple sprites on multiple sprite sheets.

A good place to start is with an object-oriented architecture. A sprite
class could include a reference to the texture as well as sprite source data.
An object class could include a reference to the sprite class as well as the
appropriate destination data.
*/

namespace Challenge_3_2
{
    class Sprite
    {
        Texture2D texture;

        public Sprite(Texture2D t)
        {
            texture = t;
        }

        public Texture2D Texture { get { return texture; } }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public Vector2 Origin { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }
        public float Depth { get; set; }
        public SpriteEffects Effects { get; set; }
        public Rectangle Source { get; set; }
        public Rectangle Destination { get; set; }
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;

        Sprite snowman;

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
            // both are power of two so texture fit perfectyl
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.ApplyChanges();

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

            snowman = new Sprite(Content.Load<Texture2D>("snow_assets"));

            snowman.Source = new Rectangle(0, 128, 256, 256);
            snowman.Destination = new Rectangle(200, 200, 256, 256);
            snowman.Color = Color.White;
            snowman.Rotation = 0f;
            snowman.Origin = new Vector2(192, 125);
            snowman.Scale = 1f;
            snowman.Effects = SpriteEffects.None;
            snowman.Depth = 0f;
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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(snowman.Texture,
                snowman.Destination,
                snowman.Source,
                snowman.Color,
                snowman.Rotation,
                snowman.Origin,
                snowman.Effects,
                snowman.Depth);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
