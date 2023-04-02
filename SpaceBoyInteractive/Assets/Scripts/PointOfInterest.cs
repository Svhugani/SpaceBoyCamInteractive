using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeomorphicGames.Sny.Core
{
    public class PointOfInterest : MonoBehaviour
    {
        [SerializeField] private Utils.Importance importance = Utils.Importance.Main;
        public Utils.Importance Importance { get { return importance; } }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(this.transform.position, .5f);
        }
    }
}