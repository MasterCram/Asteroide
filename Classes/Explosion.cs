using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Tools.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cstj.Sim.ES.MSimard.Classes
{
    public class Explosion : AnimatedSprite
    {
        private float _scale;
        private Color _color;
        public Explosion(Texture2D texture, int rows, int columns, Vector2 position, Vector2 speed, float scale, Color color) : base(texture,rows,columns,position,speed)
        {
            _scale = scale;
            _color = color;
        }

        public override void Update(GameTime gameTime)
        {
            if (_currentFrame == _totalFrames-1)
                _active = false;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int row = _currentFrame / _columns;
            int columns = _currentFrame % _columns;

            Rectangle source = new Rectangle(_frameWidth * columns, _frameHeight * row, _frameWidth, _frameHeight);
            Rectangle destination = new Rectangle((int)_position.X, (int)_position.Y, (int)(_frameWidth * _scale),(int)(_frameHeight * _scale));

            spriteBatch.Draw(_texture, destination, source, _color);
        }
    }
}
