using Microsoft.EntityFrameworkCore;
using MyFootball.Domain.Entities;

namespace MyFootball.Application.Interfaces
{
    public interface IApplicationDBContext
    {
        public interface IApplicationDbContext
        {
            DbSet<User> Users { get; }
            Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        }

    }
}
