using Samurai.Game;
using UnityEngine;

namespace Samurai.Session.Defs
{
    [CreateAssetMenu(fileName = "Test", menuName = "Definitions/Test")]
    public class TestDefinition : IntDefinition
    {
        public string Value;
    }
}