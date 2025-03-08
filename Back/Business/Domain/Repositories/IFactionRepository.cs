using System.Threading.Tasks;
using Business.Domain.Entities;

namespace Business.Domain.Repositories;
public interface IFactionRepository
{
    Task<Faction?> GetByNameAsync(string name);
    Task<Faction> AddAsync(Faction faction);
}
