using Samurai.Game;

namespace Samurai.Session.Example
{
    public class Example : SessionBehaviour
    {
        protected override void OnStart()
        {
            Events.Register<ExampleEvent>(OnExampleEvent, this);

            var @event = new ExampleEvent(Context.Get<ExampleModel>().Number, Context.Get<ExampleSystem>().CalculateSomeData());
            Events.Raise(@event);
            
            Events.Unregister(this);
            Events.Register<ExampleEvent>(x => Log.Debug($"Lambda callback!", "Example"), this);
            
            Events.Raise(@event);
            
            Events.Unregister<ExampleEvent>(this);
            
            Events.Raise(@event);
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