using System;
using System.Collections.Generic;
using Samurai.Application;
using Samurai.Events;

namespace Samurai.Game
{
    public class Level : IDisposable
    {
        #region Static

        private static Level _instance;
        
        public static void Create()
        {
            _instance = new Level();
            
            Add(new EventAggregator(App.Get<EventAggregator>()));
        }

        public static void Clear()
        {
            _instance?.Dispose();
            _instance = null;
        }

        #endregion Static

        private readonly Dictionary<Type, object> _content = new();
        
        public static bool Exists()
        {
            return _instance != null;
        }

        public static void Add<T>(T obj)
        {
            if (Exists())
            {
                _instance._content[typeof(T)] = obj;
            }
        }

        public static T Get<T>()
        {
            if (!Exists())
            {
                return default;
            }
            
            if (_instance._content.TryGetValue(typeof(T), out var obj))
            {
                return (T)obj;
            }

            return App.Get<T>();
        }

        private Level()
        {
            
        }

        public void Dispose()
        {
            _content.Clear();
            foreach (var entry in _content.Values)
            {
                if (entry is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
