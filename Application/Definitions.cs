﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Samurai.Application
{
    
    public class Definitions
    {
        internal const string LogTag = "Definitions";

        private const string DefinitionsFolder = "Definitions";
        private const string ConfigsFolder = "Configs";

        #region Static

        private static Definitions _instance;

        internal static void Create()
        {
            if (_instance != null)
            {
                Log.Warning("An instance of definitions is already created. Skipping creation..", LogTag);
                return;
            }

            _instance = new Definitions();
            Log.Debug("Initialized.", LogTag);
        }

        internal static void Clear()
        {
            _instance = null;
            Log.Debug("Disposed.", LogTag);
        }

        public static T Config<T>() where T : Config
        {
            return _instance._configs.TryGetValue(typeof(T), out var config) ? (T)config : null;
        }
        
        public static T Get<T>(string id) where T : Definition
        {
            if (!_instance._definitions.TryGetValue(typeof(T), out var definitions))
            {
                return null;
            }

            return definitions.TryGetValue(id, out var definition) ? (T)definition : null;
        }

        public static IEnumerable<T> Get<T>(Predicate<T> predicate = null) where T : Definition
        {
            if (!_instance._definitions.TryGetValue(typeof(T), out var definitions))
            {
                return Enumerable.Empty<T>();
            }

            var typed = definitions.Values.Cast<T>();
            return predicate != null ? typed.Where(x => predicate(x)) : typed;
        }

        public static void Get<T>(List<T> result, Predicate<T> predicate = null) where T : Definition
        {
            if (!_instance._definitions.TryGetValue(typeof(T), out var definitions))
            {
                return;
            }

            var typed = definitions.Values.Cast<T>();
            if (predicate == null)
            {
                result.AddRange(typed);
                return;
            }

            result.AddRange(typed.Where(x => predicate(x)));
        }

        #endregion Static
        
        private readonly Dictionary<Type, Dictionary<string, Definition>> _definitions = new();
        private readonly Dictionary<Type, Config> _configs = new();

        private Definitions()
        {
            var definitions = Resources.LoadAll<Definition>(DefinitionsFolder);
            var grouped = definitions.GroupBy(x => x.GetType());
            foreach (var group in grouped)
            {
                var dict = new Dictionary<string, Definition>();
                foreach (var entry in group)
                {
                    if (!dict.TryAdd(entry.Id, entry))
                    {
                        Log.Warning($"Duplicate id '{entry.Id}' for definition type '{group.Key.Name}'.", LogTag);
                    }
                }

                _definitions[group.Key] = dict;
            }

            var settings = Resources.LoadAll<Config>(ConfigsFolder);
            foreach (var setting in settings)
            {
                if (!_configs.TryAdd(setting.GetType(), setting))
                {
                    Log.Warning($"Duplicate settings of type '{_configs.GetType().Name}'.", LogTag);
                }
            }
        }
    }
}