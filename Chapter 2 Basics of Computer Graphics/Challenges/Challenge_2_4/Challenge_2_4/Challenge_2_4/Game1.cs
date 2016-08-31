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
Challenge 2.4. Write a program that allows the user to change the back-
ground color by using the hue-saturation-lightness (HSL) color palette.
*/

namespace Challenge_2_4
{
    struct RGB
    {
        public int red;
        public int green;
        public int blue;
    }

    struct HSL
    {
        public float hue;
        public float saturation;
        public float lightness;
    }

    // this class is adapted from the answers found on this stackoverflow question:
    // http://stackoverflow.com/questions/2353211/hsl-to-rgb-color-conversion
    static class HSLRGBConverter
    {
        static float HueToRgb(float p, float q, float t)
        {
            if (t < 0f)
                t += 1f;
            if (t > 1f)
                t -= 1f;
            if (t < 1f / 6f)
                return p + (q - p) * 6f * t;
            if (t < 1f / 2f)
                return q;
            if (t < 2f / 3f)
                return p + (q - p) * (2f / 3f - t) * 6f;
            return p;

        }

        // we use a more generic "container" instead of Color
        public static RGB HslToRgb(HSL hsl)
        {
            float r, g, b;

            if (hsl.saturation == 0f)
            {
                r = g = b = hsl.lightness; // achromatic
            }
            else
            {
                float q = hsl.lightness < 0.5f ? hsl.lightness * (1 + hsl.saturation) : hsl.lightness + hsl.saturation - hsl.lightness * hsl.saturation;
                float p = 2 * hsl.lightness - q;
                r = HueToRgb(p, q, hsl.hue + 1f / 3f);
                g = HueToRgb(p, q, hsl.hue);
                b = HueToRgb(p, q, hsl.hue - 1f / 3f);
            }

            return new RGB
            {
                red = (int)Math.Round(r * 255),
                green = (int)Math.Round(g * 255),
                blue = (int)Math.Round(b * 255)
            };
        }

        // we use a more generic "container" instead of a specific structure
        public static HSL RgbToHsl(RGB rgb)
        {
            float r = rgb.red / 255f;
            float g = rgb.green / 255f;
            float b = rgb.blue / 255f;

            float max = (r > g && r > b) ? r : (g > b) ? g : b;
            float min = (r < g && r < b) ? r : (g < b) ? g : b;

            float h, s, l;
            l = (max + min) / 2.0f;

            if (max == min)
            {
                h = s = 0.0f;
            }
            else
            {
                float d = max - min;
                s = (l > 0.5f) ? d / (2.0f - max - min) : d / (max + min);

                if (r > g && r > b)
                    h = (g - b) / d + (g < b ? 6.0f : 0.0f);

                else if (g > b)
                    h = (b - r) / d + 2.0f;

                else
                    h = (r - g) / d + 4.0f;

                h /= 6.0f;
            }

            return new HSL { hue = h, saturation = s, lightness = l };
        }
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        HSL background_color;
        Color current_color;

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
            background_color = HSLRGBConverter.RgbToHsl(new RGB() { red = 255, green = 0, blue = 0 });

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

            if (Keyboard.GetState().IsKeyDown(Keys.H) && Keyboard.GetState().IsKeyDown(Keys.PageUp))
            {
                if (background_color.hue <= 1.0f)
                    background_color.hue += 0.01f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.H) && Keyboard.GetState().IsKeyDown(Keys.PageDown))
            {
                if (background_color.hue >= 0.0f)
                    background_color.hue -= 0.01f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.PageUp))
            {
                // step between 0% and 100%
                if (background_color.saturation <= 1.0f)
                    background_color.saturation += 0.01f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.PageDown))
            {
                // step between 0% and 100%
                if (background_color.saturation >= 0.0f)
                    background_color.saturation -= 0.01f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.L) && Keyboard.GetState().IsKeyDown(Keys.PageUp))
            {
                // step between 0% and 100%
                if (background_color.lightness <= 1.0f)
                    background_color.lightness += 0.01f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.L) && Keyboard.GetState().IsKeyDown(Keys.PageDown))
            {
                // step between 0% and 100%
                if (background_color.lightness >= 0.0f)
                    background_color.lightness -= 0.01f;
            }

            RGB current_rgb = HSLRGBConverter.HslToRgb(background_color);
            current_color = new Color(current_rgb.red, current_rgb.green, current_rgb.blue);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(current_color);

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
