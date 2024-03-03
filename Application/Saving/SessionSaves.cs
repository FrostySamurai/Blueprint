using System.Collections.Generic;

namespace Samurai.Application.Saving
{
    public readonly struct SessionSaves
    {
        private readonly List<string> _saveFiles;
        
        public readonly string SessionId;
        public IReadOnlyList<string> SaveFiles => _saveFiles;

        public SessionSaves(string sessionId, IEnumerable<string> saves)
        {
            SessionId = sessionId;
            _saveFiles = new List<string>(saves);
        }

        public bool IsValid() => !string.IsNullOrEmpty(SessionId);
    }
}