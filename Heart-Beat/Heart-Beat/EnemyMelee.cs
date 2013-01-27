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
    public class EnemyMelee : Enemy
    {
        private Random rand = new Random();
        private const float SPEED = 4.0f;
        public EnemyMelee(Game game)
            : base(game)
        {
            location = new Vector2(500.0f, 0.0f);
            hitPoints = 100;
            defaultWeapon = 1;
        }
        public EnemyMelee(Game game, Vector2 v)
            : base(game)
        {
            Z = rand.Next(200, 300);
            location = v;
            hitPoints = 100;
            defaultWeapon = 1;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            int[] frameCountsPerAnim = { 0, 2, 3, 0, 2 };
            animation.Initialize("Enemies/Thug0" + rand.Next(1,3) + "_sprites", frameCountsPerAnim, 350);
            //animation.Initialize("Enemies/Thug01_sprites", frameCountsPerAnim, 350);
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
            int delta = 8;

            if (!isDead)
            {
                float diffX = location.X - target.getX();
                float diffZ = Z - target.Z;
                if (diffX > 0) { location.X -= (SPEED / 2) + rand.Next(-delta, delta); animation.isMirrored = false; }
                else if (diffX < 0) { location.X += (SPEED / 2) + rand.Next(-delta, delta); animation.isMirrored = true; }
                if (diffZ > 0) { Z -= (SPEED / 2) + rand.Next(-20, 20); }
                else if (diffZ < 0) { Z += (SPEED / 2) + rand.Next(-delta, delta); }
                if ((Math.Sqrt(diffZ * diffZ) < 100) && (Math.Sqrt(diffX * diffX) < 100))
                {
                    if (coolDown == 0)
                    {
                        animation.select = "Punching";
                        currentWeapon = 1;
                        coolDown = 10;
                    }
                    else
                    {
                        coolDown--;
                    }

                }
                else
                    animation.select = "Walking";

                //coolDown--;
                if (currentWeapon == 0 && coolDown < 0)
                {
                    currentWeapon = defaultWeapon;
                    coolDown = 10;
                }
            }

            if (isDead) location.X -= DEAD_SPEED;

            animation.Update(gameTime, translatedLocation);
            base.Update(gameTime);
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
