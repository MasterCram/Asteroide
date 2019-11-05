using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Tools.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cstj.Sim.ES.MSimard.Classes
{
    public class Asteroid : AnimatedSprite
    {
        public float _scale;
        public int vie = 2 + PlayerHandler.difficulty;
        public Asteroid(Texture2D texture, int rows, int columns, Vector2 position, Vector2 speed,float scale,int totalFrame, int millisecondsPerFrame=35)
            : base(texture, rows, columns, position, speed,totalFrame, millisecondsPerFrame)
        {
            _scale = scale;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int row = _currentFrame / _columns;
            int columns = _currentFrame % _columns;

            Rectangle source = new Rectangle(_frameWidth * columns, _frameHeight * row, _frameWidth, _frameHeight);
            Rectangle destination = new Rectangle((int)_position.X, (int)_position.Y, (int)(_frameWidth*_scale), (int)(_frameHeight*_scale));

            spriteBatch.Draw(_texture, destination, source, Color.White);
        }

        public override Rectangle BoundingBox
        {
            get { return new Rectangle((int)_position.X, (int)_position.Y, (int)(_frameWidth*_scale), (int)(_frameHeight*_scale)); }
        }
    }
}
