using Samurai.Application;

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
            
            OnAwake();

            if (Session.HasSave())
            {
                OnLoad();
            }
        }

        protected virtual void OnLoad()
        {
        }
    }
}