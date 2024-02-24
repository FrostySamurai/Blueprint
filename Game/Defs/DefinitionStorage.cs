using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Samurai.Game.Defs
{
    internal abstract class DefinitionStorage
    {
        
    }
    internal class DefinitionStorage<TDefinition, TKey> : DefinitionStorage, IEnumerable<TDefinition> where TDefinition : Definition, IIdentifiable<TKey>
    {
        private readonly List<TDefinition> _definitions = new();
        private readonly Dictionary<TKey, TDefinition> _definitionsById = new();

        public DefinitionStorage(IEnumerable<object> definitions)
        {
            var typedDefinitions = definitions.Cast<TDefinition>();
            _definitions.AddRange(typedDefinitions);
            foreach (var definition in _definitions)
            {
                _definitionsById[definition.Id] = definition;
            }
        }

        public TDefinition Get(TKey id)
        {
            return _definitionsById.TryGetValue(id, out var definition) ? definition : null;
        }

        public IEnumerator<TDefinition> GetEnumerator()
        {
            return _definitions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}