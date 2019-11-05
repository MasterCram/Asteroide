using Cstj.Sim.ES.MSimard.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Tools.Managers;
using MonoGame.Tools.Screens;
using MonoGame.Tools.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cstj.Sim.ES.MSimard.Screens
{
    public class TitleScreen : Screen
    {
        private SpriteFont text;
        private Rectangle worldMapZone;
        private Rectangle vaisseauZone;
        private Rectangle exitZone;
        private int select = 0;
        private MouseService mouseService = ServicesHelper.GetService<MouseService>();
        private Texture2D background;
        private Audio audio;
        private bool playRoll = true;

        public TitleScreen(Game game) : base(game)
        {
            audio = new Audio(game);
        }

        public override void LoadContent()
        {
            

            text = FontPool.GetFont(@"Fonts\Pericles14");
            background = TexturePool.GetTexture(@"Sprites\background");
            worldMapZone = new Rectangle(325, 190, 160, 20);
            vaisseauZone = new Rectangle(325, 215, 95, 20);
            exitZone = new Rectangle(325, 240, 95, 20);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (this.IsFocused)
            {
                DetectClick();
                base.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            if (IsFocused)
            {
                SpriteBatch.Draw(background, new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height), Color.White);
                if (select == 1)
                    SpriteBatch.DrawString(text, " Carte Globale", new Vector2(325, 190), Color.LightBlue);
                else
                    SpriteBatch.DrawString(text, "Carte Globale", new Vector2(325, 190), Color.WhiteSmoke);

                if (select == 2)
                    SpriteBatch.DrawString(text, " Vaisseau", new Vector2(325, 215), Color.LightBlue);
                else
                    SpriteBatch.DrawString(text, "Vaisseau", new Vector2(325, 215), Color.WhiteSmoke);

                if (select == 3)
                    SpriteBatch.DrawString(text, " Quitter", new Vector2(325, 240), Color.LightBlue);
                else
                    SpriteBatch.DrawString(text, "Quitter", new Vector2(325, 240), Color.WhiteSmoke);
            }
            
            SpriteBatch.End();
        }

        public void DetectClick()
        {
            
            Point mousePos = new Point(mouseService.CurrentState.X, mouseService.CurrentState.Y);
            if (worldMapZone.Contains(mousePos))
            {
                select = 1;
                if (playRoll)
                {
                    //audio.rollOver.Play();
                    playRoll = false;
                }
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released)
                {
                    Unload();
                    ScreenManager.AddScreen<WorldMapScreen>();
                }
            }
            else if (vaisseauZone.Contains(mousePos))
            {
                select = 2;
                if (playRoll)
                {
                    //audio.rollOver.Play();
                    playRoll = false;
                }
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released)
                {
                    Unload();
                    ScreenManager.AddScreen<VaisseauScreen>();
                }
            }
            else if (exitZone.Contains(mousePos))
            {
                select = 3;
                if (playRoll)
                {
                    //audio.rollOver.Play();
                    playRoll = false;
                }
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released)
                {
                    Game.Exit();
                }
            }
            else
            {
                select = 0;
                playRoll = true;
            }
        }
    }
}
