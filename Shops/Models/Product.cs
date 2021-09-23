namespace Shops.Models
{
    public class Product
    {
        private string _name;
        private string _id;

        public Product(string name)
        {
            Name = name;
            _id = System.Guid.NewGuid().ToString();
        }

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public string Id => _id;
    }
}