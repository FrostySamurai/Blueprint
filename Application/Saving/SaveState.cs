using System.Collections.Generic;
using Newtonsoft.Json;

namespace Samurai.Application.Saving
{
    public class SaveState
    {
        private readonly Dictionary<string, string> _state;

        public SaveState(IDictionary<string, string> state)
        {
            _state = new Dictionary<string, string>(state);
        }

        public bool TryGet<T>(string id, out T state)
        {
            state = default;
            if (!_state.TryGetValue(id, out string savedState))
            {
                return false;
            }

            state = JsonConvert.DeserializeObject<T>(savedState);
            return true;
        }

        public T Get<T>(string id)
        {
            if (!_state.TryGetValue(id, out string state))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(state);
        }
    }
}