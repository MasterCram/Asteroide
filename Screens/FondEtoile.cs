using Cstj.Sim.ES.MSimard.Screens;
using Cstj.Sim.Tp2.MSimard.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Tools.Screens;
using MonoGame.Tools.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cstj.Sim.ES.MSimard.Screens
{
    public class FondEtoile : Screen
    {
        public static bool gameFinish = false;
        private Texture2D star;
        private List<Sprite> etoiles;
        private Random rnd;
        private float scale = 0.1f;
        private double etoileSpawnTimer = 0;
        public FondEtoile(Game game):base(game)
        {
            etoiles = new List<Sprite>();
            rnd = new Random();
        }

        public override void LoadContent()
        {
            star = TexturePool.GetTexture(@"Sprites\star");
            for (int i = 0; i < 100; i++)
            {
                Sprite etoile = new Sprite(star, new Vector2(rnd.Next(0, Game.Window.ClientBounds.Width), rnd.Next(0, Game.Window.ClientBounds.Height)), new Vector2(0, 2));
                etoiles.Add(etoile);
            
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
            if (PauseScreen.active == false)
            {
                etoileSpawnTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (etoileSpawnTimer >= 100)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Sprite etoile = new Sprite(star, new Vector2(rnd.Next(0, Game.Window.ClientBounds.Width), -10), new Vector2(0, 2));
                        etoiles.Add(etoile);
                    }
                    etoileSpawnTimer -= 100;
                }

                foreach (var a in etoiles)
                {
                    if (a.Active)
                    {
                        a.Update(gameTime);
                    }
                    if (a.BoundingBox.Y >= Game.Window.ClientBounds.Height)
                    {
                        a.Active = false;
                    }
                }

                base.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            foreach (var a in etoiles)
            {
                if (a.Active)
                    a.Draw(SpriteBatch, scale);
            }
            SpriteBatch.End();
        }
    }
}
