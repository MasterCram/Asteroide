using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cstj.Sim.ES.MSimard.Classes
{
    public class Boss : Ennemi
    {

        public Boss(Game game,Texture2D texture, Texture2D bulletTexture, Vector2 position, Vector2 speed, int life, int defense, int damage, int money,int bulletDelay=500,int chemin=1)
            : base(game,texture,bulletTexture,position,speed,life,defense,damage,money,bulletDelay,chemin)
        {

        }

        public override void Update(GameTime gameTime)
        {
            shootTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            positionTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (shootTimer >= _bulletDelay && canShoot)
            {
                for (int i = -3; i <= 3; i++)
                {
                    Bullet newBullet = new Bullet(_bulletTexture, new Vector2(_position.X + (int)(_texture.Width / 2) + (int)(_bulletTexture.Width / 2) + 20 * i, _position.Y + _texture.Height), new Vector2(i*4, 10));
                    bullets.Add(newBullet);

                }
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
                if (a.Active)
                    a.Draw(spriteBatch, Color.White);
            }
            if (_active)
            spriteBatch.Draw(_texture, _position, Color.White);
        }

        public override void genererChemin()
        {
            if (_chemin == 1)
            {
                _position.Y += _speed.Y;
                _position.X += (float)Math.Cos(positionCount)*5;
                if (positionTimer >= 100)
                {
                    positionCount += 0.1f;
                    positionTimer -= 100;
                }
            }
        }
    }
}
