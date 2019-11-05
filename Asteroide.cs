#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Tools.Managers;
using MonoGame.Tools.Services;
using MonoGame.Tools.Pools;
using Cstj.Sim.ES.MSimard.Screens;
#endregion

namespace Cstj.Sim.ES.MSimard
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Asteroide : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ScreenManager screenManager;

        public Asteroide()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ServicesHelper.Game = this;
            ServicesHelper.AddService<TexturePool>(new TexturePool(this));
            ServicesHelper.AddService<FontPool>(new FontPool(this));
            ServicesHelper.AddService<AudioPool>(new AudioPool(this));
            this.Components.Add(new KeyboardService(this));
            this.Components.Add(new MouseService(this));
            screenManager = new ScreenManager(this, false);
            this.Components.Add(screenManager);
            screenManager.AddScreen<TitleScreen>();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {}

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) // || Keyboard.GetState().IsKeyDown(Keys.Escape)
                Exit();
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}
