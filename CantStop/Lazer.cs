using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using CollisionExample.Collisions;

namespace CantStop
{
    public class Lazer
    {
        public bool isVisible;
        public Rectangle boundingRectangle;
        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public float speed;
        private float _animationTimer = 0.05f;
        private int animationFrame = 0;
        private int _scrollSpeed;
        private BoundingRectangle bounds;
        public BoundingRectangle Bounds => bounds;

        public Lazer(Texture2D newTexture, Vector2 Position ,int scrollSpeed)
        {
            speed = 10;
            texture = newTexture;
            isVisible = false;
            _scrollSpeed = scrollSpeed;
            position = Position;
            bounds = new BoundingRectangle(position, 128, 128);
        }
        
        public void Update(GameTime gameTime, Vector2 playerPos)
        {
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (animationFrame >= 17) position.Y = position.Y - speed - _scrollSpeed * t;
            else
            {
                position.X = playerPos.X;
                position.Y = playerPos.Y - 128;
                position.Y = position.Y - _scrollSpeed * t;
            }
            if(animationFrame < 17)_animationTimer -= t;
            if(_animationTimer < 0)
            {
                _animationTimer = 0.05f;
                if (animationFrame > 17)
                    animationFrame = 17;
                else animationFrame++;
            }
            bounds.X = position.X;
            bounds.Y = position.Y;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, new Rectangle(128 * animationFrame, 0, 128, 128), Color.White);
        }
    }
}
