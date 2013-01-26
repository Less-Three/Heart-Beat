/* Animation.cs
 * 
 * Created by Johnathan Chu during the Vancouver Global Game Jam 2013
 * Last Modified: 2013-01-26
 */

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
    /// Class to handle object animations.
    /// </summary>
    public class Animation : Microsoft.Xna.Framework.GameComponent
    {
        public enum AnimationID { IDLE, WALKING, JUMPING, PUNCHING, ATTACKING, DYING };
        private Texture2D spriteStrip;
        private int frameCount;
        private bool active;
        private int timeToDisplayFrame;
        private int currentFrame;
        private int frameWidth, frameHeight;
        private Rectangle SourceRect, DestinationRect;

        public Animation(Game game, string imagePath, int fCount, int timeToDisplay)
            : base(game)
        {
            spriteStrip = game.Content.Load<Texture2D>(imagePath);
            frameCount = fCount;
            timeToDisplayFrame = timeToDisplay;
            currentFrame = 0;
            frameWidth = spriteStrip.Width/frameCount;
            frameHeight = spriteStrip.Height;
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
        public void beginAnimation(int animationID)
        {

        }
    }
}
