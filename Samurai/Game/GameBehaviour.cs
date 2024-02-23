using System.Collections;
using Samurai.Game.Events;
using Samurai.Game.Pooling;
using UnityEngine;

namespace Samurai.Game
{
    public abstract class GameBehaviour : MonoBehaviour
    {
        protected EventAggregator Events;
        protected ComponentPool Pool;

        protected virtual void Awake()
        {
            Events = App.Get<EventAggregator>();
            Pool = App.Get<ComponentPool>();
            
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