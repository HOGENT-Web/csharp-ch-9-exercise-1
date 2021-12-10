using Domain.Common;
using EntityFrameworkCore.Triggered;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Triggers
{
    /// <summary>
    /// Before an <see cref="Entity"/> is saved to the datebase, we set the <seealso cref="Entity.CreatedAt"/> or <seealso cref="Entity.UpdatedAt"/>.
    /// </summary>
    public class OnBeforeEntitySaved : IBeforeSaveTrigger<Entity>
    {
        public Task BeforeSave(ITriggerContext<Entity> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added)
            {
                context.Entity.CreatedAt = DateTime.UtcNow;
            }

            if (context.ChangeType == ChangeType.Modified)
            {
                context.Entity.UpdatedAt = DateTime.UtcNow;
            }

            // If you want a soft-delete instead of a real one, this is perfectly possible here too.

            return Task.CompletedTask;
        }
    }
}
