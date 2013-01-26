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
    public abstract class SceneObject : Microsoft.Xna.Framework.GameComponent
    {
        protected Vector2 location;
        protected float z;
        protected Animation animation;
        protected int hitPoints;
        protected Rectangle collisionRectangle; // X and Y collision
        protected double collisionDepth; // Z collision

        public SceneObject(Game game)
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
            // TODO: Add your update code here

            base.Update(gameTime);
        }
        public Rectangle getRectangle()
        {
            return collisionRectangle;
        }
        public int getHitPoints()
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
    }
}
