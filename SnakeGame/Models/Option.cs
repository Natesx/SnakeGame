namespace SnakeGame.Models
{
    public class Option
    {
        public string Name { get; }
        public Action Selected { get; }

        public Option(string name, Action selected)
        {
            Name = name;
            Selected = selected;
        }

        public static implicit operator Option(List<Option> options)
        {
            throw new NotImplementedException();
        }
    }
}