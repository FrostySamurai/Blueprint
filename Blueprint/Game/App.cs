using System;
using System.Collections.Generic;
using Samurai.Game.Definitions;
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

        internal static void Init()
        {
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

            var def = Get<AppDefinition>();
            var sceneLoader = Get<SceneLoader>();
            
            var unloadParameters = new LoadSceneParameters(def.MainMenuScene, OnSceneSwitched);
            var loadParameters = new LoadSceneParameters(def.SessionScene, () => sceneLoader.UnloadScene(unloadParameters));
            sceneLoader.LoadScene(loadParameters);

            void OnSceneSwitched()
            {
                Log.Debug("Session started.", LogTag);
            }
        }

        public static void LoadMainMenu()
        {
            Log.Debug("Ending session.", LogTag);
            
            var def = Get<AppDefinition>();
            var sceneLoader = Get<SceneLoader>();
            
            var unloadParameters = new LoadSceneParameters(def.SessionScene, OnSceneSwitched);
            var loadParameters = new LoadSceneParameters(def.MainMenuScene, () => sceneLoader.UnloadScene(unloadParameters));
            sceneLoader.LoadScene(loadParameters);

            void OnSceneSwitched()
            {
                Context.Clear();
                Log.Debug("Session ended.", LogTag);
            }
        }
        
        public static void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
