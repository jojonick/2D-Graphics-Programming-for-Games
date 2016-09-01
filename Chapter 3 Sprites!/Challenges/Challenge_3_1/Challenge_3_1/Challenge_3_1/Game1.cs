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

namespace Challenge_3_1
{
    class QuiltSprite
    {
        Texture2D texture;
        Vector2 position;
        Color color;
        float rotation;

        public QuiltSprite(Texture2D t, Vector2 p, Color c, float r)
        {
            texture = t;
            position = p;
            color = c;
            rotation = r;
        }

        public Texture2D Texture
        {
            get { return texture; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public Color Color
        {
            get { return color; }
        }

        public float Rotation
        {
            get { return rotation; }
        }
    }

    enum State { REPEATED, ROTATED, COLORED };

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        State current_state;

        SpriteBatch spriteBatch;
        List<QuiltSprite> quilts_sprite;
        int max_x, max_y; // for the loop

        Random rnd = new Random();

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

            // 32 x 32 is the texture size
            max_x = 800 / 32;
            max_y = 480 / 32;

            quilts_sprite = new List<QuiltSprite>();

            current_state = State.REPEATED;

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
            for (int y = 0; y < max_y; y++)
            {
                for (int x = 0; x < max_x; x++)
                {
                    quilts_sprite.Add(new QuiltSprite(
                        Content.Load<Texture2D>("quilt_piece"),
                        new Vector2(x * 32, y * 32),
                        new Color(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)),
                        (float)Math.PI / 2.0f)); // 90° because PI is equals to 180°
                }
            }
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
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                current_state = State.REPEATED;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                current_state = State.ROTATED;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                current_state = State.COLORED;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            switch (current_state)
            {
                case State.REPEATED:
                    foreach (QuiltSprite sprite in quilts_sprite)
                    {
                        spriteBatch.Draw(sprite.Texture, sprite.Position, Color.White);
                    }
                    break;
                case State.ROTATED:
                    foreach (QuiltSprite sprite in quilts_sprite)
                    {
                        spriteBatch.Draw(sprite.Texture,
                        sprite.Position,
                        null, // source rectangle; none
                        Color.White,
                        sprite.Rotation, // rotation
                        new Vector2(0, 30), // origin of rotation; default top-left corner; in our case 0,30 left-bottom corner
                        1.0f, // scale; 1.0f no scale
                        SpriteEffects.None,
                        0f); // depth
                    }
                    break;
                case State.COLORED:
                    foreach (QuiltSprite sprite in quilts_sprite)
                    {
                        spriteBatch.Draw(sprite.Texture, sprite.Position, sprite.Color);
                    }
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
