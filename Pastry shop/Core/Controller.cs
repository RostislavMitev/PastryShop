namespace ChristmasPastryShop.Core
{
    using ChristmasPastryShop.Models;
    using ChristmasPastryShop.Models.Booths.Contracts;
    using ChristmasPastryShop.Models.Cocktails.Contracts;
    using ChristmasPastryShop.Models.Delicacies.Contracts;
    using ChristmasPastryShop.Utilities.Messages;
    using Contracts;
    using Repositories;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Controller : IController
    {
        private BoothRepository booths;
        private DelicacyRepository delicacies;
        private CocktailRepository cocktails;



        public Controller()
        {
            booths = new BoothRepository();
            delicacies = new DelicacyRepository();
            cocktails = new CocktailRepository();
        }

        public string AddBooth(int capacity)
        {
            int boothId = booths.Models.Count + 1;
            IBooth newBooth = new Booth(boothId, capacity);
            booths.AddModel(newBooth);

            return string.Format(OutputMessages.NewBoothAdded, boothId, capacity);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            IDelicacy delicacy = delicacies.Models.FirstOrDefault(d => d.Name == delicacyName);
            if (delicacyTypeName != "Gingerbread" && delicacyTypeName != "Stolen")
                return string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            if (delicacy != null)
                return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);

            if (delicacyTypeName == "Gingerbread") delicacy = new Gingerbread(delicacyName);
            if (delicacyTypeName== "Stolen") delicacy = new Stolen(delicacyName);

            delicacies.AddModel(delicacy);

            IBooth booth = new Booth(boothId, 1);
            booths.AddModel(booth);

            return string.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            ICocktail cocktail = cocktails.Models.FirstOrDefault(c => c.Name == cocktailName);
            if (cocktailTypeName != "MulledWine" && cocktailTypeName != "Hibernation")
                return string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            if (size != "Small" && size != "Middle" && size != "Large")
                return string.Format(OutputMessages.InvalidCocktailSize, size);
            if (cocktail != null && cocktail.Size==size)
                return string.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);

            if (cocktailTypeName == "MulledWine") cocktail = new MulledWine(cocktailName, size);
            if (cocktailTypeName == "Hibernation") cocktail = new Hibernation(cocktailName, size);

            cocktails.AddModel(cocktail);

            IBooth booth = new Booth(boothId, 1);
            booths.AddModel(booth);

            return string.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }

        public string ReserveBooth(int countOfPeople)
        {
            List<IBooth> emptyBooths = booths.Models.Where(r => r.IsReserved == false)
                                                        .Where(c => c.Capacity >= countOfPeople)
                                                        .OrderByDescending(c => c.Capacity)
                                                        .ThenByDescending(b => b.BoothId)
                                                        .ToList();
            IBooth firstEmptyBooth = emptyBooths.FirstOrDefault();
            if (firstEmptyBooth == null)
                return string.Format(OutputMessages.NoAvailableBooth, countOfPeople);

            firstEmptyBooth.ChangeStatus();
            return string.Format(OutputMessages.BoothReservedSuccessfully, firstEmptyBooth.BoothId, countOfPeople);
        }

        public string TryOrder(int boothId, string order)
        {
            string[] orderInput = order.Split("/");
            string itemTypeName = orderInput[0];
            string itemName = orderInput[1];
            int countOfOrderedPieces = int.Parse(orderInput[2]);
            string size="";
            if (itemTypeName == "MulledWine" || itemTypeName == "Hibernation")
            {
                size = orderInput[3];
            }

            if (itemTypeName != "Gingerbread" && itemTypeName != "Hibernation" && itemTypeName != "MulledWine" && itemTypeName != "Stolen")
                return string.Format(OutputMessages.NotRecognizedType, itemTypeName);

            bool isAvailable = true;
            IDelicacy delicacy = delicacies.Models.FirstOrDefault(d => d.Name == itemName);
            ICocktail cocktail = cocktails.Models.FirstOrDefault(d => d.Name == itemName);
            if (itemTypeName == "Gingerbread" || itemTypeName == "Stolen")
            {
                if(delicacy == null) isAvailable = false;
            }
            if (itemTypeName == "MulledWine" || itemTypeName == "Hibernation")
            { 
                if (cocktail == null) isAvailable = false;
            }

            if (isAvailable == false)
                return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);

            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            if (itemTypeName == "MulledWine" || itemTypeName == "Hibernation")
            {
                if (itemTypeName == "MulledWine") cocktail = new MulledWine(itemName, size);
                if (itemTypeName == "Hibernation") cocktail = new Hibernation(itemName, size);
                if (cocktail == null || cocktail.Name != itemName || cocktail.Size != size)
                    return string.Format(OutputMessages.NotRecognizedItemName, size, itemName);
                else
                {
                    booth.UpdateCurrentBill(cocktail.Price);
                }
            }

            if (itemTypeName == "Gingerbread" || itemTypeName == "Stolen")
            {
                if (itemTypeName == "Gingerbread") delicacy = new Gingerbread(itemName);
                if (itemTypeName == "Stolen") delicacy = new Stolen(itemName);

                if (delicacy == null || delicacy.Name != itemName)
                    return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
                else
                {
                    booth.UpdateCurrentBill(delicacy.Price);
                }
            }

            return string.Format(OutputMessages.SuccessfullyOrdered, boothId, countOfOrderedPieces, itemName);
        }
        public string LeaveBooth(int boothId)
        {
            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            double currentBill = booth.CurrentBill;
            booth.Charge();
            booth.ChangeStatus();

            StringBuilder sb = new StringBuilder();
            sb
                .AppendLine($"Bill {currentBill:f2} lv")
                .AppendLine($"Booth {boothId} is now available!");

            return sb.ToString().Trim();
        }

        public string BoothReport(int boothId)
        {
            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            
            return booth.ToString();
        }
    }
}
