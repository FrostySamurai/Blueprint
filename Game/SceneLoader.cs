using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Samurai.Game
{
    
    public class SceneLoader : MonoBehaviour
    {
        public void LoadInitialScene(string sceneName)
        {
            bool isLoadedProperly = SceneManager.sceneCount == 1 && SceneManager.GetSceneAt(0).name == sceneName;            
            if (!isLoadedProperly)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
        }

        public void LoadScene(LoadSceneParameters parameters)
        {
            StartCoroutine(LoadSceneAsync(parameters));
        }

        private IEnumerator LoadSceneAsync(LoadSceneParameters parameters)
        {
            var op = SceneManager.LoadSceneAsync(parameters.Scene, LoadSceneMode.Additive);
            while (!op.isDone)
            {
                parameters.OnProgressChanged?.Invoke(op.progress);
                yield return null;
            }
            
            parameters.OnFinished?.Invoke();
        }

        public void UnloadScene(LoadSceneParameters parameters)
        {
            StartCoroutine(UnloadSceneAsync(parameters));
        }

        private IEnumerator UnloadSceneAsync(LoadSceneParameters parameters)
        {
            var op = SceneManager.UnloadSceneAsync(parameters.Scene);
            while (!op.isDone)
            {
                parameters.OnProgressChanged?.Invoke(op.progress);
                yield return null;
            }
            
            parameters.OnFinished?.Invoke();
        }
    }
}
