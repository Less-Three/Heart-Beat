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
    /// Abstract class to handle player behaviour.
    /// </summary>
    public class Player : SceneObject
    {
        private Texture2D texture;
        private SoundEffect footstep, punchWhoosh;

        private bool hasItem;

        public Player(Game game)
            : base(game)
        {
            hitPoints = 100;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void Initialize(ContentManager Content, string texturePath)
        {
            texture = Content.Load<Texture2D>(texturePath);

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw player.
        /// </summary>
        /// <param name="spriteBatch">Group of sprites to be drawn with same settings.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(800/2, 600/2), Color.White);
        }
    }
}
