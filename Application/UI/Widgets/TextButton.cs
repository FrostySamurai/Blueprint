using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Text = TMPro.TMP_Text;

namespace Samurai.Application.UI.Widgets
{
    public class TextButton : MonoBehaviour
    {
        [SerializeField]
        private Text _text;
        [SerializeField]
        private Button _button;

        public void Init(string text, UnityAction onClick)
        {
            _text.SetText(text);
            _button.SetOnClick(onClick);
        }
    }
}