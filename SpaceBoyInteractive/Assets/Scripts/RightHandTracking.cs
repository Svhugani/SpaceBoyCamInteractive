using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeomorphicGames.Sny.Core
{
    public class RightHandTracking : GeneralRigTracking
    {
        
        private void Update()
        {
/*            float currentRadius = _sqrRange;
            float rigWeight = 0;

            Vector3 restTarget = this.transform.position;

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
            targetRig.weight = Mathf.Lerp(targetRig.weight, rigResponseCurve.Evaluate(rigWeight), Time.deltaTime * smoothing);*/
        }
    }
}
