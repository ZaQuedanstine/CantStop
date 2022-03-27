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
        private bool hit;
        //false = left 
        private bool leftOrRight;
        float hitTimer = 0.5f;
        float roamTimer = 2f;

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
            if (hit == true)
            {
                hitTimer -= t;
            }

            if (hitTimer <= 0)
            {
                hit = false;
                hitTimer = 0.5f;
            }

            roamTimer -= t;
            if(roamTimer <= 0)
            {
                leftOrRight = !leftOrRight;
                roamTimer = 2f;
            }

            if(leftOrRight)
            {
                position.X += 300 * t;
            }
            else
            {
                position.X -= 300 * t;
            }
            bounds.X = position.X;
            bounds.Y = position.Y;
        }

        public bool CheckForCollisions(Lazer lazer)
        {
            if(bounds.CollidesWith(lazer.Bounds))
            {
                hit = true;
            }
            return hit;
        }

        public void Draw(SpriteBatch batch)
        {
            if (!hit)
            {
                batch.Draw(texture, position, Color.White);
            }
        }

    }
}
