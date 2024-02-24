using Samurai.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Samurai.Session.UI
{
    public class SessionView : SessionBehaviour
    {
        [SerializeField]
        private Button _mainMenuButton;

        protected override void OnAwake()
        {
            _mainMenuButton.SetOnClick(App.LoadMainMenu);
        }
    }
}
