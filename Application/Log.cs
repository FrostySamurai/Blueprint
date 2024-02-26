namespace Samurai.Application
{
    public static class Log
    {
        public static void Debug(string message, string tag)
        {
            UnityEngine.Debug.Log($"[{tag}] {message}");
        }
        
        public static void Warning(string message, string tag)
        {
            UnityEngine.Debug.LogWarning($"[{tag}] {message}");
        }
        
        public static void Error(string message, string tag)
        {
            UnityEngine.Debug.LogError($"[{tag}] {message}");
        }
    }
}