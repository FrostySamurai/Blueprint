using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Samurai.Game
{
    
    public class Definitions
    {
        internal const string LogTag = "Definitions";
        
        private readonly Dictionary<Type, DefinitionStorage> _storages = new();

        public Definitions(string folder)
        {
            string identifiableTypeName = typeof(IIdentifiable<>).Name;
            var storageType = typeof(DefinitionStorage<,>);
            
            var definitions = Resources.LoadAll(folder);
            var grouped = definitions.GroupBy(x => x.GetType());
            foreach (var group in grouped)
            {
                var definitionType = group.Key;
                var @interface = definitionType.GetInterface(identifiableTypeName);
                if (@interface == null)
                {
                    Log.Error($"No IIdentifiable<TKey> interface found on definition type '{definitionType.Name}'. Skipping..", LogTag);
                    continue;
                }

                var keyType = @interface.GetGenericArguments()[0];
                var currentStorageType = storageType.MakeGenericType(definitionType, keyType);
                object instance = Activator.CreateInstance(currentStorageType, group);
                _storages[definitionType] = (DefinitionStorage)instance;
            }
        }

        public TDefinition Get<TDefinition, TKey>(TKey key) where TDefinition : Definition, IIdentifiable<TKey>
        {
            return GetStorage<TDefinition, TKey>().Get(key);
        }

        public IEnumerable<TDefinition> Get<TDefinition, TKey>() where TDefinition : Definition, IIdentifiable<TKey>
        {
            return GetStorage<TDefinition, TKey>();
        }

        public void Get<TDefinition, TKey>(List<TDefinition> definitions) where TDefinition : Definition, IIdentifiable<TKey>
        {
            definitions.AddRange(GetStorage<TDefinition, TKey>());
        }

        private DefinitionStorage<TDefinition, TKey> GetStorage<TDefinition, TKey>() where TDefinition : Definition, IIdentifiable<TKey>
        {
            if (!_storages.TryGetValue(typeof(TDefinition), out var storage))
            {
                return default;
            }

            return (DefinitionStorage<TDefinition, TKey>)storage;
        }
    }
}