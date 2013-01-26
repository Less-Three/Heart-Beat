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
        private Vector2 location;
        private int[] frameCounts;
        private Texture2D spriteStrip;
        public bool active;
        private int timeDisplayFrame;
        private double elapsedTime;
        private int currentFrame;
        private int frameWidth, frameHeight;
        private Rectangle sourceRect, destinationRect;

        public Animation(Game game)
            : base(game)
        { }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void Initialize(ContentManager Content, string imagePath, int[] fCounts, int time)
        {
            active = false;

            // Get number of different animations
            int size = getEnumSize();

            spriteStrip = Content.Load<Texture2D>(imagePath);
            frameCounts = new int[size];
            currentFrame = 0;
            timeDisplayFrame = time;

            // Get frame counts for each animation
            for (int i = 0; i < frameCounts.Length; i++)
                frameCounts[i] = fCounts[i];
            int maxFramesInAType = getMaxFrames();

            frameWidth = spriteStrip.Width / maxFramesInAType;
            frameHeight = spriteStrip.Height / size;
            
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime, Vector2 loc, AnimationID state)
        {
            location = loc;
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (active && elapsedTime > timeDisplayFrame)
            {
                   currentFrame++;
                   if (currentFrame == frameCounts[0] - 1) currentFrame = 0;
                   elapsedTime = 0.0;
            }

            // Get next frame
            sourceRect = new Rectangle(currentFrame * frameWidth,
                                       currentFrame * frameHeight,
                                       frameWidth,
                                       frameHeight);
            destinationRect = new Rectangle((int)location.X,
                                            (int)location.Y,
                                            frameWidth,
                                            frameHeight);
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw player.
        /// </summary>
        /// <param name="spriteBatch">Group of sprites to be drawn with same settings.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteStrip, destinationRect, sourceRect, Color.White);
        }

        /********************************************
         * Return the number of elements in an enum *
         ********************************************/
        private int getEnumSize()
        {
            Type enumType = typeof (AnimationID);
            return Enum.GetValues(enumType).Length;
        }

        /*****************************************
         * Return the maximum number of frames   *
         * in of all the types in a sprite sheet *
         * ***************************************/
        private int getMaxFrames()
        {
            int max = 0;

            for (int i = 0; i < getEnumSize(); i++ )
            {
                if (frameCounts[i] > max) max = frameCounts[i];
            }
            return max;
        }
    }
}
