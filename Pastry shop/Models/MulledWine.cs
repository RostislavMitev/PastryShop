namespace ChristmasPastryShop.Models
{
    public class MulledWine : Cocktail
    {
        private const double LargeMuledWinePrice = 13.50;
        public MulledWine(string cocktailName, string size) : base(cocktailName, size, LargeMuledWinePrice)
        {
        }
    }
}
