using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CantStop
{
    public class Obstacle
    {
        public Rectangle boundingRectangle;
        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public float rotationAngle;
        public int speed;

        public bool isVisible;
        Random random = new Random();
        public float randX;
        public float randY;

        public Obstacle(Texture2D newTexture, Vector2 newPosition, int scrollSpeed)
        {
            position = newPosition;
            texture = newTexture;
            speed = 4;
            isVisible = true;
            randX = random.Next(0, 750);
            randY = random.Next(-600, -50);
        }

        public void LoadContent(ContentManager Content)
        {

        }

        public void Update(GameTime gt)
        {
            //sets collision bounds
            boundingRectangle = new Rectangle((int)position.X, (int)position.Y, 45, 45);

            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;

            //update movement
            position.Y = position.Y + speed;
            if (position.Y >= 950)
            {
                position.Y = -50;
            }

            //rotate asteroid

            float elapsed = (float)gt.ElapsedGameTime.TotalSeconds;
            rotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            rotationAngle = rotationAngle % circle;

        }

        public void Draw(SpriteBatch sb)
        {
            if (isVisible)
                sb.Draw(texture, position, Color.White);
        }
    }
}
