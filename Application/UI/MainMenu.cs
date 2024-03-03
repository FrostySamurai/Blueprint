using UnityEngine;
using UnityEngine.UI;

namespace Samurai.Application.UI
{
    public class MainMenu : GameBehaviour
    {
        [Header("Layout")]
        [SerializeField]
        private RectTransform _menuPanel;
        [SerializeField]
        private LoadView _loadView;
        
        [Header("Interaction")]
        [SerializeField]
        private Button _playButton;
        [SerializeField]
        private Button _loadButton;
        [SerializeField]
        private Button _quitButton;

        protected override void OnStart()
        {
            // TODO: add text field for new game name
            
            _menuPanel.gameObject.SetActive(true);
            _loadView.gameObject.SetActive(false);
            
            string saveName = "game";
            _playButton.SetOnClick(() => App.StartSession(saveName));
            _loadButton.SetOnClick(() =>
            {
                _menuPanel.gameObject.SetActive(false);
                _loadView.Init();
                _loadView.gameObject.SetActive(true);
            });
            _quitButton.SetOnClick(App.Quit);
        }
    }
}