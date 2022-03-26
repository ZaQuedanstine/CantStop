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
        private Texture2D _background;
        private Vector2 _backGroundPosition = new Vector2(192, 0);
        private Player player;
        private Vector2 _playerPosition = new Vector2(960, 8500);
        private Vector2 _viewportPosition = new Vector2(0, 0);
        private int scrollSpeed;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.IsFullScreen = true;
            scrollSpeed = 350;
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
            player = new Player(scrollSpeed, _map);
            player.position = new Vector2(960, 8500);
            player.LoadContent(Content);
            _background = Content.Load<Texture2D>("newBackground");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _playerPosition.Y -= scrollSpeed * t;
            player.Update(gameTime);
            player.UpdateBullets(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            float offsety =  200 - _playerPosition.Y;
            Matrix transform = Matrix.CreateTranslation(0, offsety, 0);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(transformMatrix: transform);
            _spriteBatch.Draw(_background, _backGroundPosition, Color.White);
            //_spriteBatch.Draw(_background2, _backgroundPosition2, Color.White);
            _map.Draw(_spriteBatch, new Rectangle(192, 0, 1536, 8960), _viewportPosition);
            player.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
