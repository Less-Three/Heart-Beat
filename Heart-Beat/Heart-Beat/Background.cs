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


namespace Heart_Beat
{
    /// <summary>
    /// Class to handle behaviour of background.
    /// </summary>
    public class Background : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Vector2[] tiles;    // For continuous scrolling
        private float speed;

        public Background(Game game)
            : base(game)
        {
            // No child components to construct
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            texture = Game.Content.Load<Texture2D>("Background/city_bg");

            // Divide screen by texture width to determine # of tiles required
            int numTiles = Game.GraphicsDevice.Viewport.Width/texture.Width + 1;
            tiles = numTiles > 2 ? new Vector2[numTiles] : new Vector2[2];
            for (int i = 0; i < tiles.Length; i++)
                tiles[i] = new Vector2(i * texture.Width, 0);

            speed = 1.0f;

            base.Initialize();
        }

        /// <summary>
        /// Update position of tiles
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].X -= speed;

                if (tiles[i].X <= -texture.Width)
                    tiles[i].X = texture.Width;
            }

                base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (Vector2 p in tiles)
                spriteBatch.Draw(texture, p, Color.White);
            spriteBatch.End();
        }
    }
}
