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
        [SerializeField] private int port = 5055;
        private Thread _reveiveDataThread;
        private UdpClient _client;
        private string _data;
        private bool _collectData = false;

        public string Data { get { return _data; } }

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
                }

            }
            
        }

        private void OnApplicationQuit()
        {
            StopReceiving();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E)) StartReceiving();
            if(Input.GetKeyDown(KeyCode.Q)) StopReceiving();

            Debug.Log("------------------------------------------");
            Debug.Log("Data: " + _data);
        }
    }
}
