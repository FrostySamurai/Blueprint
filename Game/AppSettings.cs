using UnityEngine;

namespace Samurai.Game.Defs
{
    [CreateAssetMenu(fileName = "App", menuName = "Settings/App")]
    public class AppSettings : ScriptableObject
    {
        [Header("Scenes")]
        public string MainMenuScene;
        public string SessionScene;

        [Header("Definitions")]
        public string DefinitionsFolder;
    }
}