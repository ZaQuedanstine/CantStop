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
    public class Enemy
    {
        public Rectangle boundingRectangle;
        public Texture2D texture, laserTexture;
        public Vector2 position;
        public int health, speed, laserDelay, currentDifficultyLevel;
        public bool isVisible;
        public List<Lazer> laserList;
        int _scrollSpeed;

        public Enemy(Texture2D newTexture, Vector2 newPosition, Texture2D newLaserTexture, int scrollSpeed)
        {
            laserList = new List<Lazer>();
            texture = newTexture;
            laserTexture = newLaserTexture;
            health = 5;
            position = newPosition;
            currentDifficultyLevel = 1;
            laserDelay = 40;
            speed = 5;
            isVisible = true;
            _scrollSpeed = scrollSpeed;

        }

        public void Update(GameTime gt)
        {
            //updates collision rectangle
            boundingRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            //Update enemy movement
            position.Y += speed;

            //enmeny moves back to the top
            if (position.Y >= 950)
                position.Y = -75;

            EnemyShoot();
            UpdateBullets();
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);

            foreach (Lazer l in laserList)
            {
                l.Draw(sb);
            }
        }

        public void UpdateBullets()
        {
            foreach (Lazer l in laserList)
            {
                l.boundingRectangle = new Rectangle((int)l.position.X, (int)l.position.Y, l.texture.Width, l.texture.Height);
                l.position.Y = l.position.Y + l.speed;

                if (l.position.Y >= 0)
                {
                    l.isVisible = false;
                }
            }

            for (int i = 0; i < laserList.Count; i++)
            {
                if (!laserList[i].isVisible)
                {
                    laserList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void EnemyShoot()
        {
            if (laserDelay >= 0)
                laserDelay--;
            if (laserDelay <= 0)
            {
                //Create a new bullet in front of enemy ship
                Lazer newLaser = new Lazer(laserTexture, new Vector2(position.X, position.Y - 128), _scrollSpeed);
                newLaser.position = new Vector2(position.X + texture.Width / 2 - newLaser.texture.Width / 2, position.Y + 30);

                newLaser.isVisible = true;

                if (laserList.Count < 20)
                {
                    laserList.Add(newLaser);
                }
            }

            if (laserDelay == 0)
            {
                laserDelay = 40;
            }
        }
    }
}
