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
            animation = new Animation(game);
            location = new Vector2(400,300);
            hitPoints = 100;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void Initialize(ContentManager Content, string texturePath)
        {
            int[] frameCountsPerAnim = {3, 2, 2, 3, 3};
            animation.Initialize(Content, "Player/Heartbeat_sprites", frameCountsPerAnim, 350);
            animation.AddAnimation("Idle", 1);
            animation.AddAnimation("Walking", 2);
            animation.AddAnimation("Punching", 3);
            animation.AddAnimation("Jumping", 4);
            animation.AddAnimation("Dying", 5);

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            animation.select = "Idle";

            keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Up))
            {
                z += 5.0f;
                animation.select = "Walking";
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                z -= 5.0f;
                animation.select = "Walking";
            }

            if (keyState.IsKeyDown(Keys.Left))
            {
                location.X -= 5.0f;
                animation.select = "Walking";
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                location.X += 5.0f;
                animation.select = "Walking";
            }

            if (keyState.IsKeyDown(Keys.Space))
            {
                //TODO make player jump
            }

            //System.Console.Write (location.X + ", " + location.Y + ", " + z + ", " + translatedLocation + "\n");

            base.Update(gameTime);
            animation.Update(gameTime, translatedLocation);
        }

        /// <summary>
        /// Draw player.
        /// </summary>
        /// <param name="spriteBatch">Group of sprites to be drawn with same settings.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }
    }
}
