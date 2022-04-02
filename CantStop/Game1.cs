using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using Squared.Tiled;

namespace CantStop
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Map _map;
        private Map levelXMap;
        private Map levelZMap;
        private Map levelRMap;
        private Map testMap;
        private Texture2D _background;
        private Vector2 _backGroundPosition = new Vector2(192, 17920);
        private Player player;
        private Vector2 _cameraPosition = new Vector2(960, 17920);
        private Vector2 _viewportPosition = new Vector2(0, 0);
        private int scrollSpeed;
        private OctoBoss octoBoss;

        private Song titleMusic;
        private Song spoopyBackgroundMusic;

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
            octoBoss = new OctoBoss(_cameraPosition - new Vector2(0, 200), scrollSpeed);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //_map = Map.Load(Path.Combine(Content.RootDirectory, "level1.tmx"), Content);
            //levelXMap = Map.Load(Path.Combine(Content.RootDirectory, "spoopyXmap.tmx"), Content);
            //evelZMap = Map.Load(Path.Combine(Content.RootDirectory, "spoopyZMap.tmx"), Content);
            //levelRMap = Map.Load(Path.Combine(Content.RootDirectory, "spoopyRmap.tmx"), Content);
            testMap = Map.Load(Path.Combine(Content.RootDirectory, "TestMap.tmx"), Content);

            octoBoss.LoadContent(Content);
            //player = new Player(new Vector2(896, 18020),scrollSpeed, _map, octoBoss);
            //player = new Player(new Vector2(896, 18020), scrollSpeed, levelXMap, octoBoss);
            //player = new Player(new Vector2(896, 18020), scrollSpeed, levelXMap, octoBoss);
            player = new Player(new Vector2(896, 18020), scrollSpeed, testMap, octoBoss);

            player.LoadContent(Content);
            
            _background = Content.Load<Texture2D>("cosmicbackground");

            titleMusic = Content.Load<Song>("B R U H");
            //MediaPlayer.IsRepeating = true;
            //MediaPlayer.Play(titleMusic);

            spoopyBackgroundMusic = Content.Load<Song>("spoopyshark");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(spoopyBackgroundMusic);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _cameraPosition.Y -= scrollSpeed * t;
            player.Update(gameTime);
            player.UpdateBullets(gameTime);
            octoBoss.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            float offsety =  200 - _cameraPosition.Y;
            Matrix transform = Matrix.CreateTranslation(0, offsety, 0);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(transformMatrix: transform);
            _spriteBatch.Draw(_background, _backGroundPosition, Color.White);
            _spriteBatch.Draw(_background, _backGroundPosition + new Vector2(0, -4480), Color.White);
            _spriteBatch.Draw(_background, _backGroundPosition + new Vector2(0, -4480 * 2), Color.White);
            _spriteBatch.Draw(_background, _backGroundPosition + new Vector2(0, -4480 * 3), Color.White);
            _spriteBatch.Draw(_background, _backGroundPosition + new Vector2(0, -4480 * 4), Color.White);
            _spriteBatch.Draw(_background, _backGroundPosition + new Vector2(0, -4480 * 5), Color.White);
            //_map.Draw(_spriteBatch, new Rectangle(192, 0, 1536, 17920), _viewportPosition);
            //levelXMap.Draw(_spriteBatch, new Rectangle(192, 0, 1536, 17920), _viewportPosition);
            //levelZMap.Draw(_spriteBatch, new Rectangle(192, 0, 1536, 17920), _viewportPosition);
            testMap.Draw(_spriteBatch, new Rectangle(192, 0, 1536, 17920), _viewportPosition);

            octoBoss.Draw(_spriteBatch);
            player.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
