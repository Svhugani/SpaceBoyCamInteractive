using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace HomeomorphicGames
{
    public class EnvironmentManager : AbstractManager
    {
        [Header("SPECIFIC POINTS OF INTEREST")]
        [SerializeField] private PointOfInterest viewer;
        [SerializeField] private PointOfInterest mirror;
        [SerializeField] private PointOfInterest mushroom;
        [SerializeField] private PointOfInterest stone;
        [SerializeField] private PointOfInterest tree;


        public PointOfInterest GetViewer()
        {
            return viewer;
        }

        public PointOfInterest GetMirror()
        {
            return mirror;
        }

        public PointOfInterest GetMushroom()
        {
            return mushroom;
        }

        public PointOfInterest GetStone()
        {
            return stone;
        }

        public PointOfInterest GetTree()
        {
            return tree;
        }

    }
}
