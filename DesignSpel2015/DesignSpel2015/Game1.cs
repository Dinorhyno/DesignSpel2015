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

namespace DesignSpel2015
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Vector2 playerPos;
        public Texture2D playerTex;

        public Vector2 spritePosition;
        int score;
        List<BulletReg> Bullet1;
        Texture2D bullet1Tex;
        int shotTimerReg = 10;
        float Ammo = 32;
        int Mag1 = 8;
        int reloadTime1 =  50;
        int punchTimer;
        bool reload1 = false;
        public List<Smoke> smoke;
        Texture2D smokeTex;

        List<Punch> punch;
        Texture2D pt;

        List<EnemyType1> Enemy1;
        Texture2D enemyTex;

        Texture2D Back1Tex;

        int VSpeed = 2;
        int HSpeed = 5;

        KeyboardState ks;
        KeyboardState prevKs;

        Texture2D texHpBar;

        Camera2D camera;

        public SpriteFont GameFont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 900;
            graphics.PreferredBackBufferWidth = 1600;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            playerPos = new Vector2(100, 400);
            Bullet1 = new List<BulletReg>();
            Enemy1 = new List<EnemyType1>();
            smoke = new List<Smoke>();
            punch = new List<Punch>();
            camera = new Camera2D(GraphicsDevice.Viewport);
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
            playerTex = Content.Load<Texture2D>("PimpBox");
            bullet1Tex = Content.Load<Texture2D>("Bullet");
            Back1Tex = Content.Load<Texture2D>("BcgStreet");
            enemyTex = Content.Load<Texture2D>("EnemyBox");
            GameFont = Content.Load<SpriteFont>("SpriteFont1");
            smokeTex = Content.Load<Texture2D>("smoke");
            pt = Content.Load<Texture2D> ("hitBox");
            texHpBar = Content.Load<Texture2D>("Hpbar");
            Enemy1.Add(new EnemyType1(new Vector2(1600, 400), enemyTex, playerPos));
            Enemy1.Add(new EnemyType1(new Vector2(4000, 400), enemyTex, playerPos));
            
            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            prevKs = ks;
            ks = Keyboard.GetState();
            
            //movement
            if (ks.IsKeyDown(Keys.W) && playerPos.Y >= 305)
            {
                smoke.Add(new Smoke(new Vector2(playerPos.X - smokeTex.Width + 10, playerPos.Y + playerTex.Height - smokeTex.Height), smokeTex));
                playerPos.Y -= VSpeed;
                playerPos.X -= 0.5f;
            }

            if (ks.IsKeyDown(Keys.A) && playerPos.X >= 0)
            {
                smoke.Add(new Smoke(new Vector2(playerPos.X - smokeTex.Width + 10, playerPos.Y + playerTex.Height - smokeTex.Height + 5), smokeTex));
                playerPos.X-= HSpeed;
            }

            if (ks.IsKeyDown(Keys.S) && playerPos.Y <= (900 - playerTex.Height))
            {
                playerPos.Y += VSpeed;
                smoke.Add(new Smoke(new Vector2(playerPos.X - smokeTex.Width + 10, playerPos.Y + playerTex.Height - smokeTex.Height + 5), smokeTex));
                if (playerPos.X >= 0)playerPos.X -= 0.5f;
            }
            if (ks.IsKeyDown(Keys.D))
            {
                smoke.Add(new Smoke(new Vector2(playerPos.X - smokeTex.Width + 5, playerPos.Y + playerTex.Height - smokeTex.Height + 5), smokeTex));
                playerPos.X += HSpeed;
            }
            // combat
            shotTimerReg--; 
            punchTimer--;
            if (ks.IsKeyDown(Keys.J) && shotTimerReg < 0 && reload1 == false && Mag1 >= 1)
            {
                Bullet1.Add(new BulletReg(new Vector2(playerPos.X + playerTex.Width, playerPos.Y + playerTex.Height/2), bullet1Tex));
                shotTimerReg = 10;
                Mag1--;
            }
            if (Ammo == 0)
            {
                reload1 = false;
            }

            if (Mag1 <= 0)
            {
                if (Ammo > 0)
                {
                    reload1 = true;
                    Ammo = Ammo - 8;
                }
            }
            if (reload1 == true && Ammo >= 8)
            {
                Mag1 = 8;
                
                reloadTime1--;
            }
            if (reloadTime1 <= 0)
            {
                reload1 = false;
                reloadTime1 = 50;
            }
            if (ks.IsKeyDown(Keys.K) && prevKs.IsKeyUp(Keys.K) && punchTimer <= 0)
            {
                punch.Add(new Punch(new Vector2( playerPos.X + playerTex.Width + 16, playerPos.Y + (playerTex.Height /2) + 32), pt));
                punchTimer = 25;
            }

            for (int i = 0; i < Bullet1.Count; i++)
            {
                Bullet1[i].update();
                smoke.Add(new Smoke(new Vector2(Bullet1[i].pos.X - smokeTex.Width * 2, Bullet1[i].pos.Y + smokeTex.Height / 2), smokeTex));
                if (Bullet1[i].smokeTimer <= 0) 
                {                  
                    Bullet1[i].smokeTimer = 5;
                }                
                if (Bullet1[i].speed <= 0)
                {
                    Bullet1.RemoveAt(i);
                }
            }

            

            for (int i = 0; i < Enemy1.Count; i++)
            {
                Enemy1[i].update();
                if (Enemy1[i].enemyHp < 1)
                {
                    score += 100;
                    Enemy1.RemoveAt(i);
                }
                else if (Enemy1[i].pos.X - playerPos.X < 800) Enemy1[i].Activated = true;
                for (int j = 0; j < Bullet1.Count; j++)
                {
                    
                    Bullet1[j].update();
                    if (Bullet1[j].shotR.Intersects(Enemy1[i].eR))
                    {
                        Enemy1[i].enemyHp--;
                        Bullet1.RemoveAt(j);
                        break;
                    }
                }
                for (int j = 0; j < punch.Count; j++)
                {
                    if (Enemy1[i].eR.Intersects(punch[j].pR))
                    {
                        Enemy1[i].stunned = true;
                        Enemy1[i].stunTime = 5;
                        if (Enemy1[i].enemyHp >= 0) Enemy1[i].enemyHp -= 5;
                        punch[j].timer = 3;
                        punch.RemoveAt(j);
                    }
                }
            }
            for (int i = 0; i < punch.Count; i++)
            {
                punch[i].Update();
                if (punch[i].timer <= 0) punch.RemoveAt(i);
            }

            for (int i = 0; i < smoke.Count; i++)
            {
                smoke[i].Update();
                if (smoke[i].timer <= 0) smoke.RemoveAt(i);
            }
            camera.Update(gameTime, this);
            camera.shake = true;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
            for (int i = 0; i < 10; i++)
            {
                spriteBatch.Draw(Back1Tex, new Vector2((1600*i) - i, 0), Color.White);
            }
            spriteBatch.Draw(playerTex, playerPos, Color.White);
            for (int i = 0; i < Bullet1.Count; i++)
            {
                Bullet1[i].draw(spriteBatch);
            }
            for (int i = 0; i < smoke.Count; i++)
            {
                smoke[i].draw(spriteBatch);
            }
            for (int i = 0; i < Enemy1.Count; i++)
            {
                Enemy1[i].draw(spriteBatch, GameFont);
            }
            for (int i = 0; i < punch.Count; i++)
            {
                punch[i].draw(spriteBatch);
            }
            spriteBatch.DrawString(GameFont, "Ammo: " + Mag1 + ": " + Ammo, new Vector2(10 + camera.centre.X, 70), Color.Red);
            spriteBatch.DrawString(GameFont, "Score: " + score, new Vector2(10 + camera.centre.X, 92), Color.Red);
            
            if (reload1 == true && Ammo >= 0) spriteBatch.DrawString(GameFont, "Reloading: " + reloadTime1, new Vector2(playerPos.X + 32, playerPos.Y - 32), Color.Red);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
