using Samurai.Application.NDefinitions;
using UnityEngine;

namespace Samurai.NSession.NDefinitions
{
    [CreateAssetMenu(fileName = "Test", menuName = "Definitions/Test")]
    public class TestDefinition : Definition
    {
        [Space]
        public string Value;
    }
}