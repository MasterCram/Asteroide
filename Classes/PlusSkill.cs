using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Tools.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cstj.Sim.ES.MSimard.Classes
{
    public class PlusSkill : Sprite
    {
        private float _scale;
        public PlusSkill(Texture2D texture, Vector2 position, Vector2 speed, float scale)
            : base(texture, position, speed)
        {
            _scale = scale;
        }

        public override void Draw(SpriteBatch spriteBatch,Color color)
        {
            spriteBatch.Draw(_texture, _position, null, color, 0, Vector2.Zero, _scale, SpriteEffects.None, 0);
        }

        public override Rectangle BoundingBox
        {
            get { return new Rectangle((int)_position.X, (int)_position.Y, (int)(_texture.Width * _scale), (int)(_texture.Height * _scale)); }
        }
    }
}
