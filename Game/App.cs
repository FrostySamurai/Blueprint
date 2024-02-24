using System;
using System.Collections.Generic;
using Samurai.Game.Defs;
using Samurai.Game.Events;
using Samurai.Game.Pooling;
using Samurai.Session;
using UnityEditor;

#if UNITY_EDITOR

#else 
using UnityEngine;
#endif

namespace Samurai.Game
{
    public static class App
    {
        internal static string LogTag = "Application";
        
        private static Dictionary<Type, object> _content = new();
        private static SceneLoader _sceneLoader;

        internal static void Init(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;

            Definitions.Create(Get<AppSettings>().DefinitionsFolder);
            
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

        public static void LoadLevel()
        {
            Log.Debug("Starting session.", LogTag);
            
            Context.Create();

            var def = Get<AppSettings>();
            var unloadParameters = new LoadSceneParameters(def.MainMenuScene, OnSceneSwitched);
            var loadParameters = new LoadSceneParameters(def.SessionScene, () => _sceneLoader.UnloadScene(unloadParameters));
            _sceneLoader.LoadScene(loadParameters);

            void OnSceneSwitched()
            {
                Log.Debug("Session started.", LogTag);
            }
        }

        public static void LoadMainMenu()
        {
            Log.Debug("Ending session.", LogTag);
            
            var def = Get<AppSettings>();
            var unloadParameters = new LoadSceneParameters(def.SessionScene, OnSceneSwitched);
            var loadParameters = new LoadSceneParameters(def.MainMenuScene, () => _sceneLoader.UnloadScene(unloadParameters));
            _sceneLoader.LoadScene(loadParameters);

            void OnSceneSwitched()
            {
                Context.Clear();
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
