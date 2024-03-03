using System.Collections;
using UnityEngine;

namespace Samurai.Application
{
    public abstract class GameBehaviour : MonoBehaviour
    {
        protected virtual void Awake()
        {
            OnAwake();
        }

        private IEnumerator Start()
        {
            OnStart();

            yield return null;
            
            OnLateStart();
        }

        protected virtual void OnAwake()
        {
            
        }

        protected virtual void OnStart()
        {
            
        }

        protected virtual void OnLateStart()
        {
            
        }
    }
}