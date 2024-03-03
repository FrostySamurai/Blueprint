using System.Collections;
using Samurai.Application.Events;
using Samurai.Application.Pooling;
using UnityEngine;

namespace Samurai.Application
{
    public abstract class GameBehaviour : MonoBehaviour
    {
        protected EventAggregator Events;
        protected ComponentPool Pool;

        protected virtual void Awake()
        {
            InitReferences();
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

        protected virtual void InitReferences()
        {
            Events = App.Get<EventAggregator>();
            Pool = App.Get<ComponentPool>();
        }
    }
}