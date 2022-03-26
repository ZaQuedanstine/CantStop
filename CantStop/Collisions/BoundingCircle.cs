using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace CollisionExample.Collisions
{
    /// <summary>
    /// A Struct representing circular bounds
    /// </summary>
    public struct BoundingCircle
    {
        /// <summary>
        /// The Center of the circle
        /// </summary>
        public Vector2 Center;

        /// <summary>
        /// The radius of the circle
        /// </summary>
        public float Radius;

        public BoundingCircle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// tests for a collison between this and another bounding circle
        /// </summary>
        /// <param name="other"the other circle></param>
        /// <returns>true if collison false otherwise</returns>
        public bool CollidesWith(BoundingCircle other)
        {
            return CollisionHelper.Collides(this, other);
        }

        public bool CollidesWith(BoundingRectangle other)
        {
            return CollisionHelper.Collides(this, other);
        }
    }
}
