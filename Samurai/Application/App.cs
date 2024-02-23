using System;
using System.Collections.Generic;
using Samurai.Application.Definitions;
using Samurai.Application.Pooling;
using Samurai.Events;
using Samurai.Game;
using UnityEditor;
#if UNITY_EDITOR

#else 
using UnityEngine;
#endif

namespace Samurai.Application
{
    public static class App
    {
        private static Dictionary<Type, object> _content = new();

        internal static void Init()
        {
            Add(new ComponentPool());
            Add(new EventAggregator());
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
            Level.Create();

            var def = Get<AppDefinition>();
            var sceneLoader = Get<SceneLoader>();
            
            var unloadParameters = new LoadSceneParameters(def.MainMenu);
            var loadParameters = new LoadSceneParameters(def.Level, () => sceneLoader.UnloadScene(unloadParameters));
            sceneLoader.LoadScene(loadParameters);
        }

        public static void LoadMainMenu()
        {
            var def = Get<AppDefinition>();
            var sceneLoader = Get<SceneLoader>();
            
            var unloadParameters = new LoadSceneParameters(def.Level, Level.Clear);
            var loadParameters = new LoadSceneParameters(def.MainMenu, () => sceneLoader.UnloadScene(unloadParameters));
            sceneLoader.LoadScene(loadParameters);
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
