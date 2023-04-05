using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        [Header("SETTINGS")]
        [SerializeField] private Transform offsetTransform;
        [SerializeField] private Vector3 scaleValues = Vector3.one;
        [SerializeField] private float distMultiply = 1f;

        private Vector3 _offset = Vector3.zero;

        public void TrackToData(List<Vector3> data)
        {
            if(data == null || data.Count < 21) return;

            wrist.position = SetPosition(data[0]);
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


        }

        private Vector3 SetPosition(Vector3 positionData)
        {
            Vector3 pos =  Camera.main.ViewportToWorldPoint(new Vector3(positionData.x, positionData.y, distMultiply * positionData.z));
            Debug.Log(pos);
            return Vector3.Scale(scaleValues,  pos);
        }

        private void Update()
        {
            TrackToData(GameManager.Instance.UDPDataManager.HandData);
        }
    }
}
