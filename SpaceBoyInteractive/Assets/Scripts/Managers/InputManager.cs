using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeomorphicGames
{
    public class InputManager : AbstractManager
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E)) Manager.UDPDataManager.StartReceiving();

            if (Input.GetKeyDown(KeyCode.Q)) Manager.UDPDataManager.StopReceiving();

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Manager.CharacterManager.MoveTo(Manager.EnvironmentManager.GetViewer());
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Manager.CharacterManager.MoveTo(Manager.EnvironmentManager.GetMirror());
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Manager.CharacterManager.MoveTo(Manager.EnvironmentManager.GetMushroom());
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Manager.CharacterManager.MoveTo(Manager.EnvironmentManager.GetTree());
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Manager.CharacterManager.MoveTo(Manager.EnvironmentManager.GetStone());
            }

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                Manager.PostProcessManager.DistortionEffect();
            }
        }
    }
}
