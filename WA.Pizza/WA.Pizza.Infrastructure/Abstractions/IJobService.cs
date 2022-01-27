using System.Threading.Tasks;

namespace WA.Pizza.Infrastructure.Abstractions;

public interface IJobService
{
    Task Run();
}