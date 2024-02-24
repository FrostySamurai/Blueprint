using UnityEngine;

namespace Samurai.Game
{
    public interface IIdentifiable<out T>
    {
        public T Id { get; }
    }
    
    public abstract class Definition : ScriptableObject
    {
        
    }
}