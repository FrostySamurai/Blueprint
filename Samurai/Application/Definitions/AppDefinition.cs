using UnityEngine;

namespace Samurai.Application.Definitions
{
    [CreateAssetMenu(fileName = "GlobalSettings", menuName = "Manic/Settings/Global")]
    public class AppDefinition : ScriptableObject
    {
        [Header("Scenes")]
        public string MainMenu;
        public string Level;
    }
}