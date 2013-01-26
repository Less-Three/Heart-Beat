/* Source: http://coderplex.blogspot.ca/2010/04/2d-animation-part-5-multiple-animations.html */

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
        private Dictionary<string, Rectangle[]> animations;     // Animations used and their frames
        public String select;                                   // Select animation
        private Vector2 location;
        private int[] frameCounts;
        private Texture2D spriteStrip;
        private int timeDisplayFrame;                           // Time to display frame
        private double elapsedTime;
        private int currentFrame;
        private int frameWidth, frameHeight;
        private Rectangle destinationRect;

        public Animation(Game game)
            : base(game)
        {
            animations = new Dictionary<string, Rectangle[]>();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void Initialize(ContentManager Content, string imagePath, int[] fCountsPerAnim, int time)
        {
            // Get number of different animations
            int size = fCountsPerAnim.Length;

            spriteStrip = Content.Load<Texture2D>(imagePath);
            frameCounts = new int[size];
            currentFrame = 0;
            timeDisplayFrame = time;

            // Get frame counts for each animation
            for (int i = 0; i < frameCounts.Length; i++)
                frameCounts[i] = fCountsPerAnim[i];
            int maxFramesInAType = GetMaxFrames();

            frameWidth = spriteStrip.Width / maxFramesInAType;
            frameHeight = spriteStrip.Height / size;
            
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime, Vector2 loc)
        {
            location = loc;
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime > timeDisplayFrame)
            {
                   currentFrame++;
                   if (currentFrame == frameCounts[0] - 1) currentFrame = 0;
                   elapsedTime = 0.0;
            }

            // Get next frame
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
            spriteBatch.Draw(spriteStrip, destinationRect, animations[select][currentFrame], Color.White);
        }

        /*****************************************
         * Return the maximum number of frames   *
         * in of all the types in a sprite sheet *
         * ***************************************/
        private int GetMaxFrames()
        {
            int max = 0;

            for (int i = 0; i < frameCounts.Length; i++ )
            {
                if (frameCounts[i] > max) max = frameCounts[i];
            }
            return max;
        }

        public void AddAnimation(string name, int row)
        {
            Rectangle[] recs = new Rectangle[frameCounts[row-1]];

            for (int i = 0; i < frameCounts[row-1]; i++)
            {
                recs[i] = new Rectangle(i * frameWidth,
                                        (row - 1) * frameHeight,
                                        frameWidth,
                                        frameHeight);
            }
            animations.Add(name, recs);
        }
    }
}
