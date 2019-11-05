using Cstj.Sim.ES.MSimard.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Tools.Screens;
using MonoGame.Tools.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cstj.Sim.ES.MSimard.Screens
{
    public class DifficulteScreen: Screen
    {
        private Texture2D background;
        private Texture2D background2;
        private Rectangle easyZone;
        private Rectangle mediumZone;
        private Rectangle hardZone;
        private Rectangle backZone;
        private Texture2D etoile;
        private MouseService mouseService = ServicesHelper.GetService<MouseService>();
        private int select;
        private SpriteFont text;
        private Audio audio;
        private bool playRoll = true;

        public DifficulteScreen(Game game) : base(game)
        {
            audio = new Audio(game);
        }

        public override void LoadContent()
        {
            easyZone = new Rectangle(360, 200, 65, 20);
            mediumZone = new Rectangle(360, 240, 75, 20);
            hardZone = new Rectangle(360, 280, 90, 20);
            backZone = new Rectangle(Game.Window.ClientBounds.Width / 2 - 20, 430, 50, 25);
            background = TexturePool.GetTexture(@"Sprites\difficultyBackground");
            background2 = TexturePool.GetTexture(@"Sprites\background2");
            etoile = TexturePool.GetTexture(@"Sprites\etoile");
            text = FontPool.GetFont(@"Fonts\Pericles14");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (IsFocused)
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
                SpriteBatch.Draw(background2, new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height), Color.DarkBlue);
                SpriteBatch.Draw(etoile, new Rectangle(50, 300, 100, 100), Color.Black);
                SpriteBatch.Draw(etoile, new Rectangle(150, 60, 100, 100), Color.Black);
                SpriteBatch.Draw(etoile, new Rectangle(300, 250, 100, 100), Color.Black);
                SpriteBatch.Draw(etoile, new Rectangle(500, 100, 100, 100), Color.Black);
                SpriteBatch.Draw(etoile, new Rectangle(600, 300, 150, 150), Color.Black);
                SpriteBatch.Draw(background, new Rectangle(320, 150, background.Width, background.Height), Color.White);
                if (select == 1 && PlayerHandler.level >= PlayerHandler.levelSelected)
                {
                    SpriteBatch.DrawString(text, " Facile", new Vector2(360, 200), Color.LightBlue);
                }
                else if(PlayerHandler.level >= PlayerHandler.levelSelected)
                    SpriteBatch.DrawString(text, "Facile", new Vector2(360, 200), Color.WhiteSmoke);
                else
                    SpriteBatch.DrawString(text, "Facile", new Vector2(360, 200), Color.Black);

                if (select == 2 && PlayerHandler.level >= PlayerHandler.levelSelected + 5)
                {
                    SpriteBatch.DrawString(text, " Moyen", new Vector2(360, 240), Color.LightBlue);
                }
                else if (PlayerHandler.level >= PlayerHandler.levelSelected + 5)
                    SpriteBatch.DrawString(text, "Moyen", new Vector2(360, 240), Color.WhiteSmoke);
                else
                    SpriteBatch.DrawString(text, "Moyen", new Vector2(360, 240), Color.Black);

                if (select == 3 && PlayerHandler.level >= PlayerHandler.levelSelected + 10)
                {
                    SpriteBatch.DrawString(text, " Difficile", new Vector2(360, 280), Color.LightBlue);
                }
                else if (PlayerHandler.level >= PlayerHandler.levelSelected + 10)
                    SpriteBatch.DrawString(text, "Difficile", new Vector2(360, 280), Color.WhiteSmoke);
                else
                    SpriteBatch.DrawString(text, "Difficile", new Vector2(360, 280), Color.Black);

                if (select == 4)
                    SpriteBatch.DrawString(text, " back", new Vector2(Game.Window.ClientBounds.Width / 2 - 20, 430), Color.LightBlue);
                else
                    SpriteBatch.DrawString(text, "back", new Vector2(Game.Window.ClientBounds.Width / 2 - 20, 430), Color.WhiteSmoke);
            }
            SpriteBatch.End();
        }

        public void DetectClick()
        {
            Point mousePos = new Point(mouseService.CurrentState.X, mouseService.CurrentState.Y);
            if (easyZone.Contains(mousePos))
            {
                select = 1;
                if (playRoll && PlayerHandler.level >= PlayerHandler.levelSelected)
                {
                    //audio.rollOver.Play();
                    playRoll = false;
                }
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released && PlayerHandler.level >= PlayerHandler.levelSelected)
                {
                    PlayerHandler.difficulty = 1;
                    Unload();
                    ScreenManager.AddScreen<FondEtoile>();
                    ScreenManager.AddScreen<GameScreen>();
                }
            }
            else if (mediumZone.Contains(mousePos))
            {
                select = 2;
                if (playRoll && PlayerHandler.level >= PlayerHandler.levelSelected + 5)
                {
                    //audio.rollOver.Play();
                    playRoll = false;
                }
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released && PlayerHandler.level >= PlayerHandler.levelSelected+5)
                {
                    PlayerHandler.difficulty = 2;
                    Unload();
                    ScreenManager.AddScreen<FondEtoile>();
                    ScreenManager.AddScreen<GameScreen>();
                }
            }
            else if (hardZone.Contains(mousePos))
            {
                select = 3;
                if (playRoll && PlayerHandler.level >= PlayerHandler.levelSelected + 10)
                {
                    //audio.rollOver.Play();
                    playRoll = false;
                }
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released && PlayerHandler.level >= PlayerHandler.levelSelected+10)
                {
                    PlayerHandler.difficulty = 3;
                    Unload();
                    ScreenManager.AddScreen<FondEtoile>();
                    ScreenManager.AddScreen<GameScreen>();
                }
            }
            else if (backZone.Contains(mousePos))
            {
                select = 4;
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


            else
            {
                select = 0;
                playRoll = true;
            }
        }

    }
}
