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
        private SoundEffect footstep, punchWhoosh;
        private bool hasItem;
        private const float SPEED = 5.0f;

        public Player(Game game)
            : base(game)
        {
            
            location = new Vector2(300,0);
            hitPoints = 100;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            int[] frameCountsPerAnim = {3, 2, 3, 3, 3};
            animation.Initialize("Player/Heartbeat_sprites", frameCountsPerAnim, 350);
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
            if (isDead)
            {
                animation.select = "Dying";
            }
            else
            {
                animation.select = "Idle";

                keyState = Keyboard.GetState();

                if (keyState.IsKeyDown(Keys.Up))
                {
                    Z += SPEED;
                    animation.select = "Walking";
                }
                else if (keyState.IsKeyDown(Keys.Down))
                {
                    Z -= SPEED;
                    animation.select = "Walking";
                }

                if (keyState.IsKeyDown(Keys.Left))
                {
                    location.X -= SPEED;
                    animation.select = "Walking";
                    animation.isMirrored = true;
                }
                else if (keyState.IsKeyDown(Keys.Right))
                {
                    location.X += SPEED;
                    animation.select = "Walking";
                    animation.isMirrored = false;
                }

                if (keyState.IsKeyDown(Keys.Space))
                {
                    if (location.Y <= 0.0f)
                    {
                        location.Y = 1.0f;
                        ySpeed = Y_SPEED_MAX;
                        animation.select = "Jumping";
                    }
                }

                if (keyState.IsKeyDown(Keys.LeftControl))
                {
                    animation.select = "Punching";
                    this.currentWeapon = 1;
                }

                UpdateGravity(gameTime);
                location.X = MathHelper.Clamp(location.X, 0.0f + animation.FrameWidth, Game.GraphicsDevice.Viewport.Width);
                //Console.WriteLine(location.X + "," + Z);
            }
            
            animation.Update(gameTime, translatedLocation);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            animation.Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        public override void takeDamage(int damage)
        {
            base.takeDamage(damage);
            if (hitPoints < 1)
            {
                isDead = true;
                animation.select = "Dying";
            }
        }
    }
}
