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
    public class Background : Microsoft.Xna.Framework.GameComponent
    {
        private Texture2D texture;
        private Vector2[] tiles;    // For continuous scrolling
        private float speed;

        public Background(Game game)
            : base(game)
        { }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void Initialize(ContentManager content, String texturePath, int winWidth)
        {
            texture = content.Load<Texture2D>(texturePath);

            // Divide screen by texture width to determine # of tiles required
            int numTiles = winWidth/texture.Width + 1;
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
        /// Draw each background tile.
        /// </summary>
        /// <param name="spriteBatch">Group of sprites to be drawn with same settings.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Vector2 p in tiles)
                spriteBatch.Draw(texture, p, Color.White);
        }
    }
}
