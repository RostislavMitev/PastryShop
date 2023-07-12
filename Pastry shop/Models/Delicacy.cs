namespace ChristmasPastryShop.Models
{
    using System;

    using Delicacies.Contracts;
    using Utilities.Messages;

    public abstract class Delicacy : IDelicacy
    {
        private string name;
        private double price;

        public Delicacy(string delicacyName, double price)
        {
            Name = delicacyName;
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
        public double Price
        {
            get { return price; }
            private set { price = value; }
        }

        public override string ToString()
        {
            return $"{this.Name} - {this.Price:f2} lv";
        }
    }
}
