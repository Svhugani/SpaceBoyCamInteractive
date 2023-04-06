using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace HomeomorphicGames
{
    public class HandInteraction : MonoBehaviour
    {
        [Header("HAND JOINTS MAPPING")]
        [SerializeField] private Transform wrist;
        [SerializeField] private Transform thumbCMC;
        [SerializeField] private Transform thumbMCP;
        [SerializeField] private Transform thumbIP;
        [SerializeField] private Transform thumbTIP;
        [SerializeField] private Transform indexFingerMCP;
        [SerializeField] private Transform indexFingerPIP;
        [SerializeField] private Transform indexFingerDIP;
        [SerializeField] private Transform indexFingerTIP;
        [SerializeField] private Transform middleFingerMCP;
        [SerializeField] private Transform middleFingerPIP;
        [SerializeField] private Transform middleFingerDIP;
        [SerializeField] private Transform middleFingerTIP;
        [SerializeField] private Transform ringFingerMCP;
        [SerializeField] private Transform ringFingerPIP;
        [SerializeField] private Transform ringFingerDIP;
        [SerializeField] private Transform ringFingerTIP;
        [SerializeField] private Transform pinkyMCP;
        [SerializeField] private Transform pinkyPIP;
        [SerializeField] private Transform pinkyDIP;
        [SerializeField] private Transform pinkyTIP;

        [Header("RIG")]
        [SerializeField] private Rig rig;
        [SerializeField] private float rigWeightSmoothing = 1f;

        [Header("SETTINGS")]
        [SerializeField] private Transform offsetTransform;
        [SerializeField] private Vector3 scaleValues = Vector3.one;
        [SerializeField] private float distMultiply = 1f;
        [SerializeField] private float smoothing = 2f;
        [SerializeField] private float interactionDist = 1f;

        private bool _isFullHandDetected = false;
        public bool IsFullHandDetected { get { return _isFullHandDetected; } }  

        private Vector3 _offset = Vector3.zero;

        public void TrackToData(List<Vector3> data, float timeStep)
        {
            if (data == null || data.Count < 21)
            {
                _isFullHandDetected = false;
                return;
            }

            wrist.position = Vector3.Lerp(wrist.position, SetPosition(data[0]), timeStep);
            /*            thumbCMC.position = SetPosition(data[1]);
                        thumbMCP.position = SetPosition(data[2]);
                        thumbIP.position = SetPosition(data[3]);
                        thumbTIP.position = SetPosition(data[4]);
                        indexFingerMCP.position = SetPosition(data[5]); 
                        indexFingerPIP.position = SetPosition(data[6]); 
                        indexFingerDIP.position = SetPosition(data[7]); 
                        indexFingerTIP.position = SetPosition(data[8]); 
                        middleFingerMCP.position = SetPosition(data[9]);    
                        middleFingerPIP.position = SetPosition(data[10]);
                        middleFingerDIP.position = SetPosition(data[11]);
                        middleFingerTIP.position = SetPosition(data[12]);
                        ringFingerMCP.position = SetPosition(data[13]); 
                        ringFingerPIP.position = SetPosition(data[14]); 
                        ringFingerDIP.position = SetPosition(data[15]); 
                        ringFingerTIP.position = SetPosition(data[16]);     
                        pinkyMCP.position = SetPosition(data[17]);  
                        pinkyPIP.position = SetPosition(data[18]);
                        pinkyDIP.position = SetPosition(data[19]);  
                        pinkyTIP.position = SetPosition(data[20]);*/
            thumbTIP.position = Vector3.Lerp(thumbTIP.position, SetPosition(data[4]), timeStep);
            indexFingerTIP.position = Vector3.Lerp(indexFingerTIP.position, SetPosition(data[8]), timeStep);
            middleFingerTIP.position = Vector3.Lerp(middleFingerTIP.position, SetPosition(data[12]), timeStep);
            ringFingerTIP.position = Vector3.Lerp(ringFingerTIP.position, SetPosition(data[16]), timeStep);
            pinkyTIP.position = Vector3.Lerp(pinkyTIP.position, SetPosition(data[20]), timeStep);

            _isFullHandDetected = true;
        }

        private Vector3 SetPosition(Vector3 positionData)
        {
/*            Vector3 pos =  Camera.main.ViewportToWorldPoint(new Vector3(
                scaleValues.x * positionData.x, 
                scaleValues.y * positionData.y,
                scaleValues.z * distMultiply * positionData.z));*/


            Vector3 pos = new Vector3(
                scaleValues.x * (1 - positionData.x),
                scaleValues.y * positionData.y,
                scaleValues.z * distMultiply * positionData.z);
            Debug.Log("Realtive pos: " + pos);
            return pos + offsetTransform.position;
        }

        private void Update()
        {
            TrackToData(GameManager.Instance.UDPDataManager.GetHandData(), smoothing * Time.deltaTime);
            float targetWeight = IsFullHandDetected ? 1.0f : 0.0f;
            rig.weight = Mathf.Lerp(rig.weight, targetWeight, rigWeightSmoothing * Time.deltaTime);
        }

        private void LateUpdate()
        {
            
        }
    }
}
