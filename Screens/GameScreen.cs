using Cstj.Sim.ES.MSimard.Classes;
using Cstj.Sim.Tp2.MSimard.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Tools.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cstj.Sim.ES.MSimard.Screens
{
    public class GameScreen : Screen
    {
        public static bool gameFinish = false;
        private bool win = false;
        private Vaisseau player;
        private Texture2D vaisseauSprite;
        private Texture2D bulletSprite;
        private Texture2D bullet2Sprite;
        private Texture2D ennemi1Sprite;
        private Texture2D explosion;
        private Texture2D asteroidSprite;
        private Texture2D bossSprite;
        private Texture2D bossBullet;
        private SpriteFont text;
        private List<Bullet> bullets;
        private List<Ennemi> ennemis;
        private List<Explosion> explosions;
        private List<Asteroid> asteroids;
        private Random rnd;
        private double shootTimer = 0;
        private double ennemiTimer = 0;
        private int _score = 0;
        private bool enemyGroup = false;
        private Vector2 enemySpeed;
        private Vector2 enemyPosition;
        private int enemyDamage;
        private int enemyDefence;
        private int enemyLife;
        private int enemySpawnTimer;
        private int enemyChemin;
        private int enemyMoney;
        private int enemyGroupCount;
        private int enemyRndCount;
        private int _bossLife;
        private Audio audio;

        public GameScreen(Game game):base(game)
        {
            bullets = new List<Bullet>();
            ennemis = new List<Ennemi>();
            explosions = new List<Explosion>();
            asteroids = new List<Asteroid>();
            rnd = new Random();
            audio = new Audio(game);
        }

        public override void LoadContent()
        {
            bulletSprite = TexturePool.GetTexture(@"Sprites\bullet");
            bullet2Sprite = TexturePool.GetTexture(@"Sprites\bullet2");
            vaisseauSprite = TexturePool.GetTexture(@"Sprites\vaisseau" + PlayerHandler.vaisseauID.ToString());
            ennemi1Sprite = TexturePool.GetTexture(@"Sprites\ennemi1");
            explosion = TexturePool.GetTexture(@"Sprites\explosion");
            asteroidSprite = TexturePool.GetTexture(@"Sprites\asteroid");
            bossSprite = TexturePool.GetTexture(@"Sprites\boss");
            bossBullet = TexturePool.GetTexture(@"Sprites\bossBullet");
            text = FontPool.GetFont(@"Fonts\Pericles14");
            player = new Vaisseau(vaisseauSprite, 1, 1, new Vector2(Game.Window.ClientBounds.Width / 2 - vaisseauSprite.Width/2, Game.Window.ClientBounds.Height - 100), new Vector2(PlayerHandler.vitesse + PlayerHandler.vitesseAjoute + 2));
            if (PlayerHandler.levelSelected * PlayerHandler.difficulty == 5 * PlayerHandler.difficulty)
            {
                enemyDamage = 5 * PlayerHandler.difficulty;
                enemyDefence = 3 * (PlayerHandler.difficulty - 1);
                enemyLife = 100 * PlayerHandler.difficulty;
                enemyMoney = 100 * PlayerHandler.difficulty;
                ennemis.Add(new Boss(Game, bossSprite, bossBullet, new Vector2(Game.Window.ClientBounds.Width / 2 - bossSprite.Width / 2, 0), Vector2.Zero, enemyLife, enemyDefence, enemyDamage, enemyMoney));
            }
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {           
            if (gameFinish)
            {
                Unload();
                gameFinish = false;
            }
            if (this.IsFocused)
            {
                if (player.Active && win == false)
                {

                    #region verifier victoire
                    if ((_score >= 2000 + ((PlayerHandler.difficulty-1) * 500) && PlayerHandler.levelSelected * PlayerHandler.difficulty != 5 * PlayerHandler.difficulty) || (ennemis.Count() == 0 && PlayerHandler.levelSelected * PlayerHandler.difficulty == 5 * PlayerHandler.difficulty))
                    {
                        win = true;
                        if (PlayerHandler.level == PlayerHandler.levelSelected + ((PlayerHandler.difficulty-1)*5))
                        {
                            PlayerHandler.level++;
                            PlayerHandler.skillPoints += 2;
                        }
                    }
                    #endregion

                    shootTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                    ennemiTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (PlayerHandler.levelSelected * PlayerHandler.difficulty != 5 * PlayerHandler.difficulty)
                        AjouterEnnemis();

                    AjouterAsteroid();

                    #region shoot
                    if (KeyboardService.IsKeyDown(Keys.Space) && shootTimer >= 500 - ((PlayerHandler.tir + PlayerHandler.tirAjoute) * 25))
                    {
                        if (PlayerHandler.shootSkill <= 1)
                        {
                            for (int i = -PlayerHandler.shootSkill; i <= PlayerHandler.shootSkill; i++)
                            {
                                Bullet bullet = new Bullet(bulletSprite, new Vector2(player.BoundingBox.X + vaisseauSprite.Width / 2 - bulletSprite.Width / 2 + 10 * i, player.BoundingBox.Y), new Vector2(i, -10));
                                bullets.Add(bullet);
                                //audio.shoot.Play();

                            }
                        }
                        else if (PlayerHandler.shootSkill == 2)
                        {
                            bullets.Add(new Bullet(bulletSprite, new Vector2(player.BoundingBox.X + vaisseauSprite.Width / 2 - bulletSprite.Width / 2, player.BoundingBox.Y), new Vector2(0, -10)));
                            bullets.Add(new Bullet(bullet2Sprite, new Vector2(player.BoundingBox.X + vaisseauSprite.Width / 2 - bulletSprite.Width / 2, player.BoundingBox.Y), new Vector2(-10, 0)));
                            bullets.Add(new Bullet(bullet2Sprite, new Vector2(player.BoundingBox.X + vaisseauSprite.Width / 2 - bulletSprite.Width / 2, player.BoundingBox.Y), new Vector2(10, 0)));
                            bullets.Add(new Bullet(bulletSprite, new Vector2(player.BoundingBox.X + vaisseauSprite.Width / 2 - bulletSprite.Width / 2, player.BoundingBox.Y), new Vector2(0, 10)));
                        }
                        shootTimer = 0;
                    }
                    #endregion

                    #region pause game
                    if ((KeyboardService.IsKeyPressed(Keys.Escape) || KeyboardService.IsKeyPressed(Keys.P)))
                    {
                        ScreenManager.AddScreen<PauseScreen>();
                        PauseScreen.active = true;
                    }
                    #endregion

                    #region update asteroids
                    foreach (var a in asteroids.ToList())
                    {
                        if (a.Active)
                        {
                            a.Update(gameTime);
                        }
                        if (a.BoundingBox.X <= -100 || a.BoundingBox.X >= Game.Window.ClientBounds.Width + 100 || a.BoundingBox.Y <= -100 || a.BoundingBox.Y >= Game.Window.ClientBounds.Height + 100)
                        {
                            a.Active = false;
                            asteroids.Remove(a);
                        }
                        foreach (var b in bullets.ToList())
                        {
                            if (b.BoundingBox.Intersects(a.BoundingBox) && b.Active && a.Active)
                            {
                                a.vie--;
                                if (a.vie <= 0)
                                {
                                    a.Active = false;
                                    explosions.Add(new Explosion(explosion, 5, 5, a.Position, Vector2.Zero, a._scale, Color.White));
                                    //audio.explosion.Play();
                                    asteroids.Remove(a);
                                    if(PlayerHandler.levelSelected * PlayerHandler.difficulty != 5 * PlayerHandler.difficulty)
                                        _score += 50;
                                }
                                bullets.Remove(b);
                            }
                        }
                        if (a.BoundingBox.Intersects(player.BoundingBox))
                        {
                            if (PlayerHandler.defense + PlayerHandler.defenseAjoute >= 10 * PlayerHandler.difficulty)
                                player.life -= 1;
                            else
                                player.life -= 10 * PlayerHandler.difficulty - (PlayerHandler.defense + PlayerHandler.defenseAjoute);
                            a.Active = false;
                            explosions.Add(new Explosion(explosion, 5, 5, a.Position, Vector2.Zero, a._scale, Color.White));
                            //audio.explosion.Play();
                            asteroids.Remove(a);
                        }

                    }
                    #endregion

                    #region update bullets
                    foreach (var a in bullets.ToList())
                    {
                        if (a.Active)
                        {
                            a.Update(gameTime);
                        }
                        if (a.BoundingBox.X <= -100 || a.BoundingBox.X >= Game.Window.ClientBounds.Width + 100 || a.BoundingBox.Y <= -100 || a.BoundingBox.Y >= Game.Window.ClientBounds.Height + 100)
                        {
                            a.Active = false;
                            bullets.Remove(a);
                        }
                        foreach (var e in ennemis.ToList())
                        {
                            if (a.BoundingBox.Intersects(e.BoundingBox) && e.Active && a.Active)
                            {
                                int degats = PlayerHandler.attaque + PlayerHandler.attaqueAjoute - e.defense;
                                if (degats <= 0)
                                    degats = 1;
                                e.life -= degats;
                                if (e.life <= 0)
                                {
                                    Explosion newExplosion = new Explosion(explosion, 5, 5, e.Position, Vector2.Zero, 0.5f, Color.White);
                                    explosions.Add(newExplosion);
                                    //audio.explosion.Play();
                                    _score += 100;
                                }
                                a.Active = false;
                                bullets.Remove(a);
                            }
                        }
                    }
                    #endregion

                    #region update ennemis
                    foreach (var a in ennemis.ToList())
                    {
                        a.Update(gameTime);
                        foreach (var b in a.bullets.ToList())
                        {
                            if (b.BoundingBox.Intersects(player.BoundingBox) && b.Active)
                            {
                                int degats = a.damage - player.defense;
                                if (degats <= 0)
                                    degats = 1;
                                player.life -= degats;
                                if (player.life <= 0)
                                    player.life = 0;
                                b.Active = false;
                                bullets.Remove(b);
                            }
                        }

                        if (a.BoundingBox.X <= -100 || a.BoundingBox.X >= Game.Window.ClientBounds.Width + 100 || a.BoundingBox.Y <= -100 || a.BoundingBox.Y >= Game.Window.ClientBounds.Height + 100)
                        {
                            a.Active = false;
                            ennemis.Remove(a);
                        }

                        if (player.BoundingBox.Intersects(a.BoundingBox) && a.Active && player.Active && !(a is Boss))
                        {
                            a.Active = false;
                            a.canShoot = false;
                            Explosion newExplosion = new Explosion(explosion, 5, 5, a.Position, Vector2.Zero, 0.5f, Color.White);
                            explosions.Add(newExplosion);
                            //audio.explosion.Play();
                            int degats = (a.damage - player.defense) * 3;
                            if (degats <= 0)
                                degats = 3;
                            player.life -= degats;
                            if (player.life <= 0)
                                player.life = 0;
                        }
                        else if (player.BoundingBox.Intersects(a.BoundingBox) && a.Active && player.Active && a is Boss)
                        {
                            player.Active = false;
                        }
                        if (a.Active == false && a.bullets.Count() == 0)
                            ennemis.Remove(a);
                    }
                    #endregion

                    #region update explosions
                    foreach (var a in explosions.ToList())
                    {
                        if (a.Active)
                        {
                            a.Update(gameTime);
                        }
                        else
                        {
                            explosions.Remove(a);
                        }
                    }
                    #endregion

                    #region update player
                    if (player.Active)
                        player.Update(gameTime, Game.Window.ClientBounds);
                    if (player.life <= 0 && player.Active)
                    {
                        Explosion newExplosion = new Explosion(explosion, 5, 5, player.Position, Vector2.Zero, 0.7f, Color.White);
                        explosions.Add(newExplosion);
                        //audio.explosion.Play();
                        player.Active = false;
                    }
                    #endregion

                }
                else if (player.Active == false)
                {
                    #region recommencer
                    if (KeyboardService.IsKeyPressed(Keys.Enter))
                    {
                        Unload();
                        ScreenManager.AddScreen<GameScreen>();
                    }
                    #endregion
                }
                else
                {
                    #region continuer
                    if (KeyboardService.IsKeyPressed(Keys.Enter))
                    {
                        Unload();
                        FondEtoile.gameFinish = true;
                        ScreenManager.AddScreen<WorldMapScreen>();
                    }
                    #endregion
                }
                base.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            if (player.Active && win == false)
            {
                #region dessiner asteroids
                foreach (var a in asteroids)
                {
                    if (a.Active)
                        a.Draw(SpriteBatch);
                }
                #endregion

                #region dessiner bullets
                foreach (var a in bullets)
                {
                    if (a.Active)
                        a.Draw(SpriteBatch);
                }
                #endregion

                #region dessiner ennemis
                foreach (var a in ennemis)
                {
                    a.Draw(SpriteBatch);
                }
                #endregion

                #region dessiner joueur
                if (player.Active)
                    player.Draw(SpriteBatch);
                #endregion

                #region dessiner explosions
                foreach (var a in explosions)
                {
                    if (a.Active)
                        a.Draw(SpriteBatch);
                }
                #endregion

            }

            else if (player.Active == false)
            {
                SpriteBatch.DrawString(text, "Vous avez perdu!", new Vector2(300, 380), Color.White);
                SpriteBatch.DrawString(text, "Appuyez sur enter pour recommencer", new Vector2(200, 400), Color.White);
            }
            else
            {
                SpriteBatch.DrawString(text, "Vous avez gagne!", new Vector2(300, 380), Color.White);
                SpriteBatch.DrawString(text, "Appuyez sur enter pour continuer", new Vector2(200, 400), Color.White);
            }

            #region ecrire stats
            if (PlayerHandler.levelSelected * PlayerHandler.difficulty == 5 * PlayerHandler.difficulty)
            {
                foreach (var a in ennemis)
                {
                    _bossLife = a.life;
                }
                if (ennemis.Count() == 0)
                    _bossLife = 0;
                SpriteBatch.DrawString(text, "Boss: " + _bossLife, Vector2.Zero, Color.White);
                SpriteBatch.DrawString(text, "Vie: " + player.life, new Vector2(0, 20), Color.Red);
                SpriteBatch.DrawString(text, PlayerHandler.money + "$", new Vector2(0, 40), Color.LightGreen);
            }
            else
            {
                SpriteBatch.DrawString(text, "Score: " + _score, Vector2.Zero, Color.White);
                SpriteBatch.DrawString(text, "Vie: " + player.life, new Vector2(0, 20), Color.Red);
                SpriteBatch.DrawString(text, PlayerHandler.money + "$", new Vector2(0, 40), Color.LightGreen);
            }
            #endregion

            SpriteBatch.End();
        }

        public void AjouterEnnemis()
        {
            if (enemyGroup == false && ennemiTimer >= 5000 - ((PlayerHandler.difficulty-1)*2000))
            {
                if (PlayerHandler.levelSelected == 1)
                    enemyChemin = rnd.Next(1, 4) * 2;
                else if (PlayerHandler.levelSelected == 2)
                    enemyChemin = rnd.Next(0, 3) * 2 + 1;
                else if (PlayerHandler.levelSelected == 3)
                    enemyChemin = rnd.Next(7, 9);
                else if (PlayerHandler.levelSelected == 4)
                    enemyChemin = rnd.Next(1, 9);

                if(enemyChemin == 2 || enemyChemin == 4 || enemyChemin == 6)
                {
                    enemyPosition = new Vector2(Game.Window.ClientBounds.Width + 50, rnd.Next(0, Game.Window.ClientBounds.Height / 3*2));
                    enemySpeed = new Vector2(rnd.Next(1, 4), 0);
                }
                else if (enemyChemin == 1 || enemyChemin == 3 || enemyChemin == 5)
                {
                    enemyPosition = new Vector2(-50,rnd.Next(0, Game.Window.ClientBounds.Height/3));
                    enemySpeed = new Vector2(rnd.Next(1, 4), 0);
                }
                else if (enemyChemin == 7)
                {
                    enemyPosition = new Vector2(rnd.Next(100, Game.Window.ClientBounds.Width-100), -50);
                    enemySpeed = new Vector2(0, rnd.Next(1, 4));
                }
                else if (enemyChemin == 8)
                {
                    enemyPosition = new Vector2(rnd.Next(0, Game.Window.ClientBounds.Width - ennemi1Sprite.Width), -50);
                    enemySpeed = new Vector2(0, rnd.Next(1, 4));
                }
                enemyDamage = PlayerHandler.difficulty * 3 + 3;
                enemyDefence = (PlayerHandler.difficulty - 1)*3;
                enemyLife = PlayerHandler.difficulty * 3;
                enemySpawnTimer = rnd.Next(800, 1201);
                enemyMoney = PlayerHandler.difficulty*10;
                enemyRndCount = rnd.Next(3,8);
                ennemiTimer = 0;
                enemyGroupCount = 0;
                enemyGroup = true;
            }

            if (enemyGroup)
            {
                if (ennemiTimer >= enemySpawnTimer)
                {
                    if(enemyChemin == 8)
                        ennemis.Add(new Ennemi(Game, ennemi1Sprite, bulletSprite, new Vector2(rnd.Next(0, Game.Window.ClientBounds.Width-ennemi1Sprite.Width),-ennemi1Sprite.Height), enemySpeed, enemyLife, enemyDefence, enemyDamage, enemyMoney, rnd.Next(800, 1401), enemyChemin));
                    else
                        ennemis.Add(new Ennemi(Game, ennemi1Sprite, bulletSprite, enemyPosition, enemySpeed, enemyLife, enemyDefence, enemyDamage, enemyMoney, rnd.Next(800,1401), enemyChemin));
                    ennemiTimer = 0;
                    enemyGroupCount++;
                    if (enemyGroupCount >= enemyRndCount)
                    {
                        enemyGroup = false;
                    }
                }
            }
        }

        public void AjouterAsteroid()
        {
            if (asteroids.Count <= 5*PlayerHandler.difficulty)
            {
                int direction = rnd.Next(0, 3);
                if (direction == 0)
                    asteroids.Add(new Asteroid(asteroidSprite, 7, 21, new Vector2(-100, rnd.Next(0, Game.Window.ClientBounds.Height)), new Vector2(rnd.Next(1, 4), rnd.Next(-2, 3)),0.5f,143));
                else if (direction == 1)
                    asteroids.Add(new Asteroid(asteroidSprite, 7, 21, new Vector2(Game.Window.ClientBounds.Width, rnd.Next(0, Game.Window.ClientBounds.Height)), new Vector2(-rnd.Next(1, 4), rnd.Next(-2, 3)), 0.6f, 143));
                else
                    asteroids.Add(new Asteroid(asteroidSprite, 7, 21, new Vector2(rnd.Next(0, Game.Window.ClientBounds.Width), -100), new Vector2(rnd.Next(-2, 3), rnd.Next(1, 4)), 0.7f, 143));
            
            }
        }

    }
}
