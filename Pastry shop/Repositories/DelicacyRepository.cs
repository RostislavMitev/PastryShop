namespace ChristmasPastryShop.Repositories
{
    using System.Collections.Generic;

    using Models.Delicacies.Contracts;
    using Repositories.Contracts;
    public class DelicacyRepository : IRepository<IDelicacy>
    {
        private readonly ICollection<IDelicacy> models;

        public DelicacyRepository()
        {
            models = new List<IDelicacy>();
        }

        public IReadOnlyCollection<IDelicacy> Models => (IReadOnlyCollection<IDelicacy>) models;

        public void AddModel(IDelicacy delicacy)
            => models.Add(delicacy);
    }
}
