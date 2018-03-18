using System.Collections.Generic;
using System.Linq;

namespace AirForce
{
    public class Rewinder
    {
        private readonly FlyingObject flyingObject;

        private readonly List<Point2D> savedPositions = new List<Point2D>();
        private readonly List<int> savedStrengths = new List<int>();

        public Rewinder(FlyingObject flyingObject)
        {
            this.flyingObject = flyingObject;
        }

        public void RestorePreviousState()
        {
            if (!CanRestorePreviousState())
                return;

            flyingObject.Position = savedPositions.Last();
            flyingObject.Strength = savedStrengths.Last();
            savedPositions.RemoveAt(savedPositions.Count - 1);
            savedStrengths.RemoveAt(savedStrengths.Count - 1);
        }

        public void SaveState()
        {
            savedPositions.Add(flyingObject.Position);
            savedStrengths.Add(flyingObject.Strength);
        }

        public bool CanRestorePreviousState()
        {
            return savedPositions.Any() &&
                   savedStrengths.Any();
        }
    }
}