using UnityEngine;

namespace Samurai.Game.Defs
{
    public abstract class StringDefinition : Definition, IIdentifiable<string>
    {
        [SerializeField]
        private string _id;

        public string Id => _id;
    }
}