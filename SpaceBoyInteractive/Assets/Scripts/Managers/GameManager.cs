using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace HomeomorphicGames
{
    public class GameManager : AbstractManager
    {
        [Header("MANAGERS")]
        [SerializeField] private EnvironmentManager environmentManager;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private CharacterManager characterManager;
        [SerializeField] private UDPDataManager udpDataManager;
        [SerializeField] private PostProcessManager postProcessManager;

        public EnvironmentManager EnvironmentManager { get {return environmentManager ;}}
        public AudioManager AudioManager { get {return audioManager; }}
        public CharacterManager CharacterManager { get {return characterManager; }}
        public UDPDataManager UDPDataManager { get {return UDPDataManager; }}
        public PostProcessManager PostProcessManager { get {return postProcessManager; }}
        private AbstractManager[] _managers;
        public override async Task Prepare()
        {
            _managers = new AbstractManager[] { environmentManager, audioManager, characterManager, udpDataManager };
            List<Task> tasks = new List<Task>();
            foreach (var manager in _managers)
            {
                tasks.Add(manager.Prepare());
                await manager.Prepare();
            }
            await Task.WhenAll(tasks);
        }

        private void Start()
        {
            _ = Prepare();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                characterManager.MoveTo(environmentManager.GetViewer());
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                characterManager.MoveTo(environmentManager.GetMirror());
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                characterManager.MoveTo(environmentManager.GetMushroom());
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                characterManager.MoveTo(environmentManager.GetTree());
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                characterManager.MoveTo(environmentManager.GetStone());
            }
        }
    }
}
