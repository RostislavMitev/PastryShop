namespace ChristmasPastryShop.Models
{
    using System;
    using System.Text;

    using Booths.Contracts;
    using Cocktails.Contracts;
    using Delicacies.Contracts;
    using Repositories.Contracts;
    using Utilities.Messages;

    public class Booth : IBooth
    {
        private int boothId;
        private int capacity;
        private IRepository<IDelicacy> delicacyMenu;
        private IRepository<ICocktail> cocktailMenu;
        private double currentBill;
        private double turnover;
        private bool isReserved;

        public Booth(int boothId, int capacity)
        {
            BoothId = boothId;
            Capacity = capacity;
            CurrentBill = 0;
            Turnover = 0;
            IsReserved = false;
        }

        public int BoothId
        {
            get { return boothId; }
            private set { boothId = value; }
        }
        public int Capacity
        {
            get { return capacity; }
            private set 
            {
                if (value <= 0) throw new ArgumentException(ExceptionMessages.CapacityLessThanOne);

                capacity = value; 
            }
        }
        public IRepository<IDelicacy> DelicacyMenu
        {
            get { return delicacyMenu; }
            private set { delicacyMenu = value; }
        }
        public IRepository<ICocktail> CocktailMenu
        {
            get { return cocktailMenu; }
            private set { cocktailMenu = value; }
        }
        public double CurrentBill
        {
            get { return currentBill; }
            private set { currentBill = value; }
        }
        public double Turnover
        {
            get { return turnover; }
            private set { turnover = value; }
        }
        public bool IsReserved
        {
            get { return isReserved; }
            private set { isReserved = value; }
        }
        public void UpdateCurrentBill(double amount)
        {
            CurrentBill += amount;
        }
        public void Charge()
        {
            Turnover += CurrentBill;
            CurrentBill = 0;
        }
        public void ChangeStatus()
        {
            IsReserved = !IsReserved;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb
                .AppendLine($"Booth: {this.BoothId}")
                .AppendLine($"Capacity: {this.Capacity}")
                .AppendLine($"Turnover: {Turnover:f2} lv")
                .AppendLine($"-Cocktail menu:");

            foreach (ICocktail cocktail in cocktailMenu.Models)
            {
                sb.AppendLine($"--{cocktail.ToString()}");
            }

            sb.AppendLine($"-Delicacy menu:");

            foreach (IDelicacy delicacy in delicacyMenu.Models)
            {
                sb.AppendLine($"--{delicacy.ToString()}");
            }

            return sb.ToString().Trim();
        }
    }
}
