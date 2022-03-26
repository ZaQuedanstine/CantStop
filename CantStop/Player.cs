using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Squared.Tiled;
using CollisionExample.Collisions;

namespace CantStop
{
    public class Player
    {
        public Rectangle boundingRectangle;
        public Texture2D texture, laserTexture;
        private Texture2D flounder;
        public Vector2 position;
        public float laserDelay;
        public int speed;
        public bool isColliding;
        public List<Lazer> laserList;
        private KeyboardState priorKeyboardState;
        private KeyboardState currentKeyboardState;
        public int Scrollspeed;
        private Map _map;
        private Layer layer;
        private bool hit;
        private float hitTimer = 1.5f;

        private BoundingRectangle bounds;

        public BoundingRectangle Bounds => bounds;


        public Player(Vector2 InitialPosition,int scrollspeed, Map map)
        {
            laserList = new List<Lazer>();
            texture = null;
            position = InitialPosition;
            bounds = new BoundingRectangle(position.X, position.Y, 128, 256);
            laserDelay = 5;
            speed = 10;
            isColliding = false;
            Scrollspeed = scrollspeed;
            _map = map;
            map.Layers.TryGetValue("Tiles" ,out layer);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("shark");
            laserTexture = content.Load<Texture2D>("unknown");
            flounder = content.Load<Texture2D>("flounder");
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
            
            foreach(Lazer l in laserList)
            {
                l.Draw(sb);
            }
            if (hit)
            {
                sb.Draw(flounder, position, Color.White);
            }
        }

        public void Update(GameTime gt)
        {
            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            float t = (float)gt.ElapsedGameTime.TotalSeconds;
            hit = false;
            if (currentKeyboardState.IsKeyDown(Keys.Space) && priorKeyboardState.IsKeyUp(Keys.Space))
            {
                Shoot();
            }

            if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                position.X = position.X - speed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.D))
            {
                position.X = position.X + speed;
            }

            if (position.X < 192) position.X = 192;
            if (position.X > 1728 - texture.Width) position.X = 1728 - texture.Width;
            float tempypos = position.Y;
            position.Y -= Scrollspeed * t;
            if(CollidesWithTile())
            {
                hit = true;
            }

            bounds.X = position.X;
            bounds.Y = position.Y;
        }

        public void Shoot()

        {

            if (laserDelay >= 0)
            {
                laserDelay--;
            }

            if (laserDelay <= 0)
            {
                Lazer newLaser = new Lazer(laserTexture);

                newLaser.position = new Vector2(position.X + 32 - newLaser.texture.Width / 2, position.Y + 30);
                newLaser.isVisible = true;

                if (laserList.Count < 20)

                {
                    laserList.Add(newLaser);
                }
            }

            if (laserDelay == 0)

            {
                laserDelay = 5;
            }
        }

        public void UpdateBullets(GameTime gameTime)
        {
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (Lazer l in laserList)

            {
                l.position.Y = l.position.Y - l.speed - Scrollspeed * t;

                if (l.position.Y <= position.Y - 1080)

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


        public bool CollidesWithTile()
        {
            
            for (int y = 0; y < 140; y++)
            {
                for (int x = 0; x < 24; x++)
                {
                    int tile = layer.GetTile(x, y);
                    BoundingRectangle tileBounds = new BoundingRectangle(x * 64 + 192, y * 64, 64, 64);
                    if (tile != 0 && tileBounds.CollidesWith(this.bounds)) return true;
                }
            }
            return false;
        }
    }
}
