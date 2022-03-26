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
        private bool invulnerable;
        private float hitAnimationTimer = 0.1f;
        private int hitAnimationFrame = 0;

        private BoundingRectangle bounds;

        public BoundingRectangle Bounds => bounds;


        public Player(Vector2 InitialPosition, int scrollspeed, Map map)
        {
            laserList = new List<Lazer>();
            texture = null;
            position = InitialPosition;
            bounds = new BoundingRectangle(position.X, position.Y, 128, 256);
            laserDelay = 5;
            speed = 200;
            isColliding = false;
            Scrollspeed = scrollspeed;
            _map = map;
            map.Layers.TryGetValue("Tiles" ,out layer);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("sharkAnimation");
            laserTexture = content.Load<Texture2D>("laser");
            flounder = content.Load<Texture2D>("flounder");
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, new Rectangle(hitAnimationFrame * 128, 0, 128, 256), Color.White);
            
            foreach(Lazer l in laserList)
            {
                l.Draw(sb);
            }
        }

        public void Update(GameTime gt)
        {
            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            float t = (float)gt.ElapsedGameTime.TotalSeconds;
            if(hit)
            {
                HitAnimation(gt);
                if (hitAnimationFrame == 23)
                {
                    hit = false;
                    invulnerable = false;
                    hitAnimationFrame = 0;
                }
            }
            if (currentKeyboardState.IsKeyDown(Keys.Space) && priorKeyboardState.IsKeyUp(Keys.Space))
            {
                Shoot();
            }
            if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                position.X = position.X - speed * t;
            }
            if (currentKeyboardState.IsKeyDown(Keys.D))
            {
                position.X = position.X + speed * t;
            }
            if (position.X < 192) position.X = 192;
            if (position.X > 1728 - 128) position.X = 1728 - 128;

            position.Y -= Scrollspeed * t;
            if(CollidesWithTile())
            {
                if (!invulnerable)
                {
                    hit = true;
                    position.Y += 100;
                    invulnerable = true;
                }
            }

            bounds.X = position.X;
            bounds.Y = position.Y;
        }

        public void HitAnimation(GameTime gameTime)
        {
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (hitAnimationTimer > 0)
            {
                hitAnimationTimer -= t;
            }
            else
            {
                hitAnimationTimer = 0.1f;
                if (hitAnimationFrame <= 23) hitAnimationFrame++;
                else hitAnimationFrame = 0;
            }
        }

        public void Shoot()

        {
                Lazer newLaser = new Lazer(laserTexture, new Vector2(position.X, position.Y - 128), Scrollspeed);

                newLaser.isVisible = true;

                if (laserList.Count < 20)
                {
                    laserList.Add(newLaser);
                }
        }

        public void UpdateBullets(GameTime gameTime)
        {
            foreach (Lazer l in laserList)
            {
                if (l.position.Y <= position.Y - 1080)
                {
                    l.isVisible = false;
                }
                l.Update(gameTime, position);
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
