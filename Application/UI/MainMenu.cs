using Samurai.Application.Saving;
using UnityEngine;
using UnityEngine.UI;

namespace Samurai.Application.UI
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
            // TODO: do the example loading, only show Load button if there are saves
            // TODO: add text field for new game name
            
            string saveName = "game";
            _playButton.SetOnClick(() => App.StartSession(saveName));
            _quitButton.SetOnClick(App.Quit);
        }
    }
}