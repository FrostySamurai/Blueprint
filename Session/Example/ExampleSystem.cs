using Samurai.Game;
using Samurai.Game.Defs;
using Samurai.Session.Defs;

namespace Samurai.Session.Example
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