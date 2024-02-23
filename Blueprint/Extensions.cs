using UnityEngine.Events;
using UnityEngine.UI;

namespace Samurai
{
    public static class Extensions
    {
        public static void SetOnClick(this Button @this, UnityAction action)
        {
            @this.onClick.RemoveAllListeners();
            @this.onClick.AddListener(action);
        }
    }
}