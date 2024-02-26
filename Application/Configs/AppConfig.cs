using UnityEngine;

namespace Samurai.Application.Configs
{
    [CreateAssetMenu(fileName = "App", menuName = "Settings/App")]
    public class AppConfig : Config
    {
        [Header("Scenes")]
        [Tooltip("First scene that is loaded upon game start. It should also be first in build order.")]
        public string AppScene;
        public string SessionScene;
    }
}