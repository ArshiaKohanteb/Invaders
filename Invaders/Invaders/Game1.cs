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

namespace Invaders
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //Difficulty 1 = .05
        //Difficulty 2 = .1
        //Difficulty 3 = 1

        enum GameState
        {
            mainMenu,
            game,
            gameOver,
            pause,
            restart
        }

        GameState gameState;

        #region Variables
        float Difficulty = 1;
        float BulletSpeed = 7;
        float EnemySpeed = 7;
        float Hardness = .05f;
        float EnemySpeedX = 1;
        float bulletDelayChange = 0;
        int ranNum;
        int speedX = 5;
        int EnemY = 70;
        float TestVal = 0;
        bool BulletSpray = false;
        bool GameIsRunning = true;
        bool DeveloperMode = false;
        bool Win = false;

        Vector2 BulletPos;
        List<Bullet> bullets;
        List<Bullet> Enemybullets;
        Texture2D BulletPic;
        Texture2D BulletPic2;
        Texture2D Picture;
        Texture2D pixel;
        SpriteFont font;
        SpriteFont font2;
        SpriteFont font3;
        List<Base> bases;
        List<Enemy> enemies;
        KeyboardState lastKs;
        TimeSpan ElapsedTime;
        TimeSpan ShownElapsedTime;
        TimeSpan ShotDelay = TimeSpan.FromSeconds(2);
        Rectangle[] enemyRects;
        KeyboardState ks;
        Player player;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MouseState ms;
        Random gen = new Random();
        bool pause = false;
        List<Frame> baseFrames;
        #endregion
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Picture = Content.Load<Texture2D>("Difficulty");

            font = Content.Load<SpriteFont>("SpriteFont1");
            font2 = Content.Load<SpriteFont>("Font2");
            font3 = Content.Load<SpriteFont>("Font3");
            bullets = new List<Bullet>();
            Enemybullets = new List<Bullet>();
            enemies = new List<Enemy>();
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
            BulletPic = Content.Load<Texture2D>("bullet");
            BulletPic2 = Content.Load<Texture2D>("enemy bullet");
            Texture2D SpriteSheet = Content.Load<Texture2D>("sheet");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(SpriteSheet, new Vector2(100, 450), Color.White, new Rectangle(141, 452, 28, 14));
            baseFrames = new List<Frame>();
            baseFrames.Add(new Frame(new Rectangle(316, 213, 44, 32), new Vector2(0, 0)));
            baseFrames.Add(new Frame(new Rectangle(480, 210, 44, 32), new Vector2(0, 0)));
            ranNum = gen.Next(0, 500);
            if (ranNum > 250)
            {
                baseFrames.Add(new Frame(new Rectangle(373, 211, 44, 32), new Vector2(0, 0)));
            }
            if (ranNum < 250)
            {
                baseFrames.Add(new Frame(new Rectangle(428, 210, 44, 32), new Vector2(0, 0)));
            }
            bases = new List<Base>();
            bases.Add(new Base(SpriteSheet, new Vector2(150, 400), baseFrames));
            bases.Add(new Base(SpriteSheet, new Vector2(350, 400), baseFrames));
            bases.Add(new Base(SpriteSheet, new Vector2(550, 400), baseFrames));

            enemyRects = new Rectangle[]
            {
                new Rectangle(7, 225, 16, 16),
                new Rectangle(40,225,16,16),
                new Rectangle(75,225,22,16),
                new Rectangle(107,225,22,16),
                new Rectangle(147,226,24,16)
            };
            for (int i = 2; i < 12; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    enemies.Add(new Enemy(SpriteSheet, new Vector2(30 * i, EnemY + (30 * j)), Color.White, enemyRects[j % enemyRects.Length]));
                }
            }

        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            Texture2D SpriteSheet = Content.Load<Texture2D>("sheet");
            ks = Keyboard.GetState();
            ms = Mouse.GetState();
            if (ks.IsKeyDown(Keys.F))
            {
                DeveloperMode = false;
            }
            
            if (ks.IsKeyDown(Keys.Z))
            {
                DeveloperMode = true;
            }
           
            if (gameState == GameState.restart)
            {
                bulletDelayChange = 0;
                GameIsRunning = true;

                enemies.Clear();
                Enemybullets.Clear();
                bases.Clear();                
       
                EnemySpeedX = 1;
                bullets.Clear();

                for (int i = 2; i < 12; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        enemies.Add(new Enemy(SpriteSheet, new Vector2(30 * i, EnemY + (30 * j)), Color.White, enemyRects[j % enemyRects.Length]));
                    }
                }

                bases.Add(new Base(SpriteSheet, new Vector2(150, 400), baseFrames));
                bases.Add(new Base(SpriteSheet, new Vector2(350, 400), baseFrames));
                bases.Add(new Base(SpriteSheet, new Vector2(550, 400), baseFrames));

                gameState = GameState.game;                
            }
            if (!GameIsRunning && gameState != GameState.mainMenu)
            {
                gameState = GameState.gameOver;
            }
            if (gameState == GameState.gameOver)
            {
                if (ks.IsKeyDown(Keys.R) && lastKs.IsKeyUp(Keys.R))
                {
                    gameState = GameState.mainMenu;                    
                }
            }
            #region Main Menu
            if (gameState == GameState.mainMenu)
            {
                if (ms.X > 40 && ms.X < 400 && ms.Y > 80 && ms.Y < 180 && ms.LeftButton == ButtonState.Pressed)
                {                  
                    Difficulty = 1;
                    gameState = GameState.restart;
                }
                else if (ms.X > 80 && ms.X < 360 && ms.Y > 85 && ms.Y < 370 && ms.LeftButton == ButtonState.Pressed)
                {
                    Difficulty = 2;
                    gameState = GameState.restart;
                }
                else if (ms.X > 430 && ms.X < 660 && ms.Y > 130 && ms.Y < 380 && ms.LeftButton == ButtonState.Pressed)
                {
                    Difficulty = 3;
                    gameState = GameState.restart;
                }
                if (Difficulty == 1)
                {
                    Hardness = .05f;
                    EnemySpeed = 5;
                    BulletSpeed = 7;
                }
                else if (Difficulty == 2)
                {
                    Hardness = .1f;
                    EnemySpeed = 6;
                    BulletSpeed = 6;
                }
                else if (Difficulty == 3)
                {
                    Hardness = .75f;
                    EnemySpeed = 7;
                    BulletSpeed = 5;
                }

            }
            #endregion
            #region Game

            if (gameState == GameState.game)
            {
                if (ks.IsKeyDown(Keys.Escape) && lastKs.IsKeyUp(Keys.Escape))
                {
                    gameState = GameState.pause;
                }

                if (DeveloperMode == true)
                {
                    if (ks.IsKeyDown(Keys.P))
                    {
                        BulletSpray = true;
                    }
                    if (ks.IsKeyUp(Keys.P))
                    {
                        BulletSpray = false;
                    }
                    if (ks.IsKeyDown(Keys.R))
                    {
                        GameIsRunning = true;
                    }
                }
                ElapsedTime += gameTime.ElapsedGameTime;
                if (GameIsRunning == true)
                {
                    ShownElapsedTime += gameTime.ElapsedGameTime;
                }

                if (enemies.Count > 0 && ElapsedTime > ShotDelay - TimeSpan.FromMilliseconds(bulletDelayChange * 100))
                {
                    ElapsedTime = TimeSpan.Zero;
                    int randomEnemyIndex = gen.Next(enemies.Count);
                    Enemy toShoot = enemies[randomEnemyIndex];
                    Bullet enemyBullet = new Bullet(BulletPic2, toShoot.Position + new Vector2(player.Hitbox.Width / 2 - BulletPic.Width / 2, 0), Color.White, new Rectangle(0, 0, BulletPic.Width, BulletPic.Height), -EnemySpeed);
                    Enemybullets.Add(enemyBullet);
                }

                #region EnemyBullets

                for (int k = 0; k < Enemybullets.Count(); k++)
                {
                    for (int j = 0; j < bases.Count; j++)
                    {
                        if (Enemybullets.Count > 0)
                        {
                            if (bases[j].BaseHit(Enemybullets[k].Hitbox))
                            {
                                bases[j].damage++;
                                if (bases[j].damage > 4)
                                {
                                    bases.Remove(bases[j]);
                                }
                                
                                
                                    Enemybullets.RemoveAt(k);
                                

                                k--;
                            }
                            else if (Enemybullets[k].Hitbox.Intersects(player.Hitbox))
                            {
                                GameIsRunning = false;
                                if (GameIsRunning == false)
                                {

                                }

                                Enemybullets.RemoveAt(k);

                                k--;
                            }
                        }
                    }
                }

                #endregion EnemyBullets
                if (enemies.Count() <= 0)
                {
                    GameIsRunning = false;
                    Win = true;
                    return;
                }

                for (int i = 0; i < enemies.Count(); i++)
                {
                    if (enemies[i].Y > 400)
                    {
                        GameIsRunning = false;
                        return;
                    }

                    enemies[i].X += EnemySpeedX;

                    if (enemies[i].X + enemies[i].Hitbox.Width > GraphicsDevice.Viewport.Width || enemies[i].X < 0)
                    {
                        EnemySpeedX *= -1;
                        for (int j = 0; j < enemies.Count(); j++)
                        {
                            enemies[j].Y += 10;
                        }
                    }

                    for (int j = 0; j < bullets.Count(); j++)
                    {
                        if (enemies[i].Hitbox.Intersects(bullets[j].Hitbox))
                        {
                            enemies.RemoveAt(i);

                            bullets.RemoveAt(j);

                            j--;
                            i--;

                            BulletSpeed += Hardness;
                            if (EnemySpeedX < 0)
                            {
                                EnemySpeedX -= Hardness;
                            }
                            else
                            {
                                EnemySpeedX += Hardness;
                            }
                            bulletDelayChange = Math.Abs(EnemySpeedX);

                            break;
                        }
                    }
                }

                BulletPos = new Vector2(player.X, 450 - BulletPic.Height);
                if (player.X < 0)
                {
                    player.X = GraphicsDevice.Viewport.Width;
                }

                if (player.X > GraphicsDevice.Viewport.Width)
                {
                    player.X = 0;
                }
                if (bullets.Count > 0 && bullets[0].Y <= 0 - BulletPic.Height && DeveloperMode == false)
                {
                    bullets.Clear();
                }

                for (int i = 0; i < bullets.Count; i++)
                {
                    for (int j = 0; j < bases.Count; j++)
                    {

                        if (bases[j].BaseHit(bullets[i].Hitbox))
                        {
                            bases[j].damage++;
                            if (bases[j].damage > 4)
                            {
                                bases.Remove(bases[j]);
                            }

                            bullets.Remove(bullets[i]);
                            break;
                        }
                    }
                }
                if (ks.IsKeyDown(Keys.Space) && lastKs.IsKeyUp(Keys.Space) && bullets.Count == 0)
                {
                    Bullet bullet = new Bullet(BulletPic, BulletPos + new Vector2(player.Hitbox.Width / 2 - BulletPic.Width / 2, 0), Color.White, new Rectangle(0, 0, BulletPic.Width, BulletPic.Height), BulletSpeed);
                    bullets.Add(bullet);
                }
                if (BulletSpray)
                {
                    Bullet bullet = new Bullet(BulletPic, BulletPos + new Vector2(player.Hitbox.Width / 2 - BulletPic.Width / 2, 0), Color.White, new Rectangle(0, 0, BulletPic.Width, BulletPic.Height), BulletSpeed);
                    bullets.Add(bullet);
                }
                if (ks.IsKeyDown(Keys.A) || ks.IsKeyDown(Keys.Left))
                {
                    player.X -= speedX;
                }
                if (ks.IsKeyDown(Keys.D) || ks.IsKeyDown(Keys.Right))
                {
                    player.X += speedX;
                }


                for (int i = 0; i < bullets.Count; i++)
                {
                    bullets[i].Update(gameTime);
                }

                for (int i = 0; i < Enemybullets.Count; i++)
                {
                    Enemybullets[i].Update(gameTime);
                }


                for (int i = 0; i < bases.Count; i++)
                {
                    bases[i].Update(gameTime);
                }

            }

            #endregion
            #region Pause
            if (gameState == GameState.pause)
            {
                if (pause)
                {
                    if (ks.IsKeyDown(Keys.Escape) && lastKs.IsKeyUp(Keys.Escape))
                    {
                        gameState = GameState.game;
                        pause = false;
                    }
                }
                else
                {
                    pause = true;
                }
            }
            #endregion
            lastKs = ks;
            base.Update(gameTime);

        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            if (gameState == GameState.mainMenu)
            {
                spriteBatch.Draw(Picture, new Vector2(0, 0), Color.White);
            }
            if (gameState == GameState.pause)
            {
                spriteBatch.DrawString(font2, $"Paused", new Vector2(225, 200), Color.White);
            }
            else if (gameState == GameState.gameOver && Win == false)
            {
                spriteBatch.DrawString(font2, $"Game Over", new Vector2(225, 200), Color.White);
                spriteBatch.DrawString(font3, $"Press R to restart", new Vector2(320, 280), Color.White);
            }
            else if (gameState == GameState.gameOver && Win == true)
            {
                spriteBatch.DrawString(font2, $"You Win!", new Vector2(225, 200), Color.White);
                spriteBatch.DrawString(font3, $"Press R to restart", new Vector2(320, 280), Color.White);
            }
            else if (gameState == GameState.game)
            {
                if (GameIsRunning)
                {
                    for (int i = 0; i < bases.Count(); i++)
                    {
                        //whatever is drawn last, is drawn on top
                        bases[i].Draw(spriteBatch);
                    }
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].Draw(spriteBatch);
                    }
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        bullets[i].Draw(spriteBatch);
                        //spriteBatch.Draw(pixel, bullets[i].Hitbox, Color.Red);
                    }
                    for (int i = 0; i < Enemybullets.Count; i++)
                    {
                        Enemybullets[i].Draw(spriteBatch);
                    }
                }

                player.Draw(spriteBatch);
                spriteBatch.DrawString(font, ShownElapsedTime.ToString(), new Vector2(350, 0), Color.White);

                //spriteBatch.Draw(pixel, bases[0].Hitbox, Color.Red);
            }
            if (DeveloperMode)
                spriteBatch.DrawString(font, $"({ms.X}, {ms.Y})", Vector2.Zero, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
