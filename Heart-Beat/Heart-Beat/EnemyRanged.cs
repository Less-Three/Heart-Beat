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
    public class EnemyRanged : Enemy
    {
        private const float SPEED = 3.5f;
        private bool isDesperate = false;
        public EnemyRanged(Game game)
            : base(game)
        {
            location = new Vector2(400.0f, 0.0f);
            hitPoints = 100;
            defaultWeapon = 2;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            int[] frameCountsPerAnim = { 0, 2, 3, 0, 2 };

            animation.Initialize("Enemies/Thug02_sprites", frameCountsPerAnim, 350);
            animation.AddAnimation("Walking", 2);
            animation.AddAnimation("Punching", 3);
            animation.AddAnimation("Dying", 5);

            animation.select = "Walking";
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime, Player target)
        {
            if (hitPoints <= 0)
                animation.select = "Dying";

            float diffX = location.X - target.getX();
            float diffZ = Z - target.Z;

            if (diffX > 0) 
            {
                if ((location.X >= 800.0f)||isDesperate)
                {
                    location.X -= (SPEED / 2);
                    animation.isMirrored = false;
                    isDesperate = true;
                }
                else
                {
                    location.X += (SPEED / 2);
                    animation.isMirrored = true;
                }
            }
            else if (diffX < 0) 
            {
                if ((location.X <= 200.0f)||isDesperate)
                {
                    location.X += (SPEED / 2);
                    animation.isMirrored = true;
                    isDesperate = true;
                }
                else
                {
                    location.X -= (SPEED / 2);
                    animation.isMirrored = false;
                }
            }
            if (diffZ > 0) 
                Z += (SPEED / 2);
            else if (diffZ < 0) 
                Z -= (SPEED / 2);
            
            animation.Update(gameTime, translatedLocation);
            base.Update(gameTime);
            coolDown --;
            if (currentWeapon == 0 && coolDown < 0)
            {
                currentWeapon = defaultWeapon;
                coolDown = 10;
            }

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
    }
}
