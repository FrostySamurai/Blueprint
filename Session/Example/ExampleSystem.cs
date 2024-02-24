using Samurai.Game;
using Samurai.Game.Defs;
using Samurai.Session.Defs;

namespace Samurai.Session.Example
{
    public class ExampleSystem
    {
        public string CalculateSomeData()
        {
            var def = Definitions.Get<TestDefinition, int>(0);
            if (def != null)
            {
                return def.Value;
            }
            
            return "Calculating some data";
        }
    }
}