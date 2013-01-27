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
    /// Abstract class to handle enemy behaviour.
    /// </summary>
    public abstract class Enemy : SceneObject
    {
        protected Player target; //a reference, probably, to the player - could also move towards items
        protected enum Types { BasicMelee=1, BasicRanged, AdvancedMelee, AdvancedRanged, Boss }; // a list of the different enemy types that could spawn?
        protected int type;
        protected int defaultWeapon;
        protected int coolDown = 5;
        protected bool isDead;
        protected const float DEAD_SPEED = 1.0f;

        public Enemy(Game game)
            : base(game)
        {
            // No child components to construct
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime, Player target)
        {
            base.Update(gameTime);
        }
        public override void takeDamage(int damage)
        {
            base.takeDamage(damage);
            if (hitPoints < 1)
            {
                animation.select = "Dying";
                isDead = true;
            }
        
        }
    }
}
