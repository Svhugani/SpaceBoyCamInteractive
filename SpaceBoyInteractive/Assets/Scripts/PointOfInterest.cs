using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeomorphicGames
{
    public class PointOfInterest : MonoBehaviour
    {
        [SerializeField] private Utils.ObjectImportance importance = Utils.ObjectImportance.Main;
        [SerializeField] private Vector3 offset = Vector3.zero;
        public Utils.ObjectImportance Importance { get { return importance; } }
        public static List<PointOfInterest> actives = new List<PointOfInterest>();

        private void OnEnable()
        {
            actives.Add(this); 
        }

        private void OnDisable()
        {
            actives.Remove(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(InterestPosition(), .3f);
        }

        public Vector3 InterestPosition()
        {
            return this.transform.position + this.transform.forward * offset.z + this.transform.right * offset.x + this.transform.up * offset.y;
        }
    }
}