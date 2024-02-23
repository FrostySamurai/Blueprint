using UnityEngine;

namespace Samurai.Game.Definitions
{
    [CreateAssetMenu(fileName = "App", menuName = "Definitions/App")]
    public class AppDefinition : ScriptableObject
    {
        [Header("Scenes")]
        public string MainMenuScene;
        public string SessionScene;
    }
}