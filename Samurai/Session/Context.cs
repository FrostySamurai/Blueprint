using System;
using System.Collections.Generic;
using Samurai.Game;
using Samurai.Game.Events;
using Samurai.Session.Example;

namespace Samurai.Session
{
    public class Context : IDisposable
    {
        internal const string LogTag = "Session";
        
        #region Static

        private static Context _instance;
        
        public static void Create()
        {
            _instance = new Context();
            
            Add(new EventAggregator(App.Get<EventAggregator>()));
            Add(new ExampleModel());
            Add(new ExampleSystem());
            
            Log.Debug("Initialized.", LogTag);
        }

        public static void Clear()
        {
            _instance?.Dispose();
            _instance = null;
            
            Log.Debug("Disposed.", LogTag);
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

        private Context()
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
