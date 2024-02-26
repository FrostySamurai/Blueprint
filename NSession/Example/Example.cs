using Samurai.Application;

namespace Samurai.NSession.Example
{
    public class Example : SessionBehaviour
    {
        private const int ExpectedNumber = 10;
        
        protected override void OnStart()
        {
            var exampleModel = Session.Get<ExampleModel>();
            Log.Debug($"Current value of model number is '{exampleModel.Number}'.", "Example");
            exampleModel.Number = ExpectedNumber;
            
            Events.Register<ExampleEvent>(OnExampleEvent, this);

            var @event = new ExampleEvent(exampleModel.Number, Session.Get<ExampleSystem>().CalculateSomeData());
            Events.Raise(@event);
            
            Events.Unregister(this);
            Events.Register<ExampleEvent>(x => Log.Debug($"Lambda callback!", "Example"), this);
            
            Events.Raise(@event);
            
            Events.Unregister<ExampleEvent>(this);
            
            Events.Raise(@event);
        }

        private void OnExampleEvent(ExampleEvent evt)
        {
            if (!evt.IsMatch(ExpectedNumber))
            {
                return;
            }

            Log.Debug($"OnExampleEvent with data {evt.Id} and {evt.Data}", "Example");
        }
    }
}