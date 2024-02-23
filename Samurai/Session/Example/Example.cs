using Samurai.Game;
using Samurai.Game.Events;
using UnityEngine;

namespace Samurai.Session.Example
{
    public class Example : MonoBehaviour
    {
        private void Start()
        {
            Context.Get<EventAggregator>().Register<ExampleEvent>(OnExampleEvent, this);

            var @event = new ExampleEvent(Context.Get<ExampleModel>().Number, Context.Get<ExampleSystem>().CalculateSomeData());
            Context.Get<EventAggregator>().Raise(@event);
        }

        private void OnExampleEvent(ExampleEvent evt)
        {
            int expectedId = 6;
            if (!evt.IsMatch(expectedId))
            {
                return;
            }

            Log.Debug($"OnExampleEvent with data {evt.Id} and {evt.Data}", "Example");
        }
    }
}