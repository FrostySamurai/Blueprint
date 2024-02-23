using System;
using System.Collections.Generic;

namespace Samurai.Game.Events
{
    public class EventAggregator : IDisposable
    {
        private EventAggregator _parent;
        private List<EventAggregator> _children = new();

        private Dictionary<Type, EventChannel> _channels = new();

        #region Lifecycle

        public EventAggregator(EventAggregator parent = null)
        {
            _parent = parent;
            _parent?.Add(this);
        }

        public void Dispose()
        {
            _parent?.Remove(this);
        }

        #endregion Lifecycle

        private void Add(EventAggregator child)
        {
            _children.Add(child);
        }

        private void Remove(EventAggregator child)
        {
            _children.Remove(child);
        }

        #region Events

        public void Register<T>(Action<T> callback, object source) where T : IEvent
        {
            GetChannel<T>().Register(callback, source);
        }

        public void Unregister<T>(Action<T> callback, object source) where T : IEvent
        {
            GetChannel<T>().Unregister(callback, source);
        }

        public void Unregister<T>(object source) where T : IEvent
        {
            GetChannel<T>().Unregister(source);
        }

        public void Raise<T>(T @event) where T : IEvent
        {
            GetChannel<T>().Raise(@event);
            foreach (var child in _children)
            {
                child.Raise(@event);
            }
        }

        #endregion Events

        #region Private

        private EventChannel<T> GetChannel<T>() where T : IEvent
        {
            var type = typeof(T);
            if (_channels.TryGetValue(type, out var channel))
            {
                return (EventChannel<T>)channel;
            }

            var typedChannel = new EventChannel<T>();
            _channels[type] = typedChannel;
            return typedChannel;
        }

        #endregion Private
    }
}