namespace Samurai.Game.Pooling
{
    public interface IPoolable
    {
        void OnRetrievedFromPool();
        void OnReturnedToPool();
    }
}