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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public abstract class SceneObject : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;
        protected Vector2 location;
        private float z;
        protected Animation animation;
        protected int hitPoints;
        protected Rectangle collisionRectangle; // X and Y collision
        protected double collisionDepth; // Z collision
        protected Vector2 translatedLocation; // Location converted to X-Z axis.
        protected float ySpeed;
        protected const float Y_SPEED_MAX = 15.0f;
        private const float MAX_BOUNDARY = 440.0f;
        private const float MIN_BOUNDARY = 200.0f;

        protected SceneObject(Game game)
            : base(game)
        {
            animation = new Animation(game);
        }

        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        protected void Initialize(Vector2 loc, float z, int hp, Rectangle rect, double depth, Animation anim)
        {
            location = loc;
            this.z = z;
            hitPoints = hp;
            collisionRectangle = rect;
            collisionDepth = depth;
            animation = anim;

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

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            collisionRectangle = new Rectangle((int)translatedLocation.X, (int)translatedLocation.Y, (int)animation.FrameWidth, (int)animation.FrameHeight);

            z = MathHelper.Clamp(z, MIN_BOUNDARY, MAX_BOUNDARY);    // Ensure Z is within player moveable boundaries
            translatedLocation = new Vector2((location.X) - (0.5f * z), 600 - z - location.Y);

            base.Update(gameTime);
        }

        public Rectangle GetRectangle()
        {
            return collisionRectangle;
        }

        public int GetHitPoints()
        {
            return hitPoints;
        }

        public void takeDamage(int damage)
        {
            hitPoints = hitPoints - damage;
            if (hitPoints < 0)
            {
                hitPoints = 0;
            }
        }

        public void UpdateGravity(GameTime gameTime)
        {
            // System.Console.WriteLine("location = " + location.Y);
            if (location.Y > 0)
            {
                ySpeed -= 1.0f;
                location.Y += ySpeed;
            }
            else
            {
                ySpeed = 0.0f;
                location.Y = 0;
            }
        }

        public float getX()
        {
            return location.X;
        }

        public float getY()
        {
            return location.Y;
        }

        public bool getIsMirroed()
        {
            return animation.isMirrored;
        }
    }
}
