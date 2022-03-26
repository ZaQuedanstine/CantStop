using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

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

        public Lazer(Texture2D newTexture)
        {
            speed = 10;
            texture = newTexture;
            isVisible = false;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, Color.White);
        }
    }
}
