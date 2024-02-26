using Samurai.Application;
using UnityEngine;
using UnityEngine.UI;

namespace Samurai.NSession.UI
{
    public class SessionView : SessionBehaviour
    {
        [SerializeField]
        private Button _mainMenuButton;

        protected override void OnAwake()
        {
            _mainMenuButton.SetOnClick(App.EndSession);
        }
    }
}
