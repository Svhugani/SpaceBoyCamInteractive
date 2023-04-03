using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace HomeomorphicGames
{
    public abstract class GeneralRigTracking : MonoBehaviour
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
    }
}
