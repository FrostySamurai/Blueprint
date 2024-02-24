using Samurai.Game.Defs;
using UnityEngine;

namespace Samurai.Session.Defs
{
    [CreateAssetMenu(fileName = "Test", menuName = "Definitions/Test")]
    public class TestDefinition : IntDefinition
    {
        public string Value;
    }
}