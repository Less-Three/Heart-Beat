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
        public const float defaultCollisionWidth = 1.5f;
        public enum Attack{none = 0, punch = 1, knife = 2};
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
        protected int currentWeapon;

        protected SceneObject(Game game)
            : base(game)
        {
            animation = new Animation(game);
            collisionRectangle = new Rectangle((int)location.X, (int)location.Y, 100, 100);
        }

        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            collisionRectangle = new Rectangle((int)location.X, (int)location.Y, (int)animation.FrameWidth, (int)animation.FrameHeight);

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

        public virtual void takeDamage(int damage)
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

        public bool getIsMirrored()
        {
            return animation.isMirrored;
        }

        /// <summary>
        /// returns the weapon being fired and then sets weapon to zero
        /// </summary>
        /// <returns></returns>
        public int getAttack()
        {
            
            int w = currentWeapon;
            
            currentWeapon = 0;
            return w;
        }
        
    }
}
