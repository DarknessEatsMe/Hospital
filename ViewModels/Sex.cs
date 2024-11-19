namespace Health.ViewModels
{
    public class Sex
    {

        public string Name { get; set; } = null!;
        public bool Value { get; set; }

        public Sex(string name, bool value)
        {
            Name = name;
            Value = value;
        }

    }
}
