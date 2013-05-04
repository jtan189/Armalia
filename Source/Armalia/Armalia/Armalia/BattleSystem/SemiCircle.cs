using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Armalia.BattleSystem
{
    class SemiCircle
    {

        public CircleHalf Half { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int R { get; set; }

        SemiCircle(int originX, int originY, int radius, CircleHalf half)
        {
            this.Half = half;
            this.X = X;
            this.Y = Y;
            this.R = R;
        }

        // http://stackoverflow.com/questions/401847/circle-rectangle-collision-detection-intersection/402010#402010
        bool Intersects(Rectangle rect)
        {
            // TODO: account for CircleHalf

            Point circleDistance = new Point(Math.Abs(this.X - rect.X), Math.Abs(this.Y - rect.Y));

            if (circleDistance.X > (rect.Width / 2 + this.R)) { return false; }
            if (circleDistance.Y > (rect.Height / 2 + this.R)) { return false; }

            if (circleDistance.X <= (rect.Width / 2)) { return true; }
            if (circleDistance.Y <= (rect.Height / 2)) { return true; }

            int cornerDistanceSquared = (this.X - rect.Width / 2) ^ 2 +
                                 (circleDistance.Y - rect.Height / 2) ^ 2;

            return (cornerDistanceSquared <= (this.R ^ 2));

        }

        public enum CircleHalf
        {
            Upper,
            Lower,
            Left,
            Right
        }
    }
}
