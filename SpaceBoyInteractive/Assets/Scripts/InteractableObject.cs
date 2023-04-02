using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeomorphicGames
{
    [RequireComponent(typeof(Animator))]
    public class InteractableObject : EnvironmentObject
    {
        private Animator _animator;
        private int _idleTriggerHash;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _idleTriggerHash = Animator.StringToHash("GoIdle");
            GoIdle();
        }

        public virtual void GoIdle()
        {
            _animator.SetTrigger(_idleTriggerHash);
        }
    }
}
