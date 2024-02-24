using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Samurai.Game.Defs
{
    
    public class Definitions
    {
        internal const string LogTag = "Definitions";

        #region Static

        private static Definitions _instance;

        internal static void Create(string folder)
        {
            if (_instance != null)
            {
                Log.Warning("An instance of definitions is already created. Skipping creation..", LogTag);
                return;
            }

            _instance = new Definitions(folder);
            Log.Debug("Initialized.", LogTag);
        }

        internal static void Clear()
        {
            _instance = null;
            Log.Debug("Disposed.", LogTag);
        }
        
        public static TDefinition Get<TDefinition, TKey>(TKey key) 
            where TDefinition : Definition, IIdentifiable<TKey>
        {
            return _instance.TryGetStorage<TDefinition, TKey>(out var storage) ? storage.Get(key) : null;
        }

        public static IEnumerable<TDefinition> Get<TDefinition, TKey>() 
            where TDefinition : Definition, IIdentifiable<TKey>
        {
            return _instance.TryGetStorage<TDefinition, TKey>(out var storage) ? storage : null;
        }

        public static void Get<TDefinition, TKey>(List<TDefinition> result, Predicate<TDefinition> predicate = null)
            where TDefinition : Definition, IIdentifiable<TKey>
        {
            if (!_instance.TryGetStorage<TDefinition, TKey>(out var storage))
            {
                return;
            }
            
            if (predicate == null)
            {
                result.AddRange(storage);
                return;
            }
            
            foreach (var definition in storage)
            {
                if (predicate(definition))
                {
                    result.Add(definition);
                }
            }
        }

        #endregion Static
        
        private readonly Dictionary<Type, DefinitionStorage> _storages = new();

        private Definitions(string folder)
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
                    Log.Warning($"No IIdentifiable<TKey> interface found on definition type '{definitionType.Name}'. Skipping..", LogTag);
                    continue;
                }

                var keyType = @interface.GetGenericArguments()[0];
                var currentStorageType = storageType.MakeGenericType(definitionType, keyType);
                object instance = Activator.CreateInstance(currentStorageType, group);
                _storages[definitionType] = (DefinitionStorage)instance;
            }
        }

        private bool TryGetStorage<TDefinition, TKey>(out DefinitionStorage<TDefinition, TKey> result) where TDefinition : Definition, IIdentifiable<TKey>
        {
            result = null;
            if (!_storages.TryGetValue(typeof(TDefinition), out var storage))
            {
                return false;
            }

            result = (DefinitionStorage<TDefinition, TKey>)storage; 
            return true;
        }
    }
}