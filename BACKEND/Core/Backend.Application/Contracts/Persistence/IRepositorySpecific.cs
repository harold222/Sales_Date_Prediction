using Backend.Application.Functions.Vm;
using Backend.Domain;

namespace Backend.Application.Contracts.Persistence;

public interface IRepositorySpecific
{
    Task<IEnumerable<Orders>> GetOrderByClient(int Id);
}
