namespace ChristmasPastryShop.Repositories
{
    using System.Collections.Generic;

    using Models.Cocktails.Contracts;
    using Repositories.Contracts;

    public class CocktailRepository: IRepository<ICocktail>
    {
        private readonly ICollection<ICocktail> models;

        public CocktailRepository()
        {
            models = new List<ICocktail>();
        }

        public IReadOnlyCollection<ICocktail> Models => (IReadOnlyCollection<ICocktail>)models;

        public void AddModel(ICocktail cocktail)
            => models.Add(cocktail);
    }
}
