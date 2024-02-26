namespace Samurai.Application.Pooling
{
    public interface IPoolable
    {
        void OnRetrievedFromPool();
        void OnReturnedToPool();
    }
}