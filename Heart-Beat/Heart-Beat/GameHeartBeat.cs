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
    /// This is the main type for your game
    /// </summary>
    public class GameHeartBeat : Microsoft.Xna.Framework.Game
    {
        private const int MAX_ENEMIES = 5;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;  // the players
        List<SceneObject> gameObjects;  // list of all game objects - updated in game loops

        //the following lists are used only to simplify collision detection
        List<Enemy> enemies;
        List<Projectile> enemyObjects; // list of all enemy projectiles
        List<Projectile> playerObjects; // list of all player projectiles including punches

        public GameHeartBeat()
        {
            Window.Title = "Heart Beat";
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";

            Components.Add(new Background(this));
            player = new Player(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            gameObjects = new List<SceneObject>();
            enemies = new List<Enemy>();
            enemyObjects = new List<Projectile>();
            playerObjects = new List<Projectile>();

            player.Initialize();
            for (int i = 0; i < 1; i++)
            {
                Enemy enemy = new EnemyMelee(this);
                enemy.Initialize();

                Enemy enemyTwo = new EnemyRanged(this);
                enemyTwo.Initialize();

                enemies.Add(enemyTwo);
                enemies.Add(enemy);
            }
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            CheckCollisions();

            foreach (Enemy e in enemies)
            {
                e.Update(gameTime, player);
            }

            foreach (SceneObject s in gameObjects){
                //s.Update(gameTime);
            }

            player.Update(gameTime);

            base.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
            foreach (Enemy e in enemies)
                e.Draw(gameTime);
            player.Draw(gameTime);
        }

        /// <summary>
        /// Searches for all possible collisions between relevant objects
        /// collisions which destroy a sceneobject put it in a corpse list
        /// which allows the object to complete its animation without affecting game loop
        /// </summary>
        protected void CheckCollisions()
        {
            List<Enemy> enemiesToRemove = new List<Enemy>();
            List<Projectile> enemyObjectsToRemove = new List<Projectile>();

            // compares all enemy projectiles to scenery, player
            foreach (Projectile p in enemyObjects)
            {
                if (p.GetRectangle().Intersects(player.GetRectangle()) && Math.Abs(p.Z - player.Z) < SceneObject.defaultCollisionWidth)
                {
                    player.takeDamage(p.getDamage());
                    p.takeDamage(100); // objects which hit player are removed
                }

                if (p.GetHitPoints() < 1)
                {
                    enemyObjectsToRemove.Add(p);
                }
            }

            
            foreach (Enemy e in enemies)
            {
                if (player.GetRectangle().Intersects(e.GetRectangle()) && Math.Abs(player.Z - e.Z) < SceneObject.defaultCollisionWidth)
                {
                    if (player.getAttack() == 1)
                    {
                        e.takeDamage(5);
                    }
                }

                if (e.IsDead && e.getX() <= -50.0f) enemiesToRemove.Add(e);
            }
            

            //below loops clean up main object lists
            foreach (Enemy e in enemiesToRemove)
            {
                e.Dispose();
                enemies.Remove(e);
            }

            foreach (Projectile p in enemyObjectsToRemove)
            {
                gameObjects.Remove(p);
                enemyObjects.Remove(p);
                //probably not necessary to add projectile deaths to corpse list
                //projectiles are not currently designed to have a death animation
            }
        }
    }
}
