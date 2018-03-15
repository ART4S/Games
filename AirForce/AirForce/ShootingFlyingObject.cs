using System.Drawing;
using System.Windows.Forms;

namespace AirForce
{
    public class ShootingFlyingObject : FlyingObject
    {
        public bool IsShooting { get; private set; }

        public ShootingFlyingObject(FlyingObjectType type, Point2D position, int radius, int movespeed, int strength, Image image)
            : base(type, position, radius, movespeed, strength, image)
        {
        }
    }
}