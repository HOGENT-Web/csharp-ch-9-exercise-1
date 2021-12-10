using Bogus;
using System;

namespace Domain.Common
{
    public abstract class EntityFaker<T> : Faker<T> where T : Entity
    {
        public EntityFaker(bool hasRandomId = true)
        {
            UseSeed(1337);
            if (hasRandomId)
            {
                RuleFor(x => x.Id, f => ++f.IndexVariable);
            }

            RuleFor(x => x.CreatedAt, f => f.Date.Between(DateTime.Now.AddMonths(-2), DateTime.Now.AddMonths(-1)));
            RuleFor(x => x.UpdatedAt, f => f.Date.Between(DateTime.Now.AddMonths(-1), DateTime.Now));
        }
    }
}
