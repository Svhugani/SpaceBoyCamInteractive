using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HomeomorphicGames
{

#if UNITY_EDITOR
    [CustomEditor(typeof(GameManager))]
    public class GameManager_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            var manager = (GameManager)target;
            if (manager == null) return;

            base.OnInspectorGUI();
            if (GUILayout.Button("Set DoF to character"))
            {
                manager.PostProcessManager.SetDoFFocusDist(manager.CharacterManager.DistanceFromCamera());
            }
        }
    }
#endif

    public class GameManager : AbstractManager
    {
        public static GameManager Instance { get; private set; }    

        [Header("MANAGERS")]
        [SerializeField] private EnvironmentManager environmentManager;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private CharacterManager characterManager;
        [SerializeField] private UDPDataManager udpDataManager;
        [SerializeField] private PostProcessManager postProcessManager;
        [SerializeField] private InputManager inputManager;

        public EnvironmentManager EnvironmentManager { get {return environmentManager ;}}
        public AudioManager AudioManager { get {return audioManager; }}
        public CharacterManager CharacterManager { get {return characterManager; }}
        public UDPDataManager UDPDataManager { get {return UDPDataManager; }}
        public PostProcessManager PostProcessManager { get {return postProcessManager; }}
        public InputManager InputManager { get { return inputManager; }}    

        private AbstractManager[] _managers;
        public override async Task Prepare()
        {
            _managers = new AbstractManager[] { environmentManager, audioManager, characterManager, udpDataManager, inputManager };
            List<Task> tasks = new List<Task>();
            foreach (var manager in _managers)
            {
                tasks.Add(manager.Prepare());
                await manager.Prepare();
            }
            await Task.WhenAll(tasks);
        }


        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
        }
        private void Start()
        {
            _ = Prepare();
        }

        private void Update()
        {
            PostProcessManager.SetDoFFocusDist(CharacterManager.DistanceFromCamera());

        }
    }
}
