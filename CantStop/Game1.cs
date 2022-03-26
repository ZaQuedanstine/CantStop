using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Squared.Tiled;

namespace CantStop
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Map _map;
        private Vector2 _playerPosition = new Vector2(960, 8500);
        private Vector2 _viewportPosition = new Vector2(0, 0);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _map = Map.Load(Path.Combine(Content.RootDirectory, "level1.tmx"), Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _playerPosition.Y -= 250 * t;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            float offsety =  200 - _playerPosition.Y;
            Matrix transform = Matrix.CreateTranslation(0, offsety, 0);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(transformMatrix: transform);
            _map.Draw(_spriteBatch, new Rectangle(192, 0, 1536, 8960), _viewportPosition);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
