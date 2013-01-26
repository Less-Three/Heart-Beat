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
        private AnimationID playerState;
        private KeyboardState keyState;
        private Texture2D texture;
        private SoundEffect footstep, punchWhoosh;
        private bool hasItem;

        public Player(Game game)
            : base(game)
        {
            animation = new Animation(game);
            location = new Vector2(0,0);
            hitPoints = 100;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void Initialize(ContentManager Content, string texturePath)
        {
            int[] frameCounts = {3, 2, 2, 3, 3};
            animation.Initialize(Content, "Player/Heartbeat_sprites", frameCounts, 350);

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            playerState = AnimationID.IDLE;
            keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Up))
            {
                z += 5.0f;
                playerState = AnimationID.WALKING;
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                z -= 5.0f;
                playerState = AnimationID.WALKING;
            }

            if (keyState.IsKeyDown(Keys.Left))
            {
                location.X -= 5.0f;
                playerState = AnimationID.WALKING;
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                location.X += 5.0f;
                playerState = AnimationID.WALKING;
            }

            if (keyState.IsKeyDown(Keys.Space))
            {
                if (location.Y == 0)
                {
                    location.Y = 1.0f;
                    ySpeed = 15.0f;
                }
            }

            //System.Console.Write (location.X + ", " + location.Y + ", " + z + ", " + translatedLocation + "\n");
            this.UpdateGravity(gameTime);
            base.Update(gameTime);
            translatedLocation = new Vector2((location.X) - (0.5f * z), (600 - z) - location.Y);
            animation.Update(gameTime, translatedLocation, playerState);
        }

        /// <summary>
        /// Draw player.
        /// </summary>
        /// <param name="spriteBatch">Group of sprites to be drawn with same settings.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }
    }
}
