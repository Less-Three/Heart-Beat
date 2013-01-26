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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Background scrollingBackground;
        Player player;  // the player
        List<SceneObject> gameObjects;  // list of all game objects - updated in game loops

        //the following lists are used only to simplify collision detection
        List<Enemy> enemies;
        List<Projectile> enemyObjects; // list of all enemy projectiles
        List<Scenery> scenery; // list of all scenery objects
        List<Item> items; // list of all pick up items
        List<Projectile> playerObjects; // list of all player projectiles including punches
        List<SceneObject> corpses; // list of all SceneObjects which are playing a death animation

        public GameHeartBeat()
        {
            Window.Title = "Heart Beat";
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            scrollingBackground = new Background(this);
            player = new Player(this);
            gameObjects = new List<SceneObject>();
            enemies = new List<Enemy>();
            enemyObjects = new List<Projectile>();
            scenery = new List<Scenery>();
            items = new List<Item>();
            playerObjects = new List<Projectile>();
            corpses = new List<SceneObject>();

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

            scrollingBackground.Initialize(Content, "Background/city_bg", GraphicsDevice.Viewport.Width);
            player.Initialize(Content, "Player/Heartbeat_Stand01");
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
                this.Exit();

            scrollingBackground.Update(gameTime);
            player.Update(gameTime);
            foreach (SceneObject s in gameObjects){
                s.Update(gameTime);
            }
            List<SceneObject> corpsesToRemove = new List<SceneObject>();
            foreach (SceneObject s in corpses)
            {
                s.Update(gameTime);
                if (s.getHitPoints() < 1)
                {
                    corpsesToRemove.Add(s);
                }
            }
            foreach (SceneObject corpse in corpsesToRemove)
            {
                corpses.Remove(corpse);
            }
            corpsesToRemove.Clear();
            base.Update(gameTime);
            
        }

        /// <summary>
        /// Searches for all possible collisions between relevant objects
        /// collisions which destroy a sceneobject put it in a corpse list
        /// which allows the object to complete its animation without affecting game loop
        /// </summary>
        protected void checkCollisions(){
            List <Enemy> enemiesToRemove = new List<Enemy>();
            List <Projectile> enemyObjectsToRemove = new List<Projectile>();
            List <Scenery> sceneryToRemove = new List<Scenery>();
            List <SceneObject> itemsToRemove = new List<SceneObject>();
            List <Projectile> playerObjectsToRemove = new List<Projectile>();

            // compares all enemy projectiles to scenery, player
            foreach (Projectile p in enemyObjects)
            {
                if (p.getRectangle().Intersects(player.getRectangle()))
                {
                    player.takeDamage(p.getDamage());
                    p.takeDamage(100); // objects which hit player are removed
                }
                foreach (Scenery s in scenery)
                {
                    if (p.getRectangle().Intersects(s.getRectangle()))
                    {
                        s.takeDamage(1);
                        p.takeDamage(1);
                        if (s.getHitPoints() < 1)
                        {
                            sceneryToRemove.Add(s);
                        }
                    }
                }
                if (p.getHitPoints() < 1) 
                {
                    enemyObjectsToRemove.Add(p);
                }
            }

            // compares all player projectiles to scenery, enemies
            foreach (Projectile p in playerObjects)
            {
                foreach (Enemy e in enemies)
                {
                    if (p.getRectangle().Intersects(e.getRectangle()))
                    {
                        e.takeDamage(p.getDamage());
                        p.takeDamage(1); //objects which hit enemy "might" be removed
                        
                        if (e.getHitPoints() < 1)
                        {
                            enemiesToRemove.Add(e);
                        }
                    }
                }
                foreach (Scenery s in scenery)
                {
                    if (p.getRectangle().Intersects(s.getRectangle()))
                    {
                        s.takeDamage(1);
                        if (s.getHitPoints() < 1)
                        {
                            sceneryToRemove.Add(s);
                        }
                    }
                }
                if (p.getHitPoints() < 1) 
                {
                    playerObjectsToRemove.Add(p);
                }
            }

            //compares all pickup items with player
            foreach (Item i in items)
            {
                if (i.getRectangle().Intersects(player.getRectangle()))
                {
                    itemsToRemove.Add(i);
                }
            }

            //below loops clean up main object lists
            foreach (Enemy e in enemiesToRemove)
            {
                gameObjects.Remove(e);
                enemies.Remove(e);
                corpses.Add(e);
            }
            foreach (Projectile p in enemyObjectsToRemove)
            {
                gameObjects.Remove(p);
                enemyObjects.Remove(p);
                //probably not necessary to add projectile deaths to corpse list
                //projectiles are not currently designed to have a death animation
            }
            foreach (Scenery s in sceneryToRemove)
            {
                gameObjects.Remove(s);
                scenery.Remove(s);
                corpses.Add(s); // destroyed scenery could have death animation
            }
            foreach (Item i in itemsToRemove)
            {
                gameObjects.Remove(i);
                items.Remove(i);
                corpses.Add(i); // item pickups could play a short animation before being removed
            }
            foreach (Projectile p in playerObjectsToRemove)
            {
                gameObjects.Remove(p);
                playerObjects.Remove(p);
                //not necessary to add projectile to death animation list
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            scrollingBackground.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
