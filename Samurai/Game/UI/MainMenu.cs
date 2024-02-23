using UnityEngine;
using UnityEngine.UI;

namespace Samurai.Game.UI
{
    public class MainMenu : GameBehaviour
    {
        [Header("Interaction")]
        [SerializeField]
        private Button _playButton;
        [SerializeField]
        private Button _quitButton;

        protected override void OnStart()
        {
            _playButton.SetOnClick(App.LoadLevel);
            _quitButton.SetOnClick(App.Quit);
        }
    }
}