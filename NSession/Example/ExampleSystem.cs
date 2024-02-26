using Samurai.Application.NDefinitions;
using Samurai.NSession.NDefinitions;

namespace Samurai.NSession.Example
{
    public class ExampleSystem
    {
        public string CalculateSomeData()
        {
            var def = Definitions.Get<TestDefinition>("test");
            if (def != null)
            {
                return def.Value;
            }
            
            return "Calculating some data";
        }
    }
}