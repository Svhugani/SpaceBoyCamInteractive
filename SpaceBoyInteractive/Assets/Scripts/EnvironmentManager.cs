using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HomeomorphicGames.Sny.Core
{
    public class EnvironmentManager : AbstractManager
    {
        private static List<PointOfInterest> _pointsOfInterest;

        public static List<PointOfInterest> PointsOfInterest { get { return _pointsOfInterest; } }

        private void Awake()
        {
            _pointsOfInterest = FindObjectsOfType<PointOfInterest>(true).ToList();
        }
    }
}
