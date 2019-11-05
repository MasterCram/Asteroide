using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Tools.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cstj.Sim.ES.MSimard.Classes
{
    public class Bullet : Sprite
    {
        public Bullet(Texture2D texture, Vector2 position, Vector2 speed) : base(texture,position,speed)
        {

        }
    }
}
