using Gruppeoppgave1.Model;
using System.Threading.Tasks;

namespace Gruppeoppgave1.DAL.IRepositories
{
    public interface IBrukerRepository
    {

        Task<bool> LoggInn(Bruker bruker);
        
    }
}
