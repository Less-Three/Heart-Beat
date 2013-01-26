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
        private KeyboardState keyState;
        private Texture2D texture;
        private SoundEffect footstep, punchWhoosh;

        private bool hasItem;

        public Player(Game game)
            : base(game)
        {
            location = new Vector2(400,300);
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
            keyState = Keyboard.GetState();

            if ((keyState.IsKeyDown(Keys.Up))&&(location.Y > 160))
            {
                location.Y -= 5.0f;
            }
            if ((keyState.IsKeyDown(Keys.Down))&&(location.Y < 400))
            {
                location.Y += 5.0f;
            }
            if (keyState.IsKeyDown(Keys.Left))
            {
                location.X -= 5.0f;
            }
            if (keyState.IsKeyDown(Keys.Right))
            {
                location.X += 5.0f;
            }
            if (keyState.IsKeyDown(Keys.Space))
            {
                //TODO make player jump
            }
            System.Console.Write (location.X + ", " + location.Y + "\n");
            base.Update(gameTime);

            // 160 -> 400
        }

        /// <summary>
        /// Draw player.
        /// </summary>
        /// <param name="spriteBatch">Group of sprites to be drawn with same settings.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
                spriteBatch.Draw(texture, location, Color.White);
        }
    }
}
