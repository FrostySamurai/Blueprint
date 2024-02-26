using Samurai.Application.Events;

namespace Samurai.NSession.Example
{
    public struct ExampleEvent : IEvent
    {
        public int Id;
        public string Data;

        public ExampleEvent(int id, string data)
        {
            Id = id;
            Data = data;
        }

        public bool IsMatch(int number, string data)
        {
            return IsMatch(number) && IsMatch(data);
        }

        public bool IsMatch(string data)
        {
            return Data == data;
        }

        public bool IsMatch(int number)
        {
            return Id == number;
        }
    }
}