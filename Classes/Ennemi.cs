using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Tools.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cstj.Sim.ES.MSimard.Classes
{
    public class Ennemi : Sprite
    {
        public int life;
        public int defense;
        public int damage;
        protected int _money;
        protected int _bulletDelay;
        protected double shootTimer = 0;
        public List<Bullet> bullets;
        protected Texture2D _bulletTexture;
        public bool canShoot = true;
        protected Game _game;
        protected float positionCount = 0;
        protected double positionTimer = 0;
        protected int _chemin;

        public Ennemi(Game game,Texture2D texture,Texture2D bulletTexture, Vector2 position, Vector2 speed, int lif, int def, int dmg,int money, int bulletDelay,int chemin) : base(texture,position,speed)
        {
            bullets = new List<Bullet>();
            life = lif;
            defense = def;
            damage = dmg;
            _money = money;
            _bulletTexture = bulletTexture;
            _bulletDelay = bulletDelay;
            _game = game;
            _chemin = chemin;
        }

        public override void Update(GameTime gameTime)
        {
            shootTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            positionTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (shootTimer >= _bulletDelay && canShoot)
            {
                Bullet newBullet = new Bullet(_bulletTexture, new Vector2(_position.X + (int)(_texture.Width/2) + (int)(_bulletTexture.Width/2),_position.Y), new Vector2(0, 10));
                bullets.Add(newBullet);
                shootTimer = 0;
            }
            if (life <= 0 && _active)
            {
                _active = false;
                canShoot = false;
                PlayerHandler.money += _money;
            }

            foreach (var a in bullets.ToList())
            {
                a.Update(gameTime);
                if (a.BoundingBox.Y >= _game.Window.ClientBounds.Height + 100)
                    bullets.Remove(a);
            }

            genererChemin();
 	        
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var a in bullets)
            {
                if(a.Active)
                    a.Draw(spriteBatch, Color.Red);
            }
            if(_active)
                base.Draw(spriteBatch);
        }

        public virtual void genererChemin()
        {
            if (_chemin == 1)
            {
                _position.Y += (float)Math.Cos(positionCount);
                _position.X += _speed.X;
                if (positionTimer >= 100)
                {
                    positionCount += 0.1f;
                    positionTimer -= 100;
                }
            }
            else if (_chemin == 2)
            {
                _position.Y += (float)Math.Cos(positionCount);
                _position.X -= _speed.X;
                if (positionTimer >= 100)
                {
                    positionCount += 0.1f;
                    positionTimer -= 100;
                }
            }
            else if (_chemin == 3)
            {
                _position.Y += (float)Math.Cos(positionCount) * (float)Math.Cos(positionCount);
                _position.X += _speed.X;
                if (positionTimer >= 100)
                {
                    positionCount += 0.1f;
                    positionTimer -= 100;
                }
            }
            else if (_chemin == 4)
            {
                _position.Y += (float)Math.Cos(positionCount) * (float)Math.Cos(positionCount);
                _position.X -= _speed.X;
                if (positionTimer >= 100)
                {
                    positionCount += 0.1f;
                    positionTimer -= 100;
                }
            }
            else if (_chemin == 5)
            {
                _position.Y += (float)Math.Cos(positionCount) + (float)Math.Cos(positionCount);
                _position.X += _speed.X;
                if (positionTimer >= 100)
                {
                    positionCount += 0.1f;
                    positionTimer -= 100;
                }
            }
            else if (_chemin == 6)
            {
                _position.Y += (float)Math.Cos(positionCount) + (float)Math.Cos(positionCount);
                _position.X -= _speed.X;
                if (positionTimer >= 100)
                {
                    positionCount += 0.1f;
                    positionTimer -= 100;
                }
            }
            else if (_chemin == 7)
            {
                _position.Y += _speed.Y;
                _position.X += (float)Math.Cos(positionCount) + (float)Math.Cos(positionCount);
                if (positionTimer >= 100)
                {
                    positionCount += 0.1f;
                    positionTimer -= 100;
                }
            }
            else
            {
                _position += _speed;
            }
        }
    }
}
