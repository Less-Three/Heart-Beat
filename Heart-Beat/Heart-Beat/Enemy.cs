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
        public Enemy(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
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
        public virtual void Update(GameTime gameTime, Player player)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }
    }
}
