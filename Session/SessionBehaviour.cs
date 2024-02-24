using Samurai.Game;
using Samurai.Game.Events;
using Samurai.Game.Pooling;

namespace Samurai.Session
{
    public abstract class SessionBehaviour : GameBehaviour
    {
        protected sealed override void Awake()
        {
            if (!Context.Exists())
            {
                return;
            }
            
            Events = Context.Get<EventAggregator>();
            Pool = Context.Get<ComponentPool>();
            
            OnAwake();
        }
    }
}