using System;
using System.Collections.Generic;
using Samurai.Application;
using Samurai.Application.Events;
using Samurai.NSession.Example;

namespace Samurai.NSession
{
    public class Session
    {
        internal const string LogTag = "Session";
        
        #region Static

        private static Session _instance;
        
        public static void Create()
        {
            _instance = new Session();
            
            Add(new EventAggregator(App.Get<EventAggregator>()));
            Add(new ExampleModel());
            Add(new ExampleSystem());
            
            Log.Debug("Initialized.", LogTag);
        }

        public static void Dispose()
        {
            _instance?.DisposeInternal();
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

        private Session()
        {
            
        }

        private void DisposeInternal()
        {
            foreach (var entry in _content.Values)
            {
                if (entry is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            _content.Clear();
        }
    }
}
