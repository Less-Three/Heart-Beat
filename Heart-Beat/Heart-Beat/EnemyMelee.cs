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
        private const float SPEED = 5.0f;
        public EnemyMelee(Game game)
            : base(game)
        {
            animation = new Animation(game);
            location = new Vector2(300.0f, 0.0f);
            hitPoints = 100;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            int[] frameCountsPerAnim = { 0, 2, 3, 0, 2 };
            Random rand = new Random();
            int thugType = rand.Next(1, 3);
            if( thugType == 1) animation.Initialize("Enemies/Thug01_sprites", frameCountsPerAnim, 350);
            else animation.Initialize("Enemies/Thug02_sprites", frameCountsPerAnim, 350);
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
        public void Update(GameTime gameTime, Player target)
        {
            float diffX = location.X - target.getX();
            float diffZ = Z - target.Z;
            if (diffX > 0) { location.X -= (SPEED / 2); }
            else if (diffX < 0) { location.X += (SPEED / 2); }
            if (diffZ > 0) { Z -= (SPEED / 2); }
            else if (diffZ < 0) { Z += (SPEED / 2); }
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
