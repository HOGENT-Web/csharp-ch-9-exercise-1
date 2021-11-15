using Ardalis.GuardClauses;
using Domain.Common;

namespace Domain.Products
{
    public class Category : Entity
    {
        private string name;

        public string Name
        {
            get => name;
            set => name = Guard.Against.NullOrWhiteSpace(value, nameof(name));
        }

        public Category(string name)
        {
            Name = name;
        }
    }
}
