namespace Health.ViewModels
{
    public class Money
    {
        public string? Name { get; set; }
        public int? Price { get; set; }

        public Money(string name, int money) {
            Name = name;
            Price = money;
        }

    }
}
