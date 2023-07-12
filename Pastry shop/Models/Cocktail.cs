namespace ChristmasPastryShop.Models
{
    using System;

    using Utilities.Messages;
    using Cocktails.Contracts;

    public class Cocktail : ICocktail
    {
        private string name;
        private string size;
        private double price;

        public Cocktail(string cocktailName, string size, double price)
        {
            Name = cocktailName;
            Size = size;
            Price = price;
        }

        public string Name
        {
            get { return name; }
            private set 
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);

                name = value; 
            }
        }
        public string Size
        {
            get { return size; }
            private set { size = value; }
        }
        public double Price
        {
            get { return price; }
            private set 
            { 
                if(Size == "Large") price = value;
                if (Size == "Middle") price = (2 / 3) * value;
                if (Size == "Small") price = (1 / 3) * value;
            }
        }

        public override string ToString()
        {
            return $"{this.Name} ({this.Size}) - {this.Price:f2} lv";
        }
    }
}
