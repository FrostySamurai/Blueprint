using System;

namespace Samurai.Game
{
    public struct LoadSceneParameters
    {
        public string Scene;
        public Action OnFinished;
        public Action<float> OnProgressChanged;

        public LoadSceneParameters(string scene, Action onFinished = null, Action<float> onProgressChanged = null)
        {
            Scene = scene;
            OnFinished = onFinished;
            OnProgressChanged = onProgressChanged;
        }
    }
}