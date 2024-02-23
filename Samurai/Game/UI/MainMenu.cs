using UnityEngine;
using UnityEngine.UI;

namespace Samurai.Game.UI
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Interaction")]
        [SerializeField]
        private Button _playButton;
        [SerializeField]
        private Button _quitButton;

        private void Start()
        {
            _playButton.SetOnClick(App.LoadLevel);
            _quitButton.SetOnClick(App.Quit);
        }
    }
}