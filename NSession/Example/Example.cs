using Samurai.Application;
using Samurai.Application.Saving;

namespace Samurai.NSession.Example
{
    public class Example : SessionBehaviour, ISavable
    {
        private const int ExpectedNumber = 10;
        private const string FakeSaveStuff = "fake";

        // this should be handled trough model and instances should have guid assigned
        public string Id => "example";

        protected override void OnAwake()
        {
            Session.Register(this);
        }

        protected override void OnLoad()
        {
            string state = Session.LoadState<string>(Id);
            Log.Debug($"Loaded value '{state}'", "Example");
        }

        protected override void OnStart()
        {
            var exampleModel = Session.Get<ExampleModel>();
            Log.Debug($"Current value of model number is '{exampleModel.Number}'.", "Example");
            exampleModel.Number = ExpectedNumber;
            
            Session.Events.Register<ExampleEvent>(OnExampleEvent, this);

            var @event = new ExampleEvent(exampleModel.Number, Session.Get<ExampleSystem>().CalculateSomeData());
            Session.Events.Raise(@event);
            
            Session.Events.Unregister(this);
            Session.Events.Register<ExampleEvent>(x => Log.Debug($"Lambda callback!", "Example"), this);
            
            Session.Events.Raise(@event);
            
            Session.Events.Unregister<ExampleEvent>(this);
            
            Session.Events.Raise(@event);
        }

        public object GetSaveState()
        {
            return FakeSaveStuff;
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