using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Squared.Tiled;
using CollisionExample.Collisions;

namespace CantStop
{
    public class OctoBoss
    {
        private Texture2D texture;
        private Vector2 position;
        private BoundingRectangle bounds;
        private int _scrollSpeed;

        public BoundingRectangle Bounds => bounds;

        public OctoBoss(Vector2 InitialPosition, int scrollspeed)
        {
            position = InitialPosition;
            bounds = new BoundingRectangle(position, 256, 256);
            _scrollSpeed = scrollspeed;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Octo");
        }
        public void Update(GameTime gameTime)
        {
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.Y -= _scrollSpeed * t;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, Color.White);
        }

    }
}
