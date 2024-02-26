using UnityEngine;

namespace Samurai.Application
{
    [CreateAssetMenu(fileName = "App", menuName = "Settings/App")]
    public class AppSettings : ScriptableObject
    {
        [Header("Scenes")]
        [Tooltip("First scene that is loaded upon game start. It should also be first in build order.")]
        public string AppScene;
        public string SessionScene;

        [Header("Definitions")]
        [Tooltip("Folder within Resources that contains the definitions.")]
        public string DefinitionsFolder;
    }
}