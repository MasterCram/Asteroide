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
    public class WorldMapScreen : Screen
    {
        private Texture2D background;
        private Texture2D etoile;
        private Texture2D easySprite;
        private Texture2D mediumSprite;
        private Texture2D hardSprite;
        private Rectangle niveau1Zone;
        private Rectangle niveau2Zone;
        private Rectangle niveau3Zone;
        private Rectangle niveau4Zone;
        private Rectangle niveau5Zone;
        private Rectangle backZone;
        private Audio audio;
        private bool playRoll = true;
        private MouseService mouseService = ServicesHelper.GetService<MouseService>();
        private int select;
        private SpriteFont text;

        public WorldMapScreen(Game game) : base(game)
        {
            audio = new Audio(game);
        }

        public override void LoadContent()
        {
            niveau1Zone = new Rectangle(50, 300, 100, 100);
            niveau2Zone = new Rectangle(150, 60, 100, 100);
            niveau3Zone = new Rectangle(300, 250, 100, 100);
            niveau4Zone = new Rectangle(500, 100, 100, 100);
            niveau5Zone = new Rectangle(600, 300, 150, 150);
            backZone = new Rectangle(Game.Window.ClientBounds.Width / 2 - 20, 430, 50, 25);

            etoile = TexturePool.GetTexture(@"Sprites\etoile");
            background = TexturePool.GetTexture(@"Sprites\background2");
            easySprite = TexturePool.GetTexture(@"Sprites\easyIcon");
            mediumSprite = TexturePool.GetTexture(@"Sprites\mediumIcon");
            hardSprite = TexturePool.GetTexture(@"Sprites\hardIcon");
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
                SpriteBatch.Draw(background, new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height), Color.Blue);

                if (select == 1)
                {
                    SpriteBatch.Draw(etoile, new Rectangle(50, 300, 100, 100), Color.DarkBlue);
                    SpriteBatch.DrawString(text, "Niveau 1", new Vector2(56, 300), Color.WhiteSmoke);
                    if(PlayerHandler.level > 1)
                        SpriteBatch.Draw(easySprite, new Vector2(26, 250), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    else
                        SpriteBatch.Draw(easySprite, new Vector2(26, 250), null, Color.DarkBlue, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    if(PlayerHandler.level > 6)
                        SpriteBatch.Draw(mediumSprite, new Vector2(76, 250), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    else
                        SpriteBatch.Draw(mediumSprite, new Vector2(76, 250), null, Color.DarkBlue, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    if(PlayerHandler.level > 11)
                        SpriteBatch.Draw(hardSprite, new Vector2(126, 250), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    else
                        SpriteBatch.Draw(hardSprite, new Vector2(126, 250), null, Color.DarkBlue, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                }
                else
                    SpriteBatch.Draw(etoile, new Rectangle(50, 300, 100, 100), Color.White);
                if (select == 2)
                {
                    SpriteBatch.Draw(etoile, new Rectangle(150, 60, 100, 100), Color.DarkBlue);
                    SpriteBatch.DrawString(text, "Niveau 2", new Vector2(156, 60), Color.WhiteSmoke);
                    if (PlayerHandler.level > 2)
                        SpriteBatch.Draw(easySprite, new Vector2(126, 10), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    else
                        SpriteBatch.Draw(easySprite, new Vector2(126, 10), null, Color.DarkBlue, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    if (PlayerHandler.level > 7)
                        SpriteBatch.Draw(mediumSprite, new Vector2(176, 10), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    else
                        SpriteBatch.Draw(mediumSprite, new Vector2(176, 10), null, Color.DarkBlue, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    if (PlayerHandler.level > 12)
                        SpriteBatch.Draw(hardSprite, new Vector2(226, 10), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    else
                        SpriteBatch.Draw(hardSprite, new Vector2(226, 10), null, Color.DarkBlue, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                }
                else
                    SpriteBatch.Draw(etoile, new Rectangle(150, 60, 100, 100), Color.White);
                if (select == 3)
                {
                    SpriteBatch.Draw(etoile, new Rectangle(300, 250, 100, 100), Color.DarkBlue);
                    SpriteBatch.DrawString(text, "Niveau 3", new Vector2(306, 250), Color.WhiteSmoke);
                    if (PlayerHandler.level > 3)
                        SpriteBatch.Draw(easySprite, new Vector2(276, 200), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    else
                        SpriteBatch.Draw(easySprite, new Vector2(276, 200), null, Color.DarkBlue, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    if (PlayerHandler.level > 8)
                        SpriteBatch.Draw(mediumSprite, new Vector2(326, 200), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    else
                        SpriteBatch.Draw(mediumSprite, new Vector2(326, 200), null, Color.DarkBlue, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    if (PlayerHandler.level > 13)
                        SpriteBatch.Draw(hardSprite, new Vector2(376, 200), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    else
                        SpriteBatch.Draw(hardSprite, new Vector2(376, 200), null, Color.DarkBlue, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                }
                else
                    SpriteBatch.Draw(etoile, new Rectangle(300, 250, 100, 100), Color.White);
                if (select == 4)
                {
                    SpriteBatch.Draw(etoile, new Rectangle(500, 100, 100, 100), Color.DarkBlue);
                    SpriteBatch.DrawString(text, "Niveau 4", new Vector2(506, 100), Color.WhiteSmoke);
                    if (PlayerHandler.level > 4)
                        SpriteBatch.Draw(easySprite, new Vector2(476, 50), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    else
                        SpriteBatch.Draw(easySprite, new Vector2(476, 50), null, Color.DarkBlue, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    if (PlayerHandler.level > 9)
                        SpriteBatch.Draw(mediumSprite, new Vector2(526, 50), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    else
                        SpriteBatch.Draw(mediumSprite, new Vector2(526, 50), null, Color.DarkBlue, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    if (PlayerHandler.level > 14)
                        SpriteBatch.Draw(hardSprite, new Vector2(576, 50), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    else
                        SpriteBatch.Draw(hardSprite, new Vector2(576, 50), null, Color.DarkBlue, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                }
                else
                    SpriteBatch.Draw(etoile, new Rectangle(500, 100, 100, 100), Color.White);
                if (select == 5)
                {
                    SpriteBatch.Draw(etoile, new Rectangle(600, 300, 150, 150), Color.DarkBlue);
                    SpriteBatch.DrawString(text, "Niveau 5", new Vector2(630, 300), Color.WhiteSmoke);
                    if (PlayerHandler.level > 5)
                        SpriteBatch.Draw(easySprite, new Vector2(600, 250), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    else
                        SpriteBatch.Draw(easySprite, new Vector2(600, 250), null, Color.DarkBlue, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    if (PlayerHandler.level > 10)
                        SpriteBatch.Draw(mediumSprite, new Vector2(650, 250), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    else
                        SpriteBatch.Draw(mediumSprite, new Vector2(650, 250), null, Color.DarkBlue, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    if (PlayerHandler.level > 15)
                        SpriteBatch.Draw(hardSprite, new Vector2(700, 250), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    else
                        SpriteBatch.Draw(hardSprite, new Vector2(700, 250), null, Color.DarkBlue, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                }
                else
                    SpriteBatch.Draw(etoile, new Rectangle(600, 300, 150, 150), Color.White);
                if (select == 6)
                    SpriteBatch.DrawString(text, " back", new Vector2(Game.Window.ClientBounds.Width / 2 - 20, 430), Color.LightBlue);
                else
                    SpriteBatch.DrawString(text, "back", new Vector2(Game.Window.ClientBounds.Width / 2 - 20, 430), Color.WhiteSmoke);
            }
            SpriteBatch.End();
        }

        public void DetectClick()
        {

            Point mousePos = new Point(mouseService.CurrentState.X, mouseService.CurrentState.Y);
            if (niveau1Zone.Contains(mousePos))
            {
                select = 1;
                if (playRoll)
                {
                    //audio.rollOver.Play();
                    playRoll = false;
                }
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released)
                {
                    ScreenManager.AddScreen<DifficulteScreen>();
                    PlayerHandler.levelSelected = 1;
                    Unload();
                }
            }
            else if (niveau2Zone.Contains(mousePos))
            {
                select = 2;
                if (playRoll)
                {
                    //audio.rollOver.Play();
                    playRoll = false;
                }
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released)
                {
                    ScreenManager.AddScreen<DifficulteScreen>();
                    PlayerHandler.levelSelected = 2;
                    Unload();
                }
            }
            else if (niveau3Zone.Contains(mousePos))
            {
                select = 3;
                if (playRoll)
                {
                    //audio.rollOver.Play();
                    playRoll = false;
                }
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released)
                {
                    ScreenManager.AddScreen<DifficulteScreen>();
                    PlayerHandler.levelSelected = 3;
                    Unload();
                }
            }
            else if (niveau4Zone.Contains(mousePos))
            {
                select = 4;
                if (playRoll)
                {
                    //audio.rollOver.Play();
                    playRoll = false;
                }
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released)
                {
                    ScreenManager.AddScreen<DifficulteScreen>();
                    PlayerHandler.levelSelected = 4;
                    Unload();
                }
            }
            else if (niveau5Zone.Contains(mousePos))
            {
                select = 5;
                if (playRoll)
                {
                    //audio.rollOver.Play();
                    playRoll = false;
                }
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released)
                {
                    ScreenManager.AddScreen<DifficulteScreen>();
                    PlayerHandler.levelSelected = 5;
                    Unload();
                }
            }
            else if (backZone.Contains(mousePos))
            {
                select = 6;
                if (playRoll)
                {
                   // audio.rollOver.Play();
                    playRoll = false;
                }
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released)
                {
                    Unload();
                    ScreenManager.AddScreen<TitleScreen>();
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
