using Gruppeoppgave1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppeoppgave1.DAL.IRepositories
{
    public interface IBrukerRepository
    {
        Task<bool> LeggTilBruker(Bruker bruker);


        Task<bool> SjekkBruker(Bruker bruker);
        
    }
}
