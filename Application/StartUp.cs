using Samurai.Application.Configs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Samurai.Application
{
    /// <summary>
    /// Prefab containing this script should be in every scene (so that you can startup the app from any scene and it starts up properly).
    /// This class should be set to be executed before other scripts in the script execution order.
    /// Event system and standalone input systems should only be present on prefab with this class and disabled.
    /// </summary>
    public class StartUp : MonoBehaviour
    {
        private static bool _wasStartedUp;
        
        [SerializeField]
        private SceneLoader _sceneLoader;
        [SerializeField]
        private EventSystem _eventSystem;
        [SerializeField]
        private StandaloneInputModule _inputModule;
        
        private void Awake()
        {
            if (_wasStartedUp)
            {
                Destroy(gameObject);
                return;
            }
            
            _wasStartedUp = true;
            DontDestroyOnLoad(gameObject);
            
            _eventSystem.enabled = true;
            _inputModule.enabled = true;

            EventSystem.current = _eventSystem;

            Definitions.Create();
            
            App.Init(_sceneLoader);
            
            _sceneLoader.LoadInitialScene(Definitions.Config<AppConfig>().AppScene);
        }
    }
}