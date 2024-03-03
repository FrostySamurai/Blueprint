using Samurai.Application;
using Samurai.Application.Events;
using Samurai.Application.Pooling;

namespace Samurai.NSession
{
    public abstract class SessionBehaviour : GameBehaviour
    {
        protected sealed override void Awake()
        {
            if (!Session.Exists())
            {
                return;
            }
            
            InitReferences();
            OnAwake();

            if (Session.HasSave())
            {
                OnLoad();
            }
        }

        protected virtual void OnLoad()
        {
        }

        protected override void InitReferences()
        {
            Events = Session.Get<EventAggregator>();
            Pool = Session.Get<ComponentPool>();
        }
    }
}