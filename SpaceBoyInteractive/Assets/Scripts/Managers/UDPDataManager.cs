using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;
using System;

namespace HomeomorphicGames
{
    public class UDPDataManager : AbstractManager
    {
        /***
         * Data comes from python UDP that uses mediapipe for hand recognition. Data comes 
         * as sequence of 3-coords positions. After parsing to a list of Vector3 each element of list corresponds to:
         * 
         * 0 - WRIST
         * 1 - THUMB_CMC
         * 2 - THUMB_MCP
         * 3 - THUMB_IP
         * 4 - THUMB_TIP
         * 5 - INDEX_FINGER_MCP
         * 6 - INDEX_FINGER_PIP
         * 7 - INDEX_FINGER_DIP
         * 8 - INDEX_FINGER_TIP
         * 9 - MIDDLE_FINGER_MCP
         * 10 - MIDDLE_FINGER_PIP
         * 11 - MIDDLE_FINGER_DIP
         * 12 - MIDDLE_FINGER_TIP
         * 13 - RING_FINGER_MCP
         * 14 - RING_FINGER_PIP
         * 15 - RING_FINGER_DIP
         * 16 - RING_FINGER_TIP
         * 17 - PINKY_MCP
         * 18 - PINKY_PIP
         * 19 - PINKY_DIP
         * 20 - PINKY_TIP
         * 
         * source mediapipe https://developers.google.com/mediapipe/solutions/vision/hand_landmarker
         ***/

        [SerializeField] private int port = 5055;
        private Thread _reveiveDataThread;
        private UdpClient _client;
        private string _data;
        private bool _collectData = false;

        public List<Vector3> HandData { get { return HandTrackDataParser.HandTracking(_data); } }

        public void StartReceiving()
        {
            _reveiveDataThread = new Thread(new ThreadStart(DataReceiver));
            _reveiveDataThread.IsBackground = true;
            _collectData = true;
            _reveiveDataThread.Start();
            IsBusy = true;
        }

        public void StopReceiving()
        {
            _collectData = false;
            
            if(_reveiveDataThread != null)_reveiveDataThread.Abort();

            if(_client != null) _client.Close();

            IsBusy = false;

        }


        private void DataReceiver()
        {

            _client = new UdpClient(port);
            
            while (_collectData)
            {
                try
                {
                    IPEndPoint ipAdress = new IPEndPoint(IPAddress.Any, port);
                    byte[] dataInBytes = _client.Receive(ref ipAdress);
                    _data = Encoding.UTF8.GetString(dataInBytes);
                }

                catch(Exception exc)
                {
                    Debug.Log(exc.ToString());
                    _data = null;
                }

            }
            
        }

        private void OnApplicationQuit()
        {
            StopReceiving();
        }

    }
}
