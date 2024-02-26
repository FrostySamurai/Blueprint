using System;
using System.Collections.Generic;
using Samurai.Application.Configs;
using Samurai.Application.Events;
using Samurai.Application.Pooling;
using Samurai.NSession;
using UnityEditor;

#if UNITY_EDITOR

#else 
using UnityEngine;
#endif

namespace Samurai.Application
{
    public static class App
    {
        internal static string LogTag = "Application";
        
        private static Dictionary<Type, object> _content = new();
        private static SceneLoader _sceneLoader;

        internal static void Init(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            
            Add(new ComponentPool());
            Add(new EventAggregator());

            Log.Debug("Initialized.", LogTag);
        }
        
        internal static void Add<T>(T obj)
        {
            _content[typeof(T)] = obj;
        }

        public static T Get<T>()
        {
            return _content.TryGetValue(typeof(T), out var obj) ? (T)obj : default;
        }

        public static void StartSession()
        {
            Log.Debug("Starting session.", LogTag);
            
            Session.Create();

            var def = Definitions.Config<AppConfig>();
            var unloadParameters = new LoadSceneParameters(def.AppScene, OnSceneSwitched);
            var loadParameters = new LoadSceneParameters(def.SessionScene, () => _sceneLoader.UnloadScene(unloadParameters));
            _sceneLoader.LoadScene(loadParameters);

            void OnSceneSwitched()
            {
                Log.Debug("Session started.", LogTag);
            }
        }

        public static void EndSession()
        {
            Log.Debug("Ending session.", LogTag);
            
            var def = Definitions.Config<AppConfig>();
            var unloadParameters = new LoadSceneParameters(def.SessionScene, OnSceneSwitched);
            var loadParameters = new LoadSceneParameters(def.AppScene, () => _sceneLoader.UnloadScene(unloadParameters));
            _sceneLoader.LoadScene(loadParameters);

            void OnSceneSwitched()
            {
                Session.Dispose();
                Log.Debug("Session ended.", LogTag);
            }
        }
        
        public static void Quit()
        {
            Definitions.Clear();
            
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
