using Cstj.Sim.ES.MSimard.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Tools.Screens;
using MonoGame.Tools.Services;
using MonoGame.Tools.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cstj.Sim.ES.MSimard.Screens
{
    public class VaisseauScreen : Screen
    {
        private Texture2D background;
        private Sprite vaisseauIcon1;
        private Sprite vaisseauIcon2;
        private Sprite vaisseauIcon3;
        private Sprite vaisseauIcon4;
        private Sprite vaisseauIcon5;
        private PlusSkill plusIcon1;
        private PlusSkill plusIcon2;
        private PlusSkill plusIcon3;
        private PlusSkill plusIcon4;
        private PlusSkill plusIcon5;
        private Texture2D upgradeIcon;
        private SpriteFont text;
        private Rectangle backZone;
        private MouseService mouseService = ServicesHelper.GetService<MouseService>();
        private int select;
        private Audio audio;
        private bool playRoll = true;

        public VaisseauScreen(Game game) : base(game)
        {
            audio = new Audio(game);
        }

        public override void LoadContent()
        {
            background = TexturePool.GetTexture(@"Sprites\background3");
            vaisseauIcon1 = new Sprite(TexturePool.GetTexture(@"Sprites\vaisseauIcon1"),new Vector2(150, 100));
            vaisseauIcon2 = new Sprite(TexturePool.GetTexture(@"Sprites\vaisseauIcon2"),new Vector2(250, 100));
            vaisseauIcon3 = new Sprite(TexturePool.GetTexture(@"Sprites\vaisseauIcon3"),new Vector2(350, 100));
            vaisseauIcon4 = new Sprite(TexturePool.GetTexture(@"Sprites\vaisseauIcon4"),new Vector2(450, 100));
            vaisseauIcon5 = new Sprite(TexturePool.GetTexture(@"Sprites\vaisseauIcon5"),new Vector2(550, 100));
            plusIcon1 = new PlusSkill(TexturePool.GetTexture(@"Sprites\plus"), new Vector2(260, 205), Vector2.Zero, 0.05f);
            plusIcon2 = new PlusSkill(TexturePool.GetTexture(@"Sprites\plus"), new Vector2(260, 230), Vector2.Zero, 0.05f);
            plusIcon3 = new PlusSkill(TexturePool.GetTexture(@"Sprites\plus"), new Vector2(260, 255), Vector2.Zero, 0.05f);
            plusIcon4 = new PlusSkill(TexturePool.GetTexture(@"Sprites\plus"), new Vector2(260, 280), Vector2.Zero, 0.05f);
            plusIcon5 = new PlusSkill(TexturePool.GetTexture(@"Sprites\plus"), new Vector2(260, 305), Vector2.Zero, 0.05f);
            upgradeIcon = TexturePool.GetTexture(@"Sprites\upgradeIcon");
            text = FontPool.GetFont(@"Fonts\Pericles14");
            backZone = new Rectangle(Game.Window.ClientBounds.Width / 2 - 20, 430, 50, 25);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            DetectClick();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(background, new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height), Color.LightBlue);
            if (PlayerHandler.vaisseauID == 1)
                vaisseauIcon1.Draw(SpriteBatch);
            else
                vaisseauIcon1.Draw(SpriteBatch, Color.DarkRed);

            if (PlayerHandler.vaisseauID == 2)
                vaisseauIcon2.Draw(SpriteBatch);
            else
                vaisseauIcon2.Draw(SpriteBatch, Color.DarkRed);

            if (PlayerHandler.vaisseauID == 3)
                vaisseauIcon3.Draw(SpriteBatch);
            else
                vaisseauIcon3.Draw(SpriteBatch, Color.DarkRed);

            if (PlayerHandler.vaisseauID == 4)
                vaisseauIcon4.Draw(SpriteBatch);
            else
                vaisseauIcon4.Draw(SpriteBatch, Color.DarkRed);

            if (PlayerHandler.vaisseauID == 5)
                vaisseauIcon5.Draw(SpriteBatch);
            else
                vaisseauIcon5.Draw(SpriteBatch, Color.DarkRed);

            if (select == 1)
                SpriteBatch.DrawString(text, " back", new Vector2(Game.Window.ClientBounds.Width / 2 - 20, 430), Color.LightBlue);
            else
                SpriteBatch.DrawString(text, "back", new Vector2(Game.Window.ClientBounds.Width / 2 - 20, 430), Color.WhiteSmoke);
            SpriteBatch.DrawString(text, "Attaque", new Vector2(280, 200), Color.White);
            SpriteBatch.DrawString(text, "Defense", new Vector2(280, 225), Color.White);
            SpriteBatch.DrawString(text, "Tir", new Vector2(280, 250), Color.White);
            SpriteBatch.DrawString(text, "Vitesse", new Vector2(280, 275), Color.White);
            SpriteBatch.DrawString(text, "Vie", new Vector2(280, 300), Color.White);
            SpriteBatch.DrawString(text, "Points", new Vector2(280, 325), Color.White);
            SpriteBatch.DrawString(text, ":  " + PlayerHandler.attaque + " + " + PlayerHandler.attaqueAjoute, new Vector2(375, 200), Color.White);
            SpriteBatch.DrawString(text, ":  " + PlayerHandler.defense + " + " + PlayerHandler.defenseAjoute, new Vector2(375, 225), Color.White);
            SpriteBatch.DrawString(text, ":  " + PlayerHandler.tir + " + " + PlayerHandler.tirAjoute, new Vector2(375, 250), Color.White);
            SpriteBatch.DrawString(text, ":  " + PlayerHandler.vitesse + " + " + PlayerHandler.vitesseAjoute, new Vector2(375, 275), Color.White);
            SpriteBatch.DrawString(text, ":  " + PlayerHandler.vie + " + " + PlayerHandler.vieAjoute, new Vector2(375, 300), Color.White);
            SpriteBatch.DrawString(text, ":  " + PlayerHandler.skillPoints, new Vector2(375, 325), Color.White);
            if (PlayerHandler.skillPoints > 0)
            {
                if(select == 2 && PlayerHandler.attaqueAjoute < 10)
                    plusIcon1.Draw(SpriteBatch,Color.DarkGreen);
                else if(PlayerHandler.attaqueAjoute < 10)
                    plusIcon1.Draw(SpriteBatch, Color.White);

                if (select == 3 && PlayerHandler.defenseAjoute < 10)
                    plusIcon2.Draw(SpriteBatch, Color.DarkGreen);
                else if (PlayerHandler.defenseAjoute < 10)
                    plusIcon2.Draw(SpriteBatch, Color.White);

                if (select == 4 && PlayerHandler.tirAjoute < 10)
                    plusIcon3.Draw(SpriteBatch, Color.DarkGreen);
                else if (PlayerHandler.tirAjoute < 10)
                    plusIcon3.Draw(SpriteBatch, Color.White);

                if (select == 5 && PlayerHandler.vitesseAjoute < 10)
                    plusIcon4.Draw(SpriteBatch, Color.DarkGreen);
                else if (PlayerHandler.vitesseAjoute < 10)
                    plusIcon4.Draw(SpriteBatch, Color.White);

                if (select == 6 && PlayerHandler.vieAjoute < 10)
                    plusIcon5.Draw(SpriteBatch, Color.DarkGreen);
                else if (PlayerHandler.vieAjoute < 10)
                    plusIcon5.Draw(SpriteBatch, Color.White);
            }

            SpriteBatch.End();
        }

        public void DetectClick()
        {

            Point mousePos = new Point(mouseService.CurrentState.X, mouseService.CurrentState.Y);
            if (backZone.Contains(mousePos))
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
                    ScreenManager.AddScreen<TitleScreen>();
                }
            }
            else if (vaisseauIcon1.BoundingBox.Contains(mousePos))
            {

                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released)
                {
                    PlayerHandler.vaisseauID = 1;
                    PlayerHandler.setStats(3, 3, 3, 3, 5);
                }
            }
            else if (vaisseauIcon2.BoundingBox.Contains(mousePos))
            {
                
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released)
                {
                    PlayerHandler.vaisseauID = 2;
                    PlayerHandler.setStats(2, 5, 2, 2, 6);
                }
            }
            else if (vaisseauIcon3.BoundingBox.Contains(mousePos))
            {
                
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released)
                {
                    PlayerHandler.vaisseauID = 3;
                    PlayerHandler.setStats(2, 1, 4, 5, 5);
                }
            }
            else if (vaisseauIcon4.BoundingBox.Contains(mousePos))
            {
                
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released)
                {
                    PlayerHandler.vaisseauID = 4;
                    PlayerHandler.setStats(5, 5, 2, 2, 3);
                }
            }
            else if (vaisseauIcon5.BoundingBox.Contains(mousePos))
            {
                
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released)
                {
                    PlayerHandler.vaisseauID = 5;
                    PlayerHandler.setStats(4, 2, 3, 4, 4);
                }
            }
            else if (plusIcon1.BoundingBox.Contains(mousePos) && PlayerHandler.skillPoints > 0)
            {
                select = 2;
                if (playRoll && PlayerHandler.skillPoints > 0)
                {
                    //audio.rollOver.Play();
                    playRoll = false;
                }
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released && PlayerHandler.attaqueAjoute < 10)
                {
                    PlayerHandler.attaqueAjoute++;
                    PlayerHandler.skillPoints--;
                }
            }
            else if (plusIcon2.BoundingBox.Contains(mousePos) && PlayerHandler.skillPoints > 0)
            {
                select = 3;
                if (playRoll && PlayerHandler.skillPoints > 0)
                {
                    //audio.rollOver.Play();
                    playRoll = false;
                }
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released && PlayerHandler.defenseAjoute < 10)
                {
                    PlayerHandler.defenseAjoute++;
                    PlayerHandler.skillPoints--;
                }
            }
            else if (plusIcon3.BoundingBox.Contains(mousePos) && PlayerHandler.skillPoints > 0)
            {
                select = 4;
                if (playRoll && PlayerHandler.skillPoints > 0)
                {
                    //audio.rollOver.Play();
                    playRoll = false;
                }
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released && PlayerHandler.tirAjoute < 10)
                {
                    PlayerHandler.tirAjoute++;
                    PlayerHandler.skillPoints--;
                }
            }
            else if (plusIcon4.BoundingBox.Contains(mousePos) && PlayerHandler.skillPoints > 0)
            {
                select = 5;
                if (playRoll && PlayerHandler.skillPoints > 0)
                {
                    //audio.rollOver.Play();
                    playRoll = false;
                }
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released && PlayerHandler.vitesseAjoute < 10)
                {
                    PlayerHandler.vitesseAjoute++;
                    PlayerHandler.skillPoints--;
                }
            }
            else if (plusIcon5.BoundingBox.Contains(mousePos) && PlayerHandler.skillPoints > 0)
            {
                select = 6;
                if (playRoll && PlayerHandler.skillPoints > 0)
                {
                   // audio.rollOver.Play();
                    playRoll = false;
                }
                if (mouseService.CurrentState.LeftButton == ButtonState.Pressed && mouseService.PreviousState.LeftButton == ButtonState.Released && PlayerHandler.vieAjoute < 10)
                {
                    PlayerHandler.vieAjoute++;
                    PlayerHandler.skillPoints--;
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
