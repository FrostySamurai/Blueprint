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
            
            Events = Session.Get<EventAggregator>();
            Pool = Session.Get<ComponentPool>();
            
            OnAwake();
        }
    }
}