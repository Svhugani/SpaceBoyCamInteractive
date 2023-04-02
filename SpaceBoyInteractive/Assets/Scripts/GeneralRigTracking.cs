using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace HomeomorphicGames.Sny.Core
{
    public class GeneralRigTracking : MonoBehaviour
    {
        [SerializeField] protected float trackingRange = 10f;
        [SerializeField] protected float smoothing = 2f;
        [SerializeField] protected Transform target;
        [SerializeField] protected Rig targetRig;
        [SerializeField] protected AnimationCurve rigResponseCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        protected float _sqrRange;

        private void Awake()
        {
            _sqrRange = trackingRange * trackingRange;
        }
        private void Update()
        {
            float currentRadius = _sqrRange;
            float rigWeight = 0;

            Vector3 restTarget = this.transform.position + this.transform.forward * 2f;

            foreach (var point in EnvironmentManager.PointsOfInterest)
            {
                Vector3 delta = point.transform.position - this.transform.position;
                float dist = delta.sqrMagnitude;
                if (dist < currentRadius)
                {

                    currentRadius = dist;
                    restTarget = point.transform.position;
                    rigWeight = Vector3.Dot(this.transform.forward, delta.normalized);
                    if (rigWeight < 0) rigWeight = 0;
                }
            }

            target.position = Vector3.Lerp(target.position, restTarget, Time.deltaTime * smoothing);
            targetRig.weight = Mathf.Lerp(targetRig.weight, rigResponseCurve.Evaluate(rigWeight), Time.deltaTime * smoothing);
        }
    }
}
