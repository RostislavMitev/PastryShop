namespace ChristmasPastryShop.Repositories
{
    using System.Collections.Generic;

    using Models.Booths.Contracts;
    using Repositories.Contracts;

    public class BoothRepository : IRepository<IBooth>
    {
        private readonly ICollection<IBooth> models;

        public BoothRepository()
        {
            models = new List<IBooth>();
        }

        public IReadOnlyCollection<IBooth> Models => (IReadOnlyCollection<IBooth>)models;

        public void AddModel(IBooth booth)
            => models.Add(booth);
    }
}
