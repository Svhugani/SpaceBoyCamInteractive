using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HomeomorphicGames
{
    public class SpaceBoiChar : MonoBehaviour
    {
        [Header("COMPONENTS")]
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;
        [Header("TARGETS")]
        [SerializeField] private Transform dofTarget;
        [Header("MOVEMENT PARAMETERS")]
        [SerializeField] private float walkParameter = 1f;
        [SerializeField] private float runParameter = 3f;
        [SerializeField] private float walkAngularSpeed = 240;
        [SerializeField] private float runAngularSpeed = 400;
        [SerializeField] private float walkSpeed = 3;
        [SerializeField] private float runSpeed = 6;
        [SerializeField] private float minDistanceToRun = 5f;
        [SerializeField] private AnimationCurve velocityResponse = AnimationCurve.EaseInOut(0, 0, 1, 1);

        private float _targetMoveParam;
        private float _currentMoveParam;
        private Coroutine _agentMovementRoutine;

        public delegate void MovementEvent(Vector3 position);
        public event MovementEvent OnDestinationReached;
        public event MovementEvent OnDestinationSet;
        

        private int _animSpeedHash;
        private void Awake()
        {
            _animSpeedHash = Animator.StringToHash("Speed");
        }

        private void OnEnable()
        {
            OnDestinationReached += DebugDestReached;
            OnDestinationSet += DebugDestStarted;
        }

        private void OnDisable()
        {
            OnDestinationReached -= DebugDestReached;
            OnDestinationSet -= DebugDestStarted;   
        }

        public void DebugTest(string msg, Vector3 pos)
        {
            Debug.Log(msg + pos.ToString());
        }

        public void DebugDestReached(Vector3 pos)
        {
            DebugTest("Destination reached: ", pos);
        }

        public void DebugDestStarted(Vector3 pos)
        {
            DebugTest("Destination Started: ", pos);
        }

        public Vector3 GetDofTargetPos()
        {
            return dofTarget.position;
        }

        private void UpdateAnimator()
        {
            _currentMoveParam = _targetMoveParam * velocityResponse.Evaluate(agent.velocity.magnitude / agent.speed);
            animator.SetFloat(_animSpeedHash, _currentMoveParam);
        }

        public void MoveTo(Vector3 destination)
        {
            float dist = Vector3.Distance(destination, this.transform.position);

            //if (dist >= minDistanceToRun)
            if (false)
            {
                _targetMoveParam = runParameter;
                agent.speed = runSpeed;
                agent.angularSpeed = runAngularSpeed;
            }

            else
            {
                _targetMoveParam = walkParameter;
                agent.speed = walkSpeed;
                agent.angularSpeed = walkAngularSpeed;
            }

            agent.destination = destination;
            
            _currentMoveParam = animator.GetFloat(_animSpeedHash);

            OnDestinationSet?.Invoke(destination);

            if (_agentMovementRoutine != null) StopCoroutine(_agentMovementRoutine);
            _agentMovementRoutine = StartCoroutine(AgentMvementRoutine());
        }


        private void Update()
        {
            UpdateAnimator();


/*            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
                {
                    MoveTo(hitInfo.point);
                }
            }*/
        }

        private IEnumerator AgentMvementRoutine()
        {
            while (agent.remainingDistance > agent.stoppingDistance * 1.05f)
            {
                yield return null;
            }
            OnDestinationReached?.Invoke(agent.destination);
        }

        private void OnAnimatorMove()
        {
            //Debug.Log("Animator delta magnitude: " + animator.deltaPosition.magnitude);
        }
    }
}


