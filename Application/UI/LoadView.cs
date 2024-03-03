using Samurai.Application.Saving;
using Samurai.Application.UI.Widgets;
using UnityEngine;
using UnityEngine.UI;

namespace Samurai.Application.UI
{
    public class LoadView : GameBehaviour
    {
        [SerializeField]
        private TextButton _buttonPrefab;
        
        [Header("Layout")]
        [SerializeField]
        private RectTransform _sessionsPanel;
        [SerializeField]
        private RectTransform _savesPanel;
        [SerializeField]
        private RectTransform _mainMenu;
        
        [Header("Interaction")]
        [SerializeField]
        private Button _mainMenuButton;
        [SerializeField]
        private Button _backButton;

        private SaveSystem _saves;

        #region Lifecycle

        public void Init()
        {
            InitReferences();
            _saves = App.Get<SaveSystem>();
            
            Pool.ReturnChildren(_buttonPrefab, _sessionsPanel);
            Pool.ReturnChildren(_buttonPrefab, _savesPanel);
            foreach (var entry in _saves.Saves)
            {
                string sessionId = entry.SessionId;
                var instance = Pool.Retrieve(_buttonPrefab, _sessionsPanel);
                instance.Init(entry.SessionId, () => ShowSaves(sessionId));
            }

            _backButton.SetOnClick(() =>
            {
                _savesPanel.gameObject.SetActive(false);
                _sessionsPanel.gameObject.SetActive(true);
            });
            
            _mainMenuButton.SetOnClick(() =>
            {
                _mainMenu.gameObject.SetActive(true);
                gameObject.SetActive(false);
            });
        }

        #endregion Lifecycle

        #region Bindings

        private void ShowSaves(string sessionId)
        {
            Pool.ReturnChildren(_buttonPrefab, _savesPanel);

            var saves = _saves.GetSaves(sessionId);
            if (!saves.IsValid())
            {
                return;
            }
                
            _savesPanel.gameObject.SetActive(true);
            _sessionsPanel.gameObject.SetActive(false);

            foreach (string entry in saves.SaveFiles)
            {
                string saveName = entry;
                var instance = Pool.Retrieve(_buttonPrefab, _savesPanel);
                instance.Init(saveName, () => LoadSave(sessionId, saveName));
            }
        }

        private void LoadSave(string sessionId, string saveName)
        {
            App.StartSession(sessionId, saveName);
        }

        #endregion Bindings
    }
}