using UnityEngine;

namespace Samurai.Game
{
    public abstract class IntDefinition : Definition, IIdentifiable<int>
    {
        [SerializeField]
        private int _id;

        public int Id => _id;
    }
}