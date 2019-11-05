using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Tools.Services;
using MonoGame.Tools.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cstj.Sim.ES.MSimard.Classes
{
    public class Vaisseau : KeyboardSprite
    {
        public int life = (PlayerHandler.vie + PlayerHandler.vieAjoute) * 10;
        public int defense = PlayerHandler.defense + PlayerHandler.defenseAjoute;
        public Vaisseau(Texture2D texture ,int rows,int columns,Vector2 position,Vector2 speed):base(texture,rows,columns,position,speed)
        {
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            if (life <= 0)
                life = 0;
            base.Update(gameTime, clientBounds);
        }

    }
}
