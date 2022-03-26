using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace CollisionExample.Collisions
{
    static class CollisionHelper
    {
        /// <summary>
        /// Detects collision between two BOundingCircles
        /// </summary>
        /// <param name="a">the first circle</param>
        /// <param name="b">the second circle</param>
        /// <returns>true for collision</returns>
        public static bool Collides(BoundingCircle a, BoundingCircle b)
        {
            return Math.Pow(a.Radius + b.Radius, 2) >= Math.Pow(a.Center.X - b.Center.X, 2) + 
                                                       Math.Pow(a.Center.Y - b.Center.Y, 2);
        }

        public static bool Collides(BoundingRectangle a, BoundingRectangle b)
        {
            return !(a.Right < b.Left || a.Left > b.Right ||
                     a.Top > b.Bottom || a.Bottom < b.Top);
        }

        public static bool Collides(BoundingCircle c, BoundingRectangle r)
        {
            float nearestX = MathHelper.Clamp(c.Center.X, r.Left, r.Right);
            float nearestY = MathHelper.Clamp(c.Center.Y, r.Top, r.Bottom);

            return Math.Pow(c.Radius, 2) >= Math.Pow(c.Center.X - nearestX, 2) +
                                            Math.Pow(c.Center.Y - nearestY, 2);
        }
    }
}
