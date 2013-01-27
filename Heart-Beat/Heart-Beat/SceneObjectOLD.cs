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
    /// Class to handle behaviour of scene objects.
    /// </summary>
    public abstract class SceneObject : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected Vector2 location;
        protected float z;
        protected Animation animation;
        protected int hitPoints;
        protected Rectangle collisionRectangle; // X and Y collision
        protected double collisionDepth; // Z collision
        protected Vector2 translatedLocation; // Location converted to X-Z axis.
        protected float ySpeed;
        protected const float Y_SPEED_MAX = 15.0f;
        private const float MAX_BOUNDARY = 440.0f;
        private const float MIN_BOUNDARY = 200.0f;

        public SceneObjectOLD(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public SceneObject(Game game, Vector2 location, float z, int hitPoints, Rectangle collisionRectangle, double collisionDepth, Animation animation) : this(game)

        {
            this.location = location;
            this.z = z;
            this.hitPoints = hitPoints;
            this.collisionRectangle = collisionRectangle;
            this.collisionDepth = collisionDepth;
            this.animation = animation;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

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
            System.Console.WriteLine("location = " + location.Y);
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
    }
}
