using System.Threading.Tasks;

namespace Dto
{
    public class ControlEvent
    {
        public string Name { get; private set; }
        public string Target { get; private set; }
        public string Action { get; private set; }
        public string Value { get; private set; }
        public int Delay { get; private set; }
        public bool IsExclusive { get; private set; }
        public bool IsCompleted { get; private set; }

        public float TimeElapsed { get; private set; }

        public ControlEvent(string name, string target, string action, string value, int delay = 0, bool isExclusive = false)
        {
            Name = name;
            Target = target;
            Action = action;
            Value = value;
            Delay = delay;
            IsExclusive = isExclusive;
            TimeElapsed = 0;
        }

        public void Execute()
        {
            // Call "Action" method in "Target" object with "Value" parameter
            // Target.Action(Value);



            // If "Delay" is greater than 0, wait for "Delay" milliseconds before calling the method. after that, set "IsCompleted" to true
            if (Delay > 0)
            {
                Wait(Delay);
            }
            else
            {
                IsCompleted = true;
            }

        }

        public async void Wait(int delay)
        {
            await Task.Delay(delay);
            IsCompleted = true;
        }
    }
}