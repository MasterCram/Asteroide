using Cstj.Sim.ES.MSimard.Classes;
using Cstj.Sim.ES.MSimard.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Tools.Screens;
using MonoGame.Tools.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cstj.Sim.Tp2.MSimard.Screens
{
    public class PauseScreen : Screen
    {
        public static bool active = false;
        private Texture2D pauseBackground;
        private int select = 0;
        private SpriteFont text;
        private MouseService mouseService = ServicesHelper.GetService<MouseService>();
        private Rectangle continueZone;
        private Rectangle exitZone;
        private Audio audio;
        private bool playRoll = true;

        public PauseScreen(Game game)
            : base(game)
        {
            audio = new Audio(game);
        }

        public override void LoadContent()
        {
            pauseBackground = TexturePool.GetTexture(@"Sprites\difficultyBackground");
            text = FontPool.GetFont(@"Fonts\Pericles14");
            continueZone = new Rectangle(350, 220, 120, 20);
            exitZone = new Rectangle(350, 260, 60, 20);
            base.LoadContent();
        }

        public override void HandleInput()
        {
            if ((KeyboardService.IsKeyPressed(Keys.P) || KeyboardService.IsKeyPressed(Keys.Escape)) && IsFocused)
            {
                active = false;
                Unload();
            }
            base.HandleInput();
        }

        public override void Update(GameTime gameTime)
        {
            DetectClick();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(pauseBackground, new Rectangle(320, 150, pauseBackground.Width, pauseBackground.Height), Color.White);
            if (select == 1)
            {
                SpriteBatch.DrawString(text, " Continuer", new Vector2(350, 220), Color.LightBlue);
            }
            else
                SpriteBatch.DrawString(text, "Continuer", new Vector2(350, 220), Color.WhiteSmoke);

            if (select == 2)
            {
                SpriteBatch.DrawString(text, " Menu", new Vector2(350, 260), Color.LightBlue);
            }
            else
                SpriteBatch.DrawString(text, "Menu", new Vector2(350, 260), Color.WhiteSmoke);
            SpriteBatch.End();
        }

        public void DetectClick()
        {
            Point mousePos = new Point(mouseService.CurrentState.X, mouseService.CurrentState.Y);
            if (continueZone.Contains(mousePos))
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
                    active = false;
                }
            }
            else if (exitZone.Contains(mousePos))
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
                    GameScreen.gameFinish = true;
                    FondEtoile.gameFinish = true;
                    ScreenManager.AddScreen<TitleScreen>();
                    active = false;
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
