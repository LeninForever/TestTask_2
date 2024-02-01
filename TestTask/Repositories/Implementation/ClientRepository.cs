using LinqKit;
using Microsoft.EntityFrameworkCore;
using TestTask.Context;
using TestTask.Entities;
using TestTask.Repositories.Filters;
using TestTask.Repositories.Interfaces;

namespace TestTask.Repositories.Implementation
{
    public class ClientRepository : IClientRepository
    {
        private readonly OrderDbContext _dbContext;
        private readonly int _defaultLimit;

        public ClientRepository(OrderDbContext dbContext, int defaultLimit)
        {
            _dbContext = dbContext;
            _defaultLimit = defaultLimit;
        }

        public async Task<Client> CreateAsync(Client entity)
        {
            await _dbContext.Clients.AddAsync(entity);
            return entity;
        }
        public async Task DeleteAsync(Client entity)
        {
            _dbContext.Clients.Remove(entity);
        }

        public async Task<Client?> FindAsync(long id)
        {
            return await _dbContext.Clients.FindAsync(id);
        }

        public async Task<IEnumerable<Client>> GetAllAsync(ClientEntityFilter filter)
        {
            var predicateBuilder = PredicateBuilder.New<Client>();

            predicateBuilder.DefaultExpression = p => true;

            if (!string.IsNullOrEmpty(filter.FirstName))
                predicateBuilder = predicateBuilder.And(p => p.FirstName.ToLower().Contains(filter.FirstName.ToLower()));

            if (!string.IsNullOrEmpty(filter.LastName))
                predicateBuilder = predicateBuilder.And(p => p.LastName.ToLower().Contains(filter.LastName.ToLower()));

            if (filter.BirthDateFrom.HasValue)
                predicateBuilder = predicateBuilder.And(p => p.BirthDate >= filter.BirthDateFrom.Value);

            if (filter.BirthDateTo.HasValue)
                predicateBuilder = predicateBuilder.And(p => p.BirthDate <= filter.BirthDateTo.Value);

            return await _dbContext.Clients
                .Where(predicateBuilder)
                .Skip(filter.Offset ?? 0)
                .Take(filter.Limit ?? _defaultLimit)
                .ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(long id)
        {
            return await _dbContext.Clients.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Client> UpdateAsync(Client entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
