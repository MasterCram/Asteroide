using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using MonoGame.Tools.Pools;
using MonoGame.Tools.Screens;
using MonoGame.Tools.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cstj.Sim.ES.MSimard.Classes
{
    public class Audio
    {
        private AudioPool audioPool;
        public SoundEffect shoot;
        public SoundEffect rollOver;
        public SoundEffect explosion;

        public Audio(Game game)
        {
            audioPool = new AudioPool(game);
            shoot = audioPool.GetSoundEffect(@"Audio\laser");
            rollOver = audioPool.GetSoundEffect(@"Audio\rollOver");
            explosion = audioPool.GetSoundEffect(@"Audio\explosion");
        } 
    }
}
